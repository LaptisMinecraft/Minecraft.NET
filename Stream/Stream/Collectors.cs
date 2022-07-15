namespace Stream;

public static class Collectors
{
    public static readonly Characteristics ChConcurrentId =
        Characteristics.Concurrent | Characteristics.Unordered | Characteristics.IdentityFinish;

    public static readonly Characteristics ChConcurrentNoId = Characteristics.Concurrent | Characteristics.Unordered;
    public static readonly Characteristics ChId = Characteristics.IdentityFinish;
    public static readonly Characteristics ChUnorderedId = Characteristics.Unordered | Characteristics.IdentityFinish;
    public static readonly Characteristics ChNoId = Characteristics.None;
    public static readonly Characteristics ChUnorderedNoId = Characteristics.Unordered;

    private static InvalidOperationException DuplicateKeyException(object k, object u, object v) =>
        new($"Duplicate key {k} (attempted merging values {u} and {v})");

    private static Func<TD, TD, TD> UniqueKeysDictionaryMerger<TK, TV, TD>()
        where TD : IDictionary<TK, TV> where TK : notnull where TV : notnull => (d1, d2) =>
    {
        foreach (var (k, v) in d2)
            if (!d1.TryAdd(k, v))
                throw DuplicateKeyException(k, d1[k], v);
        return d1;
    };

    private static Action<IDictionary<TK, TV>, T> UniqueKeysDictionaryAccumulator<T, TK, TV>(Func<T, TK> keyMapper,
        Func<T, TV> valueMapper) where TK : notnull where TV : notnull => (map, element) =>
    {
        var k = keyMapper(element);
        var v = valueMapper(element);
        if (!map.TryAdd(k, v)) throw DuplicateKeyException(k, map[k], v);
    };

    private static Func<TI, TR> CastingIdentity<TI, TR>() where TI : notnull where TR : TI => i => (TR)i;

    public class CollectorImpl<T, TA, TR> : ICollector<T, TA, TR> where TA : notnull where TR : TA
    {
        public CollectorImpl(Func<TA> supplier, Action<TA, T> accumulator, Func<TA, TA, TA> combiner,
            Func<TA, TR> finisher, Characteristics characteristics)
        {
            Supplier = supplier;
            Accumulator = accumulator;
            Combiner = combiner;
            Finisher = finisher;
            Characteristics = characteristics;
        }

        public CollectorImpl(Func<TA> supplier, Action<TA, T> accumulator, Func<TA, TA, TA> combiner,
            Characteristics characteristics) : this(supplier, accumulator, combiner, CastingIdentity<TA, TR>(),
            characteristics)
        {
        }

        public Func<TA> Supplier { get; }

        public Action<TA, T> Accumulator { get; }

        public Func<TA, TA, TA> Combiner { get; }

        public Func<TA, TR> Finisher { get; }

        public Characteristics Characteristics { get; }
    }

    public static void AddRange<T>(this ICollection<T> collection, ICollection<T> other)
    {
        foreach (var t in other)
            collection.Add(t);
    }

    public static ICollector<T, TA, TC> ToCollection<T, TA, TC>(Func<TC> collectionFactory)
        where TA : notnull where TC : ICollection<T>, TA => new CollectorImpl<T, TA, TC>(() => collectionFactory(),
        (ta, t) => ((ICollection<T>)ta).Add(t), (r1, r2) =>
        {
            ((ICollection<T>)r1).AddRange((ICollection<T>)r2);
            return r1;
        }, ChId);
}