using Bridge.Game;

namespace Bridge.Launcher;

public interface ILauncher
{
    public void RegisterGame(IRunningGame game);
}