namespace Stream.Extensions;

public static class BinaryOperatorExtension
{
    public static Func<T, T, T> AndThen<T>(this Func<T, T, T> binaryOperator, Func<T, T> after) =>
        binaryOperator.AndThen<T, T, T, T>(after);

    public static Func<T, T, T> MinBy<T>() where T : IComparable<T> => (a, b) => a.CompareTo(b) <= 0 ? a : b;

    public static Func<T, T, T> MaxBy<T>() where T : IComparable<T> => (a, b) => a.CompareTo(b) >= 0 ? a : b;
}