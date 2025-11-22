namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Zip: Combine the values of two successful <see cref="Result{TValue}"/> into a tuple result.
/// </summary>
public static class ZipExtensions
{
    /// <param name="first">The first result.</param>
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    extension<T1>(Result<T1> first)
    {
        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <param name="second">The function to generate the second result from the first's result's value.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        public Result<(T1, T2)> Zip<T2>(Func<T1, Result<T2>> second)
        {
            if (!first.TryGetContents(out var firstValue, out var firstFailure))
            {
                return Result<(T1, T2)>.Failure(firstFailure);
            }

            var secondResult = second(firstValue);
            return secondResult.TryGetContents(out var secondValue, out var secondFailure) 
                ? (firstValue, secondValue) 
                : Result<(T1, T2)>.Failure(secondFailure);
        }

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <typeparam name="TResult">The type of the resulting tuple that is named using the <paramref name="projector"/></typeparam>
        /// <param name="second">The function to generate the second result from the first's result's value.</param>
        /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        public Result<TResult> Zip<T2, TResult>(Func<T1, Result<T2>> second, Func<T1, T2, TResult> projector)
        {
            if (!first.TryGetContents(out var firstValue, out var firstFailure))
            {
                return Result<TResult>.Failure(firstFailure);
            }

            var secondResult = second(firstValue);
            return secondResult.TryGetContents(out var secondValue, out var secondFailure) 
                ? projector(firstValue, secondValue) 
                : Result<TResult>.Failure(secondFailure);
        }

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        public async Task<Result<(T1, T2)>> ZipAsync<T2>(Func<T1, Task<Result<T2>>> secondTask)
        {
            if (!first.TryGetContents(out var firstValue, out var firstFailure))
            {
                return Result<(T1, T2)>.Failure(firstFailure);
            }

            var secondResult = await secondTask(firstValue).ConfigureAwait(false);
            return secondResult.TryGetContents(out var secondValue, out var secondFailure) 
                ? (firstValue, secondValue) 
                : Result<(T1, T2)>.Failure(secondFailure);
        }

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <typeparam name="TResult">The type of the resulting tuple that is named using the <paramref name="projector"/></typeparam>
        /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
        /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        public async Task<Result<TResult>> ZipAsync<T2, TResult>(Func<T1, Task<Result<T2>>> secondTask, Func<T1, T2, TResult> projector)
        {
            if (!first.TryGetContents(out var firstValue, out var firstFailure))
            {
                return Result<TResult>.Failure(firstFailure);
            }

            var secondResult = await secondTask(firstValue).ConfigureAwait(false);
            if (!secondResult.TryGetContents(out var secondValue, out var secondFailure))
            {
                return Result<TResult>.Failure(secondFailure);
            }

            return projector(firstValue, secondValue);
        }
    }

    /// <param name="firstTask">The first async result.</param>
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    extension<T1>(Task<Result<T1>> firstTask)
    {
        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <param name="second">The function to generate the second result from the first's result's value.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        public async Task<Result<(T1, T2)>> Zip<T2>(Func<T1, Result<T2>> second) => 
            Zip(await firstTask.ConfigureAwait(false), second);

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <typeparam name="TResult">The type of the resulting tuple that is named using the <paramref name="projector"/></typeparam>
        /// <param name="second">The function to generate the second result from the first's result's value.</param>
        /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        public async Task<Result<TResult>> Zip<T2, TResult>(Func<T1, Result<T2>> second, Func<T1, T2, TResult> projector) => 
            Zip(await firstTask.ConfigureAwait(false), second, projector);

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        public async Task<Result<(T1, T2)>> ZipAsync<T2>(Func<T1, Task<Result<T2>>> secondTask) => 
            await ZipAsync(await firstTask.ConfigureAwait(false), secondTask).ConfigureAwait(false);

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <typeparam name="TResult">The type of the resulting tuple that is named using the <paramref name="projector"/></typeparam>
        /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
        /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        public async Task<Result<TResult>> ZipAsync<T2, TResult>(Func<T1, Task<Result<T2>>> secondTask, Func<T1, T2, TResult> projector) => 
            await ZipAsync(await firstTask.ConfigureAwait(false), secondTask, projector).ConfigureAwait(false);
    }
}