namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Provides extension methods for replacing the value of an <see cref="Option{TValue}"/> with an alternative value if it is empty.
/// </summary>
public static class OrElseExtensions
{
    /// <param name="option">this option to work on.</param>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.Reduce{TValue}(Option{TValue}, TValue)"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"/></returns>
        public Option<TValue> OrElse(TValue alternative) =>
            option.TryGetValue(out var value) ? value : alternative;

        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.Reduce{TValue}(Option{TValue}, Func{TValue})"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"/></returns>
        public Option<TValue> OrElse(Func<TValue> alternative) =>
            option.TryGetValue(out var value) ? value : alternative();

        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.ReduceAsync{TValue}(Option{TValue}, Func{Task{TValue}})"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"/></returns>
        public async Task<Option<TValue>> OrElseAsync(Func<Task<TValue>> alternative) =>
            option.TryGetValue(out var value) ? value : await alternative();
    }

    /// <param name="optionTask">The task that will result in the option to convert.</param>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.Reduce{TValue}(Task{Option{TValue}}, TValue)"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"/></returns>
        public async Task<Option<TValue>> OrElse(TValue alternative) =>
            OrElse(await optionTask, alternative);

        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.Reduce{TValue}(Task{Option{TValue}}, Func{TValue})"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"/></returns>
        public async Task<Option<TValue>> OrElse(Func<TValue> alternative) =>
            OrElse(await optionTask, alternative);

        /// <summary>
        /// Replace the value of the <see cref="Option{TValue}"/> with an alternative value if the option is empty.
        /// </summary>
        /// <remarks> Similar to <see cref="ReduceExtensions.ReduceAsync{TValue}(Task{Option{TValue}}, Func{Task{TValue}})"/>, but returns 
        /// a <see cref="Option{TValue}"/> rather then a <typeparamref name="TValue"/>.</remarks>
        /// <param name="alternative">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty; but still as <see cref="Option{TValue}"/></returns>
        public async Task<Option<TValue>> OrElseAsync(Func<Task<TValue>> alternative) =>
            await OrElseAsync(await optionTask, alternative);
    }
}
