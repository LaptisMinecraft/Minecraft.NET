namespace Stream.Extensions;

public static class FunctionExtension
{
    public static Func<TV, TR> Compose<T, TR, TV>(this Func<T, TR> function, Func<TV, T> before) =>
        tv => function(before(tv));

    public static Func<T, TV> AndThen<T, TR, TV>(this Func<T, TR> function, Func<TR, TV> after) =>
        t => after(function(t));

    public static Func<T, T> Identity<T>() => t => t;
}