namespace Toarnbeike.Options.Extensions.Unsafe;

/// <summary>
/// Reduce or throw extensions are placed in the Unsafe namespace because they throw exceptions when the option is empty.
/// </summary>
public static class ReduceOrThrowExtensions
{
    /// <summary>
    /// Extracts the value from the specified <see cref="Option{TValue}"/> if it contains a value; otherwise, throws an
    /// <see cref="InvalidOperationException"/> with the specified message.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the <see cref="Option{TValue}"/>.</typeparam>
    /// <param name="option">The <see cref="Option{TValue}"/> instance to extract the value from.</param>
    /// <param name="message">The exception message to include if the <see cref="Option{TValue}"/> does not contain a value. Defaults to
    /// "Option has no value".</param>
    /// <returns>The value contained in the <see cref="Option{TValue}"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the <paramref name="option"/> does not contain a value.</exception>
    public static TValue ReduceOrThrow<TValue>(this Option<TValue> option, string message = "Option has no value") =>
        option.TryGetValue(out var value) ? value : throw new InvalidOperationException(message);

    /// <summary>
    /// Extracts the value from the specified Task of <see cref="Option{TValue}"/> if it contains a value; otherwise, throws an
    /// <see cref="InvalidOperationException"/> with the specified message.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the <see cref="Option{TValue}"/>.</typeparam>
    /// <param name="optionTask">The Task of <see cref="Option{TValue}"/> instance to extract the value from.</param>
    /// <param name="message">The exception message to include if the <see cref="Option{TValue}"/> does not contain a value. Defaults to
    /// "Option has no value".</param>
    /// <returns>The value contained in the <see cref="Option{TValue}"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the <paramref name="optionTask"/> does not contain a value.</exception>
    public static async Task<TValue> ReduceOrThrow<TValue>(this Task<Option<TValue>> optionTask, string message = "Option has no value") =>
        ReduceOrThrow(await optionTask, message);
}
