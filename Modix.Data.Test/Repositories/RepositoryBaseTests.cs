using Modix.Data.Repositories;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Modix.Data.Test.Repositories
{
    [TestFixture]
    public class RepositoryBaseTests
    {
        [Test]
        public void Constructor_Always_ModixContextIsGiven()
        {
            var modixContext = TestDataContextFactory.BuildTestDataContext();

            var uut = Substitute.For<RepositoryBase>(modixContext);

            uut.ModixContext.ShouldBeSameAs(modixContext);
        }
    }
}
