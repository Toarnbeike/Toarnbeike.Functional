namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Provides extension methods for matching on <see cref="Option{TValue}"/>.
/// </summary>
public static class MatchExtensions
{
    /// <param name="option">this option to work on.</param>
    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    extension<TIn>(Option<TIn> option)
    {
        /// <summary>
        /// Match the method to generate the <typeparamref name="TOut"/> depending on whether this option has a value or not.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="whenSome">Function to generate the <typeparamref name="TOut"/> when this option has a value.</param>
        /// <param name="whenNone">Function to generate the <typeparamref name="TOut"/> when this option has no value.</param>
        /// <returns>An instance of <typeparamref name="TOut"/> created depending on the status of the option.</returns>
        public TOut Match<TOut>(Func<TIn, TOut> whenSome, Func<TOut> whenNone) =>
            option.TryGetValue(out var value) ? whenSome(value) : whenNone();

        /// <summary>
        /// Match the method to generate the <typeparamref name="TOut"/> depending on whether this option has a value or not.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="whenSome">Function to generate the <typeparamref name="TOut"/> when this option has a value.</param>
        /// <param name="whenNone">Function to generate the <typeparamref name="TOut"/> when this option has no value.</param>
        /// <returns>A Task of an instance of <typeparamref name="TOut"/> created depending on the status of the option.</returns>
        public async Task<TOut> MatchAsync<TOut>(Func<TIn, Task<TOut>> whenSome, Func<Task<TOut>> whenNone) =>
            option.TryGetValue(out var value) ? await whenSome(value) : await whenNone();
    }

    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    extension<TIn>(Task<Option<TIn>> optionTask)
    {
        /// <summary>
        /// Match the method to generate the <typeparamref name="TOut"/> depending on whether this option has a value or not.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="whenSome">Function to generate the <typeparamref name="TOut"/> when this option has a value.</param>
        /// <param name="whenNone">Function to generate the <typeparamref name="TOut"/> when this option has no value.</param>
        /// <returns>A Task of an instance of <typeparamref name="TOut"/> created depending on the status of the option.</returns>
        public async Task<TOut> Match<TOut>(Func<TIn, TOut> whenSome, Func<TOut> whenNone) =>
            Match(await optionTask, whenSome, whenNone);

        /// <summary>
        /// Match the method to generate the <typeparamref name="TOut"/> depending on whether this option has a value or not.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="whenSome">Function to generate the <typeparamref name="TOut"/> when this option has a value.</param>
        /// <param name="whenNone">Function to generate the <typeparamref name="TOut"/> when this option has no value.</param>
        /// <returns>A Task of an instance of <typeparamref name="TOut"/> created depending on the status of the option.</returns>
        public async Task<TOut> MatchAsync<TOut>(Func<TIn, Task<TOut>> whenSome, Func<Task<TOut>> whenNone) =>
            await MatchAsync(await optionTask, whenSome, whenNone);
    }
}
