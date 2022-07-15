namespace Stream;

public interface ICollector<in T, TA, out TR> where TA : notnull where TR : TA
{
    Func<TA> Supplier { get; }

    Action<TA, T> Accumulator { get; }

    Func<TA, TA, TA> Combiner { get; }

    Func<TA, TR> Finisher { get; }

    Characteristics Characteristics { get; }

    public static ICollector<T, TR, TR> Of(Func<TR> supplier, Action<TR, T> accumulator, Func<TR, TR, TR> combiner,
        Characteristics characteristics) => new Collectors.CollectorImpl<T, TR, TR>(supplier, accumulator, combiner,
        characteristics.Length() == 0 ? Collectors.ChId : Characteristics.IdentityFinish);

    public static ICollector<T, TA, TR> Of(Func<TA> supplier, Action<TA, T> accumulator, Func<TA, TA, TA> combiner,
        Func<TA, TR> finisher, Characteristics characteristics) => new Collectors.CollectorImpl<T, TA, TR>(supplier,
        accumulator, combiner, finisher, characteristics.Length() == 0 ? Collectors.ChNoId : characteristics);
}

[Flags]
public enum Characteristics
{
    None = 0,
    Concurrent = 1,
    Unordered = 2,
    IdentityFinish = 4
}

public static class CharacteristicsExtension
{
    public static int Length(this Characteristics characteristics) =>
        (Characteristics.Concurrent == (characteristics & Characteristics.Concurrent) ? 1 : 0) +
        (Characteristics.Unordered == (characteristics & Characteristics.Unordered) ? 1 : 0) +
        (Characteristics.IdentityFinish == (characteristics & Characteristics.IdentityFinish) ? 1 : 0);
}