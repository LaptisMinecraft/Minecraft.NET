using Bridge.Game;
using Bridge.Launcher;

namespace Bridge.Test;

[TestClass]
public class BridgeTest
{
    public class TestLauncher : ILauncher
    {
        public void RegisterGame(IRunningGame game)
        {
        }
    }

    public class TestLauncherProvider : ILauncherProvider
    {
        private readonly ILauncher _testLauncher = new TestLauncher();

        public ILauncher CreateLauncher() => _testLauncher;
    }

    [TestMethod]
    public void Registration()
    {
        ILauncherProvider testProvider1 = new TestLauncherProvider();
        Bridge.RegisterLauncherProvider(testProvider1);
        Assert.AreSame(Bridge.GetLauncher(), testProvider1.CreateLauncher());

        ILauncherProvider testProvider2 = new TestLauncherProvider();
        Bridge.RegisterLauncherProvider(testProvider2);
        Assert.AreSame(Bridge.GetLauncher(), testProvider2.CreateLauncher());
        Assert.AreNotSame(Bridge.GetLauncher(), testProvider1.CreateLauncher());
    }
}