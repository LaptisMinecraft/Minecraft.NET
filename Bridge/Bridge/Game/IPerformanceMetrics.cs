namespace Bridge.Game;

public interface IPerformanceMetrics
{
    public int MinTime { get; }

    public int MaxTime { get; }
    
    public int AverageTime { get; }

    public int SampleCount { get; }
}