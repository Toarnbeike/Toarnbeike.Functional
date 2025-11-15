namespace Toarnbeike.Eithers.Extensions.Unsafe;

public static class GetLeftOrThrowExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Retrieves the left value of the Either instance if it contains a left value;
        /// otherwise, throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>The left value of the Either instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the Either instance does not contain a left value.</exception>
        public TLeft GetLeftOrThrow() =>
            either.GetLeftOrThrow(() => new InvalidOperationException("Either does not contain a left value."));

        /// <summary>
        /// Retrieves the left value of the Either instance if it contains a left value;
        /// otherwise, throws an exception provided by the specified <paramref name="onThrow"/> function.
        /// </summary>
        /// <param name="onThrow">A function that returns the exception to throw if the Either instance does not contain a left value.</param>
        /// <returns>The left value of the Either instance.</returns>
        /// <exception cref="Exception">The exception returned by <paramref name="onThrow"/> if the Either instance does not contain a left value.</exception>
        public TLeft GetLeftOrThrow(Func<Exception> onThrow)
        {
            return either.IsLeft(out var left)
                ? left
                : throw onThrow();
        }
    }

    extension<TLeft, TRight>(Task<Either<TLeft, TRight>> eitherTask)
    {
        /// <summary>
        /// Retrieves the left value of the async Either instance if it contains a left value;
        /// otherwise, throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>The left value of the Either instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the Either instance does not contain a left value.</exception>
        public async Task<TLeft> GetLeftOrThrowAsync() =>
            (await eitherTask).GetLeftOrThrow();

        /// <summary>
        /// Retrieves the left value of the async Either instance if it contains a left value;
        /// otherwise, throws an exception provided by the specified <paramref name="onThrow"/> function.
        /// </summary>
        /// <param name="onThrow">A function that returns the exception to throw if the Either instance does not contain a left value.</param>
        /// <returns>The left value of the Either instance.</returns>
        /// <exception cref="Exception">The exception returned by <paramref name="onThrow"/> if the Either instance does not contain a left value.</exception>
        public async Task<TLeft> GetLeftOrThrowAsync(Func<Exception> onThrow) =>
            (await eitherTask).GetLeftOrThrow(onThrow);
    }
}