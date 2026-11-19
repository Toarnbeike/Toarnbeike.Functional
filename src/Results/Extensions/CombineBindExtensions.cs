namespace Toarnbeike.Results.Extensions;

/// <summary>
/// CombineBind: Combine the pipelines of two instances of <see cref="Result{TIn}" />
/// and bind the produced Result to a new <see cref="Result{TOut}"/> using a function.
/// </summary>
public static class CombineBindExtensions
{
    /// <param name="first"> The first result to combine.</param>
    extension<T1>(Result<T1> first) where T1 : notnull
    {
        /// <summary>
        /// Maps the values of two successful <see cref="Result{T}"/> instances to a new <see cref="Result{TOut}"/>>
        /// using the provided mapping function.
        /// </summary>
        /// <typeparam name="T2">The type of the second result value to combine.</typeparam>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="second">The second result to combine with this result.</param>
        /// <param name="bind">
        /// A function to apply to the values if both results are successful. 
        /// If any result is a failure, the failure is preserved and <paramref name="bind"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed values if both input results were successful,
        /// or the original failure otherwise.
        /// </returns>
        public Result<TOut> CombineBind<T2, TOut>(Result<T2> second, Func<T1, T2, Result<TOut>> bind) where T2 : notnull where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bind);

            return (first, second) switch
            {
                (T1 value1, T2 value2) => bind(value1, value2),
                (T1, Failure failure) => failure,
                (Failure failure, _) => failure
            };
        }

        /// <summary>
        /// Maps the values of two successful <see cref="Result{T}"/> instances to a new <see cref="Result{TOut}"/>>
        /// using the provided mapping function.
        /// </summary>
        /// <typeparam name="T2">The type of the second result value to combine.</typeparam>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="second">The second result to combine with this result.</param>
        /// <param name="bind">
        /// A function to apply to the values if both results are successful. 
        /// If any result is a failure, the failure is preserved and <paramref name="bind"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed values if both input results were successful,
        /// or the original failure otherwise.
        /// </returns>
        public async Task<Result<TOut>> CombineBindAsync<T2, TOut>(Result<T2> second, Func<T1, T2, Task<Result<TOut>>> bind) where T2 : notnull where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bind);

            return (first, second) switch
            {
                (T1 value1, T2 value2) => await bind(value1, value2).ConfigureAwait(false),
                (T1, Failure failure) => failure,
                (Failure failure, _) => failure
            };
        }
    }

    /// <param name="firstTask"> The first result to combine.</param>
    extension<T1>(Task<Result<T1>> firstTask) where T1 : notnull
    {
        /// <summary>
        /// Maps the values of two successful <see cref="Result{T}"/> instances to a new <see cref="Result{TOut}"/>>
        /// using the provided mapping function.
        /// </summary>
        /// <typeparam name="T2">The type of the second result value to combine.</typeparam>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="secondTask">The other result Task to combine with this result.</param>
        /// <param name="bind">
        /// A function to apply to the values if both results are successful. 
        /// If any result is a failure, the failure is preserved and <paramref name="bind"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed values if both input results were successful,
        /// or the original failure otherwise.
        /// </returns>
        public async Task<Result<TOut>> CombineBind<T2, TOut>(Task<Result<T2>> secondTask, Func<T1, T2, Result<TOut>> bind) where T2 : notnull where TOut : notnull
        {
            var result = await firstTask.ConfigureAwait(false);
            var second = await secondTask.ConfigureAwait(false);
            return result.CombineBind(second, bind);
        }

        /// <summary>
        /// Maps the values of two successful <see cref="Result{T}"/> instances to a new <see cref="Result{TOut}"/>>
        /// using the provided mapping function.
        /// </summary>
        /// <typeparam name="T2">The type of the second result value to combine.</typeparam>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="secondTask">The other result Task to combine with this result.</param>
        /// <param name="bind">
        /// A function to apply to the values if both results are successful. 
        /// If any result is a failure, the failure is preserved and <paramref name="bind"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed values if both input results were successful,
        /// or the original failure otherwise.
        /// </returns>
        public async Task<Result<TOut>> CombineBindAsync<T2, TOut>(Task<Result<T2>> secondTask, Func<T1, T2, Task<Result<TOut>>> bind) where T2 : notnull where TOut : notnull
        {
            var result = await firstTask.ConfigureAwait(false);
            var second = await secondTask.ConfigureAwait(false);
            return await result.CombineBindAsync(second, bind);
        }
    }
}