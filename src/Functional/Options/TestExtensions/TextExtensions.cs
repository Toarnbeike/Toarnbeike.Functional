namespace Toarnbeike.Options.TestExtensions;

public static class TextExtensions
{
    /// <summary>
    /// Asserts that the option is Some; throws an exception if it is None.
    /// </summary>
    /// <typeparam name="TValue">The value type inside the Option.</typeparam>
    /// <param name="option">The option to assert on.</param>
    /// <param name="message">Optional custom failure message.</param>
    /// <returns>The inner value if present.</returns>
    /// <exception cref="FunctionalAssertException">Thrown if the Option is None.</exception>
    public static TValue ShouldBeSome<TValue>(this Option<TValue> option, string? message = null)
    {
        return option.TryGetValue(out var value)
            ? value
            : throw new FunctionalAssertException(message ?? $"Expected Option<{typeof(TValue).Name}> to be Some, but it was None.");
    }

    /// <summary>
    /// Asserts that the option is the expected value; throws an exception if it is None or a different value.
    /// </summary>
    /// <typeparam name="TValue">The value type inside the Option.</typeparam>
    /// <param name="option">The option to assert on.</param>
    /// <param name="expected">The expected value of the option.</param>
    /// <param name="message">Optional custom failure message.</param>
    /// <returns>The inner value if present.</returns>
    /// <exception cref="FunctionalAssertException">Thrown if the Option is None or Some other than expected.</exception>
    public static TValue ShouldBeSomeWithValue<TValue>(this Option<TValue> option, TValue expected, string? message = null)
    {
        return option.TryGetValue(out var value) && EqualityComparer<TValue>.Default.Equals(value, expected)
            ? value
            : throw new FunctionalAssertException(message ?? $"Expected Option<{typeof(TValue).Name}> to be {expected}, but it was not.");
    }

    /// <summary>
    /// Asserts that the option is the expected value; throws an exception if it is None or a different value.
    /// </summary>
    /// <typeparam name="TValue">The value type inside the Option.</typeparam>
    /// <param name="option">The option to assert on.</param>
    /// <param name="expected">The expected value of the option.</param>
    /// <param name="comparer">The equality comparer to use for the value comparison.</param>
    /// <param name="message">Optional custom failure message.</param>
    /// <returns>The inner value if present.</returns>
    /// <exception cref="FunctionalAssertException">Thrown if the Option is None or Some other than expected.</exception>
    public static TValue ShouldBeSomeWithValue<TValue>(this Option<TValue> option, TValue expected, IEqualityComparer<TValue> comparer, string? message = null)
    {
        return option.TryGetValue(out var value) && comparer.Equals(value, expected)
            ? value
            : throw new FunctionalAssertException(message ?? $"Expected Option<{typeof(TValue).Name}> to be {expected}, but it was {option}.");
    }

    /// <summary>
    /// Asserts that the option is the expected value; throws an exception if it is None or a different value.
    /// </summary>
    /// <typeparam name="TValue">The value type inside the Option.</typeparam>
    /// <param name="option">The option to assert on.</param>
    /// <param name="predicate">The predicate to match.</param>
    /// <param name="message">Optional custom failure message.</param>
    /// <returns>The inner value if present.</returns>
    /// <exception cref="FunctionalAssertException">Thrown if the Option is None or Some other than expected.</exception>
    public static TValue ShouldBeSomeThatMatches<TValue>(this Option<TValue> option, Func<TValue, bool> predicate, string? message = null)
    {
        return option.TryGetValue(out var value) && predicate(value)
            ? value
            : throw new FunctionalAssertException(message ?? $"Expected Option<{typeof(TValue).Name}> to match predicate, but it didn't.");
    }

    /// <summary>
    /// Asserts that the option is Some and satisfies the specified assertion; throws an exception if it is None or the assertion fails.
    /// </summary>
    /// <typeparam name="TValue">The value type inside the Option.</typeparam>
    /// <param name="option">The option to assert on.</param>
    /// <param name="assert">Secondary assertion on TVale, given that Option is Some.</param>
    /// <param name="message">Optional custom failure message.</param>
    /// <returns>The inner value if present.</returns>
    /// <exception cref="FunctionalAssertException">Thrown if the Option is None.</exception>
    public static TValue ShouldBeSomeAndSatisfy<TValue>(this Option<TValue> option, Action<TValue> assert, string? message = null)
    {
        var value = option.ShouldBeSome(message);
        assert(value);
        return value;
    }

    /// <summary>
    /// Asserts that the option is None; throws an exception if it is Some.
    /// </summary>
    /// <typeparam name="TValue">The value type inside the Option.</typeparam>
    /// <param name="option">The option to assert on.</param>
    /// <param name="message">Optional custom failure message.</param>
    /// <exception cref="FunctionalAssertException">Thrown if the Option is Some.</exception>
    public static void ShouldBeNone<TValue>(this Option<TValue> option, string? message = null)
    {
        if (option.TryGetValue(out var value))
        {
            throw new FunctionalAssertException(message ?? $"Expected Option<{typeof(TValue).Name}> to be None, but it was Some with value {value}.");
        }
    }
}