namespace Toarnbeike.Options.Extensions;

public static class MapExtensions
{
    /// <param name="option">The option this method is applied to.</param>
    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    extension<TIn>(Option<TIn> option) where TIn : notnull
    {
        /// <summary>
        /// Map the option of <typeparamref name="TIn"/> to an option of <typeparamref name="TOut"/> by providing a value selector function.
        /// If the selector returns<c>null</c>, the result will be <see cref = "Option{TValue}.None" />.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selector">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>An option of type <typeparamref name="TOut"/> that has a value depending on the original value and the result of the selector.</returns>
        public Option<TOut> Map<TOut>(Func<TIn, TOut> selector) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(selector);
            return option switch
            {
                TIn some => selector(some),
                None => Option.None
            };
        }

        /// <summary>
        /// Map the option of <typeparamref name="TIn"/> to an option of <typeparamref name="TOut"/> by providing a value selector function.
        /// If the selector returns<c>null</c>, the result will be <see cref = "Option{TValue}.None" />.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selectorTask">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>An option of type <typeparamref name="TOut"/> that has a value depending on the original value and the result of the selector.</returns>
        public async Task<Option<TOut>> MapAsync<TOut>(Func<TIn, Task<TOut>> selectorTask) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(selectorTask);
            return option switch
            {
                TIn some => await selectorTask(some).ConfigureAwait(false),
                None => Option.None
            };
        }
    }

    /// <param name="optionTask">The task that will result in the option to convert.</param>
    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    extension<TIn>(Task<Option<TIn>> optionTask) where TIn : notnull
    {
        /// <summary>
        /// Map the option of <typeparamref name="TIn"/> to an option of <typeparamref name="TOut"/> by providing a value selector function.
        /// If the selector returns<c>null</c>, the result will be <see cref = "Option{TValue}.None" />.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selector">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>A <see cref="Task"/>{<see cref="Option"/>{<typeparamref name="TOut"/>}} that has a value depending on the original value and the result of the selector.</returns>
        public async Task<Option<TOut>> Map<TOut>(Func<TIn, TOut> selector) where TOut : notnull
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Map(selector);
        }

        /// <summary>
        /// Map the option of <typeparamref name="TIn"/> to an option of <typeparamref name="TOut"/> by providing a value selector function.
        /// If the selector returns<c>null</c>, the result will be <see cref = "Option{TValue}.None" />.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selectorTask">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>An option of type <typeparamref name="TOut"/> that has a value depending on the original value and the result of the selector.</returns>
        public async Task<Option<TOut>> MapAsync<TOut>(Func<TIn, Task<TOut>> selectorTask) where TOut : notnull
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.MapAsync(selectorTask).ConfigureAwait(false);
        }
    }
}
