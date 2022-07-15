using Bridge.Launcher;

namespace Bridge;

public static class Bridge
{
    private static ILauncher? _launcher;

    public static void RegisterLauncherProvider(ILauncherProvider provider)
    {
        var launcher = provider.CreateLauncher();
        if (launcher is not null) _launcher = launcher;
    }

    public static ILauncher? GetLauncher() => _launcher;
}