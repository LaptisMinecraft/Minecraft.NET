using Bridge.Launcher;

namespace Bridge.Game;

public interface IRunningGame
{
    public IGameVersion Version { get; }

    public ILanguage SelectedLanguage { get; }

    public IGameSession CurrentSession { get; }

    public IPerformanceMetrics PerformanceMetrics { get; }

    public ISessionEventListener SessionEventListener { set; }
}