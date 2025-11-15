namespace Toarnbeike.Eithers.Extensions.Unsafe;

public static class GetOrThrowExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Retrieves the right value of the Either instance if it contains a right value;
        /// otherwise, throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>The right value of the Either instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the Either instance does not contain a right value.</exception>
        public TRight GetOrThrow() =>
            either.GetOrThrow(() => new InvalidOperationException("Either does not contain a right value."));

        /// <summary>
        /// Retrieves the right value of the Either instance if it contains a right value;
        /// otherwise, throws the exception provided by the specified <paramref name="onThrow"/> function.
        /// </summary>
        /// <param name="onThrow">A function that produces the exception to be thrown if the Either instance does not contain a right value.</param>
        /// <returns>The right value of the Either instance.</returns>
        /// <exception cref="Exception">
        /// Thrown with the exception provided by <paramref name="onThrow"/> when the Either instance does not contain a right value.
        /// </exception>
        public TRight GetOrThrow(Func<Exception> onThrow)
        {
            return either.IsRight(out var right)
                ? right
                : throw onThrow();
        }
    }

    extension<TLeft, TRight>(Task<Either<TLeft, TRight>> eitherTask)
    {
        /// <summary>
        /// Retrieves the right value of the async Either instance if it contains a right value;
        /// otherwise, throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>The right value of the Either instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the Either instance does not contain a right value.</exception>
        public async Task<TRight> GetOrThrowAsync() =>
            (await eitherTask).GetOrThrow();

        /// <summary>
        /// Retrieves the right value of the async Either instance if it contains a right value;
        /// otherwise, throws the exception provided by the specified <paramref name="onThrow"/> function.
        /// </summary>
        /// <param name="onThrow">A function that produces the exception to be thrown if the Either instance does not contain a right value.</param>
        /// <returns>The right value of the Either instance.</returns>
        /// <exception cref="Exception">
        /// Thrown with the exception provided by <paramref name="onThrow"/> when the Either instance does not contain a right value.
        /// </exception>
        public async Task<TRight> GetOrThrowAsync(Func<Exception> onThrow) =>
            (await eitherTask).GetOrThrow(onThrow);
    }
}