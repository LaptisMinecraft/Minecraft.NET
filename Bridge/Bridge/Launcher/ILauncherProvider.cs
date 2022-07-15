namespace Bridge.Launcher;

public interface ILauncherProvider
{
    public ILauncher? CreateLauncher();
}