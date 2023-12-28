using Modix.Data.Repositories;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Modix.Data.Test.Repositories;

[TestFixture]
public class ModerationActionRepositoryTests
{
    [Test]
    public void Constructor_Always_InvokesBaseConstructor()
    {
        var modixContext = Substitute.For<ModixContext>();

        var uut = new ModerationActionRepository(modixContext);

        uut.ModixContext.ShouldBeSameAs(modixContext);
    }
}
