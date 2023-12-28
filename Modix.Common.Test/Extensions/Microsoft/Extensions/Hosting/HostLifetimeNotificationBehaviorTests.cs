using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modix.Common.Messaging;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Modix.Common.Test.Extensions.Microsoft.Extensions.Hosting
{
    [TestFixture]
    public class HostLifetimeNotificationBehaviorTests
    {
        #region Test Context

        public class TestContext
            : AsyncMethodWithLoggerTestContext
        {
            public TestContext()
            {
                _mockMessagePublisher = new Mock<IMessagePublisher>();

                _mockServiceProvider = new Mock<IServiceProvider>();
                _mockServiceProvider
                    .Setup(x => x.GetService(typeof(IMessagePublisher)))
                    .Returns(() => _mockMessagePublisher.Object);

                _mockServiceScope = new Mock<IServiceScope>();
                _mockServiceScope
                    .Setup(x => x.ServiceProvider)
                    .Returns(() => _mockServiceProvider.Object);

                _mockServiceScopeFactory = new Mock<IServiceScopeFactory>();
                _mockServiceScopeFactory
                    .Setup(x => x.CreateScope())
                    .Returns(() => _mockServiceScope.Object);
            }

            public HostLifetimeNotificationBehavior BuildUut()
                => new(
                    _loggerFactory.CreateLogger<HostLifetimeNotificationBehavior>(),
                    _mockServiceScopeFactory.Object);

            public readonly Mock<IMessagePublisher> _mockMessagePublisher;
            public readonly Mock<IServiceProvider> _mockServiceProvider;
            public readonly Mock<IServiceScope> _mockServiceScope;
            public readonly Mock<IServiceScopeFactory> _mockServiceScopeFactory;
        }

        #endregion Test Context

        #region StartAsync() Tests

        [Test]
        public async Task StartAsync_Always_PublishesNotificationWithinScope()
        {
            using var testContext = new TestContext();

            var uut = testContext.BuildUut();

            await uut.StartAsync(testContext.CancellationToken);

            testContext._mockMessagePublisher.ShouldHaveReceived(x => x
                .PublishAsync(It.IsNotNull<HostStartingNotification>(), testContext.CancellationToken));

            testContext._mockServiceScope.ShouldHaveReceived(x => x
                .Dispose());
        }

        #endregion StartAsync() Tests

        #region StopAsync() Tests

        [Test]
        public async Task StopAsync_Always_PublishesNotificationWithinScope()
        {
            using var testContext = new TestContext();

            var uut = testContext.BuildUut();

            await uut.StopAsync(testContext.CancellationToken);

            testContext._mockMessagePublisher.ShouldHaveReceived(x => x
                .PublishAsync(It.IsNotNull<HostStoppingNotification>(), testContext.CancellationToken));

            testContext._mockServiceScope.ShouldHaveReceived(x => x
                .Dispose());
        }

        #endregion StopAsync() Tests
    }
}
