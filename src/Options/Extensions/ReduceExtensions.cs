namespace Toarnbeike.Options.Extensions;

public static class ReduceExtensions
{
    /// <param name="option">this option to work on.</param>
    extension<TValue>(Option<TValue> option) where TValue : notnull
    {
        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> by either taking the value or using the provided value.
        /// </summary>
        /// <param name="orElse">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public TValue Reduce(TValue orElse) =>
            option switch
            {
                TValue some => some,
                None => orElse
            };

        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> by either taking the value or using the provided value.
        /// </summary>
        /// <param name="orElseFunction">The function to generate the value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public TValue Reduce(Func<TValue> orElseFunction)
        {
            ArgumentNullException.ThrowIfNull(orElseFunction);
            return option switch
            {
                TValue some => some,
                None => orElseFunction()
            };
        }

        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> by either taking the value or using the provided value.
        /// </summary>
        /// <param name="orElseTask">The task to generate the value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public async Task<TValue> ReduceAsync(Func<Task<TValue>> orElseTask)
        {
            ArgumentNullException.ThrowIfNull(orElseTask);
            return option switch
            {
                TValue some => some,
                None => await orElseTask().ConfigureAwait(false)
            };
        }
    }

    /// <param name="optionTask">The task that will result in the option to convert.</param>
    extension<TValue>(Task<Option<TValue>> optionTask) where TValue : notnull
    {
        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> by either taking the value or using the provided value.
        /// </summary>
        /// <param name="orElse">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public async Task<TValue> Reduce(TValue orElse)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Reduce(orElse);
        }

        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> by either taking the value or using the provided value.
        /// </summary>
        /// <param name="orElseFunc">Function to calculate the value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public async Task<TValue> Reduce(Func<TValue> orElseFunc)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Reduce(orElseFunc);
        }

        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> by either taking the value or using the provided value.
        /// </summary>
        /// <param name="orElseTask">The task to generate the value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public async Task<TValue> ReduceAsync(Func<Task<TValue>> orElseTask)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.ReduceAsync(orElseTask).ConfigureAwait(false);
        }
    }
}