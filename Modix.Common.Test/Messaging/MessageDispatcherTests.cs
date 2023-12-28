using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modix.Common.Messaging;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Modix.Common.Test.Messaging;

[TestFixture]
public class MessageDispatcherTests
{
    #region Test Context

    public class TestContext
        : AsyncMethodWithLoggerTestContext
    {
        public TestContext()
        {
            _logger = _loggerFactory.CreateLogger<MessageDispatcher>();

            _mockCancellationTokenSource = new Mock<ICancellationTokenSource>();
            _mockCancellationTokenSource
                .Setup(x => x.Token)
                .Returns(() => CancellationToken);

            _mockCancellationTokenSourceFactory = new Mock<ICancellationTokenSourceFactory>();
            _mockCancellationTokenSourceFactory
                .Setup(x => x.Create(It.IsAny<TimeSpan>()))
                .Returns(() => _mockCancellationTokenSource.Object);

            _mockServiceProvider = new Mock<IServiceProvider>();

            _mockServiceScope = new Mock<IServiceScope>();
            _mockServiceScope
                .Setup(x => x.ServiceProvider)
                .Returns(() => _mockServiceProvider.Object);

            _mockServiceScopeFactory = new Mock<IServiceScopeFactory>();
            _mockServiceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(() => _mockServiceScope.Object);

            _options = Microsoft.Extensions.Options.Options.Create(new MessagingOptions());
        }

        public MessageDispatcher BuildUut()
            => new(
                _mockCancellationTokenSourceFactory.Object,
                _logger,
                _options,
                _mockServiceScopeFactory.Object);

        public readonly ILogger<MessageDispatcher> _logger;

        public readonly Mock<ICancellationTokenSource> _mockCancellationTokenSource;
        public readonly Mock<ICancellationTokenSourceFactory> _mockCancellationTokenSourceFactory;
        public readonly Mock<IServiceProvider> _mockServiceProvider;
        public readonly Mock<IServiceScope> _mockServiceScope;
        public readonly Mock<IServiceScopeFactory> _mockServiceScopeFactory;

        public readonly IOptions<MessagingOptions> _options;
    }

    #endregion Test Context

    #region DispatchAsync() Tests

    public static readonly ImmutableArray<TestCaseData> DispatchAsync_TestCaseData
        =
        [
            new TestCaseData(TimeSpan.Zero, 0, TimeSpan.Zero, TimeSpan.Zero).SetName("{m}(No handlers registered)"),
            new TestCaseData(TimeSpan.Zero, 1, TimeSpan.Zero, TimeSpan.Zero).SetName("{m}(One handler registered)"),
            new TestCaseData(TimeSpan.Zero, 3, TimeSpan.Zero, TimeSpan.Zero).SetName("{m}(Many handlers registered)"),
            new TestCaseData(TimeSpan.FromSeconds(1), 1, null, TimeSpan.FromSeconds(1)).SetName("{m}(Timeout not given)"),
            new TestCaseData(TimeSpan.FromSeconds(2), 1, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(3)).SetName("{m}(Timeout given)"),
        ];

    [TestCaseSource(nameof(DispatchAsync_TestCaseData))]
    public async Task DispatchAsync_Always_InvokesHandlersInServiceScopeWithTimeout(
        TimeSpan dispatchTimeout,
        int handlerCount,
        TimeSpan? timeout,
        TimeSpan cancellationTokenSourceDelay)
    {
        using var testContext = new TestContext();
        testContext._options.Value.DispatchTimeout = dispatchTimeout;

        var mockHandlers = Enumerable.Range(0, handlerCount)
            .Select(_ => new Mock<INotificationHandler<object>>())
            .ToArray();

        testContext._mockServiceProvider
            .Setup(x => x.GetService(typeof(IEnumerable<INotificationHandler<object>>)))
            .Returns(mockHandlers.Select(x => x.Object));

        var uut = testContext.BuildUut();

        var notification = new object();

        await uut.DispatchAsync(notification, timeout);

        testContext._mockCancellationTokenSourceFactory.ShouldHaveReceived(x => x
            .Create(cancellationTokenSourceDelay));

        testContext._mockServiceScopeFactory.ShouldHaveReceived(x => x
            .CreateScope());

        foreach (var mockHandler in mockHandlers)
        {
            mockHandler.ShouldHaveReceived(x => x
                .HandleNotificationAsync(notification, testContext.CancellationToken));
        }

        testContext._mockServiceScope.ShouldHaveReceived(x => x
            .Dispose());
    }

    [Test]
    public async Task DispatchAsync_NotificationIsLogScopeProvider_CreatesLogScope()
    {
        using var testContext = new TestContext();

        var mockHandler = new Mock<INotificationHandler<object>>();

        testContext._mockServiceProvider
            .Setup(x => x.GetService(typeof(IEnumerable<INotificationHandler<object>>)))
            .Returns(EnumerableEx.From(mockHandler.Object));

        var uut = testContext.BuildUut();

        var mockNotificationLogScope = new Mock<IDisposable>();

        var mockNotification = new Mock<ILogScopeProvider>();
        mockNotification
            .Setup(x => x.BeginLogScope(It.IsAny<ILogger>()))
            .Returns(() => mockNotificationLogScope.Object);

        await uut.DispatchAsync(mockNotification.Object);

        mockNotification.ShouldHaveReceived(x => x
            .BeginLogScope(testContext._logger));

        mockNotificationLogScope.ShouldHaveReceived(x => x
            .Dispose());
    }

    public static readonly ImmutableArray<TestCaseData> DispatchAsync_HandlerThrowsException_TestCaseData
        =
        [
            new TestCaseData(1, 0).SetName("{m}(Single handler)"),
            new TestCaseData(3, 0).SetName("{m}(First handler)"),
            new TestCaseData(3, 1).SetName("{m}(Second handler)"),
            new TestCaseData(3, 2).SetName("{m}(Third handler)"),
        ];

    [TestCaseSource(nameof(DispatchAsync_HandlerThrowsException_TestCaseData))]
    public async Task DispatchAsync_HandlerThrowsException_InvokesOtherHandlers(
        int handlerCount,
        int handlerExceptionIndex)
    {
        using var testContext = new TestContext();

        var mockHandlers = Enumerable.Range(0, handlerCount)
            .Select(_ => new Mock<INotificationHandler<object>>())
            .ToArray();

        testContext._mockServiceProvider
            .Setup(x => x.GetService(typeof(IEnumerable<INotificationHandler<object>>)))
            .Returns(mockHandlers.Select(x => x.Object));

        var exception = new Exception();
        mockHandlers[handlerExceptionIndex]
            .Setup(x => x.HandleNotificationAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Throws(exception);

        var uut = testContext.BuildUut();

        var notification = new object();

        await uut.DispatchAsync(notification);

        foreach (var mockHandler in mockHandlers)
        {
            mockHandler.ShouldHaveReceived(x => x
                .HandleNotificationAsync(notification, testContext.CancellationToken));
        }

        testContext._mockServiceScope.ShouldHaveReceived(x => x
            .Dispose());
    }

    #endregion DispatchAsync() Tests
}
