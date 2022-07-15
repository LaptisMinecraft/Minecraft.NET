namespace Stream.Extensions;

public static class BiFunctionExtension
{
    public static Func<T, TU, TV> AndThen<T, TU, TR, TV>(this Func<T, TU, TR> biFunction, Func<TR, TV> after) =>
        (t, tu) => after(biFunction(t, tu));
}