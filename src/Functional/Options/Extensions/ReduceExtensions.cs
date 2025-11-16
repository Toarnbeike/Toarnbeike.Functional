namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Provides extension methods for reducing an <see cref="Option{TValue}"/> to a value.
/// </summary>
public static class ReduceExtensions
{
    /// <param name="option">this option to work on.</param>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
        /// </summary>
        /// <remarks> Similar to <see cref="OrElseExtensions.OrElse{TValue}(Option{TValue}, TValue)"/>, but returns 
        /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
        /// <param name="orElse">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public TValue Reduce(TValue orElse) =>
            option.TryGetValue(out var value) ? value : orElse;

        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
        /// </summary>
        /// <remarks> Similar to <see cref="OrElseExtensions.OrElse{TValue}(Option{TValue}, Func{TValue})"/>, but returns 
        /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
        /// <param name="orElseFunction">The function to generate the value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public TValue Reduce(Func<TValue> orElseFunction) =>
            option.TryGetValue(out var value) ? value : orElseFunction();

        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
        /// </summary>
        /// <remarks> Similar to <see cref="OrElseExtensions.OrElseAsync{TValue}(Option{TValue}, Func{Task{TValue}})"/>, but returns 
        /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
        /// <param name="orElseTask">The task to generate the value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public async Task<TValue> ReduceAsync(Func<Task<TValue>> orElseTask) =>
            option.TryGetValue(out var value) ? value : await orElseTask();
    }

    /// <param name="optionTask">The task that will result in the option to convert.</param>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
        /// </summary>
        /// <remarks> Similar to <see cref="OrElseExtensions.OrElse{TValue}(Task{Option{TValue}}, TValue)"/>, but returns 
        /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
        /// <param name="orElse">The value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public async Task<TValue> Reduce(TValue orElse) =>
            Reduce(await optionTask, orElse);

        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
        /// </summary>
        /// <remarks> Similar to <see cref="OrElseExtensions.OrElse{TValue}(Task{Option{TValue}}, Func{TValue})"/>, but returns 
        /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
        /// <param name="orElseFunc">Function to calculate the value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public async Task<TValue> Reduce(Func<TValue> orElseFunc) =>
            Reduce(await optionTask, orElseFunc);

        /// <summary>
        /// Reduce this to the inner <typeparamref name="TValue"/> be either taking the value or using the provided value.
        /// </summary>
        /// <remarks> Similar to <see cref="OrElseExtensions.OrElseAsync{TValue}(Task{Option{TValue}}, Func{Task{TValue}})"/>, but returns 
        /// a <typeparamref name="TValue"/> rather then a <see cref="Option{TValue}"/>.</remarks>
        /// <param name="orElseTask">The task to generate the value to use if this is empty.</param>
        /// <returns>The value if provided, or the alternative if empty.</returns>
        public async Task<TValue> ReduceAsync(Func<Task<TValue>> orElseTask) =>
            await ReduceAsync(await optionTask, orElseTask);
    }
}
