using Bridge.Game;

namespace Bridge.Launcher;

public interface ISessionEventListener
{
    public static readonly ISessionEventListener None = new NoneSessionEventListener();

    public void OnStartGameSession(IGameSession session)
    {
    }

    public void OnLeaveGameSession(IGameSession session)
    {
    }

    private class NoneSessionEventListener : ISessionEventListener
    {
    }
}