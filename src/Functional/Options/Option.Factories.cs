namespace Toarnbeike.Options;

/// <summary>
/// Provides factory methods for working with <see cref="Option{TValue}"/> instances.
/// </summary>
public static partial class Option
{
    /// <summary>
    /// Gets an <see cref="Option{NoContent}"/> instance representing the absence of a value.
    /// </summary>
    /// <remarks>
    /// Useful when a generic <see cref="Option{TValue}"/> is expected but no value is provided.
    /// Enables shorthand conversion using an implicit operator to <see cref="Option{TValue}"/>.
    /// </remarks>
    public static Option<NoContent> None => Option<NoContent>.None();

    /// <summary>
    /// Creates an <see cref="Option{TValue}"/> containing the specified non-null value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value to wrap. Must not be <see langword="null"/>.</param>
    /// <returns>An <see cref="Option{TValue}"/> instance containing the value.</returns>
    public static Option<TValue> Some<TValue>(TValue value) => Option<TValue>.Some(value);
}