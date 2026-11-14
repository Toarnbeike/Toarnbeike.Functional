namespace Toarnbeike.Options;

public struct Option
{
    /// <summary>
    /// Static factory method to create an <see cref="Option{None}"/> instance without having to specify the type argument.
    /// </summary>
    public static Option<None> None => Option<None>.None;

    /// <summary>
    /// Static factory method to create an <see cref="Option{TValue}"/> without needing to specify the type argument.
    /// </summary>
    public static Option<TValue> Some<TValue>(TValue value) where TValue : notnull
        => Option<TValue>.Some(value);
}