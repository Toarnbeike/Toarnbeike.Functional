namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Map: Map the successful value of a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/> by 
/// providing a mapping function: <see cref="Func{TIn, TOut}"/>.
/// </summary>
public static class MapExtensions
{
    /// <param name="result">The result to convert.</param>
    extension(Result result)
    {
        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="value">The value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public Result<TValue> Map<TValue>(TValue value) where TValue : notnull => result switch
        {
            Success => value,
            Failure failure => failure,
        };


        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="valueFunc">Func to generate the value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public Result<TValue> Map<TValue>(Func<TValue> valueFunc) where TValue : notnull
        {
            ArgumentNullException.ThrowIfNull(valueFunc);

            return result switch
            {
                Success => valueFunc(),
                Failure failure => failure,
            };
        }

        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="valueFunc">Async func to generate the value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public async Task<Result<TValue>> MapAsync<TValue>(Func<Task<TValue>> valueFunc) where TValue : notnull
        {
            ArgumentNullException.ThrowIfNull(valueFunc);

            return result switch
            {
                Success => await valueFunc().ConfigureAwait(false),
                Failure failure => failure,
            };
        }
    }

    /// <param name="resultTask">The async result to convert.</param>
    extension(Task<Result> resultTask)
    {
        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="value">The value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public async Task<Result<TValue>> Map<TValue>(TValue value) where TValue : notnull
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Map(value);
        }

        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="valueFunc">Func to generate the value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public async Task<Result<TValue>> Map<TValue>(Func<TValue> valueFunc) where TValue : notnull
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Map(valueFunc);
        }

        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="valueFunc">Async func to generate the value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public async Task<Result<TValue>> MapAsync<TValue>(Func<Task<TValue>> valueFunc) where TValue : notnull
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.MapAsync(valueFunc).ConfigureAwait(false);
        }
    }

    /// <param name="result">The input result to transform.</param>
    /// <typeparam name="TIn">The type of the value in the input result.</typeparam>
    extension<TIn>(Result<TIn> result) where TIn : notnull
    {
        /// <summary>
        /// Maps the value of a successful <see cref="Result{TIn}"/> to a new <see cref="Result{TOut}"/> 
        /// using the specified mapping function.
        /// </summary>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="map">
        /// A function to apply to the value if the result is successful. 
        /// If the result is a failure, the failure is preserved and <paramref name="map"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containi  ng the transformed value if the input result was successful,
        /// or the original failure otherwise.
        /// </returns>
        public Result<TOut> Map<TOut>(Func<TIn, TOut> map) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(map);

            return result switch
            {
                TIn value => map(value),
                Failure failure => failure,
            };
        }

        /// <summary>
        /// Maps the value of a successful <see cref="Result{TIn}"/> to a new <see cref="Result{TOut}"/> 
        /// using the specified async mapping function.
        /// </summary>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="map">
        /// An async function to apply to the value if the result is successful. 
        /// If the result is a failure, the failure is preserved and <paramref name="map"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed value if the input result was successful,
        /// or the original failure otherwise.
        /// </returns>
        public async Task<Result<TOut>> MapAsync<TOut>(Func<TIn, Task<TOut>> map) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(map);

            return result switch
            {
                TIn value => await map(value).ConfigureAwait(false),
                Failure failure => failure
            };
        }
    }

    /// <param name="resultTask">The input result task to transform.</param>
    /// <typeparam name="TIn">The type of the value in the input result.</typeparam>
    extension<TIn>(Task<Result<TIn>> resultTask) where TIn : notnull
    {
        /// <summary>
        /// Maps the value of a successful <see cref="Task{Result{TIn}}"/> to a new <see cref="Result{TOut}"/> 
        /// using the specified mapping function.
        /// </summary>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="map">
        /// A function to apply to the value if the result is successful. 
        /// If the result is a failure, the failure is preserved and <paramref name="map"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed value if the input result was successful,
        /// or the original failure otherwise.
        /// </returns>
        public async Task<Result<TOut>> Map<TOut>(Func<TIn, TOut> map) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(map);

            var result = await resultTask.ConfigureAwait(false);
            return result.Map(map);
        }

        /// <summary>
        /// Maps the value of a successful <see cref="Task{Result{TIn}}"/> to a new <see cref="Result{TOut}"/> 
        /// using the specified async mapping function.
        /// </summary>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="map">
        /// An async function to apply to the value if the result is successful. 
        /// If the result is a failure, the failure is preserved and <paramref name="map"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed value if the input result was successful,
        /// or the original failure otherwise.
        /// </returns>
        public async Task<Result<TOut>> MapAsync<TOut>(Func<TIn, Task<TOut>> map) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(map);

            var result = await resultTask.ConfigureAwait(false);
            return await result.MapAsync(map).ConfigureAwait(false);
        }
    }
}