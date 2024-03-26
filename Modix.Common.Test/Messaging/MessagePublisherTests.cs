﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Modix.Common.Messaging;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Modix.Common.Test.Messaging;

[TestFixture]
public class MessagePublisherTests
{
    #region Test Context

    public class TestContext
        : AsyncMethodWithLoggerTestContext
    {
        public TestContext()
        {
            _logger = _loggerFactory.CreateLogger<MessagePublisher>();

            _mockServiceProvider = new Mock<IServiceProvider>();
        }

        public MessagePublisher BuildUut()
            => new(
                _logger,
                _mockServiceProvider.Object);

        public readonly ILogger<MessagePublisher> _logger;

        public readonly Mock<IServiceProvider> _mockServiceProvider;
    }

    #endregion Test Context

    #region PublishAsync() Tests

    public static readonly ImmutableArray<TestCaseData> PublishAsync_TestCaseData
        =
        [
            new TestCaseData(0).SetName("{m}(No handlers)"),
            new TestCaseData(1).SetName("{m}(Single handler)"),
            new TestCaseData(3).SetName("{m}(Many handlers)"),
        ];

    [TestCaseSource(nameof(PublishAsync_TestCaseData))]
    public async Task PublishAsync_Always_InvokesHandlers(
        int handlerCount)
    {
        using var testContext = new TestContext();

        var mockHandlers = Enumerable.Range(0, handlerCount)
            .Select(_ => new Mock<INotificationHandler<object>>())
            .ToArray();

        testContext._mockServiceProvider
            .Setup(x => x.GetService(typeof(IEnumerable<INotificationHandler<object>>)))
            .Returns(mockHandlers.Select(x => x.Object));

        var uut = testContext.BuildUut();

        var notification = new object();

        await uut.PublishAsync(notification, testContext.CancellationToken);

        foreach (var mockHandler in mockHandlers)
        {
            mockHandler.ShouldHaveReceived(x => x
                .HandleNotificationAsync(notification, testContext.CancellationToken));
        }
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

        await uut.PublishAsync(mockNotification.Object as object, testContext.CancellationToken);

        mockNotification.ShouldHaveReceived(x => x
            .BeginLogScope(testContext._logger));

        mockNotificationLogScope.ShouldHaveReceived(x => x
            .Dispose());
    }

    #endregion PublishAsync() Tests
}
