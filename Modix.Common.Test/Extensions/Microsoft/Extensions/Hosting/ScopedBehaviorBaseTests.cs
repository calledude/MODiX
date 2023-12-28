using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;
using NUnit.Framework;
using Shouldly;

namespace Modix.Common.Test.Extensions.Microsoft.Extensions.Hosting;

[TestFixture]
public class ScopedBehaviorBaseTests
{
    #region Test Context

    public class TestContext
        : AsyncMethodWithLoggerTestContext
    {
        public TestContext()
        {
            _mockServiceProvider = new Mock<IServiceProvider>();

            _mockServiceScope = new Mock<IServiceScope>();
            _mockServiceScope
                .Setup(x => x.ServiceProvider)
                .Returns(() => _mockServiceProvider.Object);

            _mockServiceScopeFactory = new Mock<IServiceScopeFactory>();
            _mockServiceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(() => _mockServiceScope.Object);
        }

        public Mock<ScopedBehaviorBase> BuildMockUut()
            => new(
                _loggerFactory.CreateLogger<ScopedBehaviorBase>(),
                _mockServiceScopeFactory.Object)
            {
                CallBase = true
            };

        public readonly Mock<IServiceProvider> _mockServiceProvider;
        public readonly Mock<IServiceScope> _mockServiceScope;
        public readonly Mock<IServiceScopeFactory> _mockServiceScopeFactory;
    }

    #endregion Test Context

    #region StartAsync() Tests

    [Test]
    public async Task StartAsync_Always_CreatesScopeAndInvokesOnStartingAsync()
    {
        using var testContext = new TestContext();

        var mockUut = testContext.BuildMockUut();

        await mockUut.Object.StartAsync(testContext.CancellationToken);

        mockUut.ShouldHaveReceived(x => x
            .OnStartingAsync(testContext._mockServiceProvider.Object, testContext.CancellationToken));

        testContext._mockServiceScope.ShouldHaveReceived(x => x
            .Dispose());
    }

    #endregion StartAsync() Tests

    #region StopAsync() Tests

    [Test]
    public async Task StopAsync_Always_CreatesScopeAndInvokesOnStoppingAsync()
    {
        using var testContext = new TestContext();

        var mockUut = testContext.BuildMockUut();

        await mockUut.Object.StopAsync(testContext.CancellationToken);

        mockUut.ShouldHaveReceived(x => x
            .OnStoppingAsync(testContext._mockServiceProvider.Object, testContext.CancellationToken));

        testContext._mockServiceScope.ShouldHaveReceived(x => x
            .Dispose());
    }

    #endregion StopAsync() Tests
}
