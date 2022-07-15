namespace Bridge.Game;

public interface IGameVersion
{
    public string Id { get; }

    public string Name { get; }

    public string ReleaseTarget { get; }

    public string SeriesId => "main";

    public int WorldVersion { get; }

    public int ProtocolVersion { get; }

    public int ResourcePackVersion { get; }

    public int DataPackVersion { get; }

    public int GetPackVersion(PackType type) =>
        type switch
        {
            PackType.Resource => ResourcePackVersion,
            PackType.Data => DataPackVersion,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    public DateTime BuildTime { get; }

    public bool Stable { get; }
}