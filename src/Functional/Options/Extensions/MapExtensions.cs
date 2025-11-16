namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Provides extension methods for mapping an <see cref="Option{Tin}"/> to an <see cref="Option{TOut}"/>.
/// </summary>
public static class MapExtensions
{
    /// <param name="option">The option this method is applied to.</param>
    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    extension<TIn>(Option<TIn> option)
    {
        /// <summary>
        /// Map the option of <typeparamref name="TIn"/> to an option of <typeparamref name="TOut"/> by providing a value selector function.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selector">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>An option of type <typeparamref name="TOut"/> that has a value depending on the original value and the result of the selector.</returns>
        public Option<TOut> Map<TOut>(Func<TIn, TOut?> selector) =>
            option.TryGetValue(out var value) ? selector(value).AsOption() : Option.None;

        /// <summary>
        /// Map the option of <typeparamref name="TIn"/> to an option of <typeparamref name="TOut"/> by providing a value selector function.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selectorTask">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>An option of type <typeparamref name="TOut"/> that has a value depending on the original value and the result of the selector.</returns>
        public async Task<Option<TOut>> MapAsync<TOut>(Func<TIn, Task<TOut?>> selectorTask) =>
            option.TryGetValue(out var value) ? await selectorTask(value).AsOption() : Option.None;
    }

    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    extension<TIn>(Task<Option<TIn>> optionTask)
    {
        /// <summary>
        /// Map the option of <typeparamref name="TIn"/> to an option of <typeparamref name="TOut"/> by providing a value selector function.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selector">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>A <see cref="Task"/>{<see cref="Option"/>{<typeparamref name="TOut"/>}} that has a value depending on the original value and the result of the selector.</returns>
        public async Task<Option<TOut>> Map<TOut>(Func<TIn, TOut?> selector) =>
            Map(await optionTask, selector);

        /// <summary>
        /// Map the option of <typeparamref name="TIn"/> to an option of <typeparamref name="TOut"/> by providing a value selector function.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selectorTask">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>An option of type <typeparamref name="TOut"/> that has a value depending on the original value and the result of the selector.</returns>
        public async Task<Option<TOut>> MapAsync<TOut>(Func<TIn, Task<TOut?>> selectorTask) =>
            await MapAsync(await optionTask, selectorTask);
    }
}
