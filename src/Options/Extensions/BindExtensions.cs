namespace Toarnbeike.Options.Extensions;

public static class BindExtensions
{
    /// <param name="option">The option this method is applied to.</param>
    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    extension<TIn>(Option<TIn> option) where TIn : notnull
    {
        /// <summary>
        /// Bind the <see cref="Option"/>{<typeparamref name="TIn"/>} to an <see cref="Option"/>{<typeparamref name="TOut"/>}
        /// Applies the specified selector only if the option contains a value.
        /// If the option is None, the result is None.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selector">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/>.</param>
        /// <returns>An option of type <typeparamref name="TOut"/> that has a value depending on the original value and the result of the selector.</returns>
        public Option<TOut> Bind<TOut>(Func<TIn, Option<TOut>> selector) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(selector);
            return option switch
            {
                TIn some => selector(some),
                None => Option.None
            };
        }

        /// <summary>
        /// Bind the <see cref="Option"/>{<typeparamref name="TIn"/>} to an <see cref="Option"/>{<typeparamref name="TOut"/>}
        /// Applies the specified selector only if the option contains a value.
        /// If the option is None, the result is None.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selectorTask">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>An option of type <typeparamref name="TOut"/> that has a value depending on the original value and the result of the selector.</returns>
        public async Task<Option<TOut>> BindAsync<TOut>(Func<TIn, Task<Option<TOut>>> selectorTask) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(selectorTask);
            return option switch
            {
                TIn some => await selectorTask(some).ConfigureAwait(false),
                None => Option.None
            };
        }
    }

    /// <typeparam name="TIn">The type of the original optional value.</typeparam>
    /// <param name="optionTask">The task that will result in the option to convert.</param>
    extension<TIn>(Task<Option<TIn>> optionTask) where TIn : notnull
    {
        /// <summary>
        /// Bind the <see cref="Option"/>{<typeparamref name="TIn"/>} to an <see cref="Option"/>{<typeparamref name="TOut"/>}
        /// Applies the specified selector only if the option contains a value.
        /// If the option is None, the result is None.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selector">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>A <see cref="Task"/>{<see cref="Option"/>{<typeparamref name="TOut"/>}} that has a value depending on the original value and the result of the selector.</returns>
        public async Task<Option<TOut>> Bind<TOut>(Func<TIn, Option<TOut>> selector) where TOut : notnull
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Bind(selector);
        }

        /// <summary>
        /// Bind the <see cref="Option"/>{<typeparamref name="TIn"/>} to an <see cref="Option"/>{<typeparamref name="TOut"/>}
        /// Applies the specified selector only if the option contains a value.
        /// If the option is None, the result is None.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting optional value.</typeparam>
        /// <param name="selectorTask">The function to convert from <typeparamref name="TIn"/> to <typeparamref name="TOut"/></param>
        /// <returns>An option of type <typeparamref name="TOut"/> that has a value depending on the original value and the result of the selector.</returns>
        public async Task<Option<TOut>> BindAsync<TOut>(Func<TIn, Task<Option<TOut>>> selectorTask) where TOut : notnull
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.BindAsync(selectorTask).ConfigureAwait(false);
        }
    }
}