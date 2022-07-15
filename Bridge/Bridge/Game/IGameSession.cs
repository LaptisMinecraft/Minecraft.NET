namespace Bridge.Game;

public interface IGameSession
{
    public int PlayerCount { get; }

    public bool IsRemoteServer { get; }

    public string Difficulty { get; }

    public string GameMode { get; }

    public Guid SessionId { get; }
}