namespace Stream.Extensions;

public static class BiConsumerExtension
{
    public static Action<T, TU> AndThen<T, TU>(this Action<T, TU> biConsumer, Action<T, TU> after) => (l, r) =>
    {
        biConsumer(l, r);
        after(l, r);
    };
}