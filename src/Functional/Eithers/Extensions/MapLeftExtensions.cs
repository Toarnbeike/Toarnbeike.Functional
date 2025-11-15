namespace Toarnbeike.Eithers.Extensions;

/// <summary>
/// Provides extension methods for working with left-hand maps.
/// </summary>
public static class MapLeftExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Projects the left value of an <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping function.
        /// </summary>
        /// <typeparam name="TLeft2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TLeft"/>.</typeparam>
        /// <param name="map">A function to project the value of type <typeparamref name="TLeft"/> to a new value of type <typeparamref name="TLeft2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft2, TRight}"/> instance holding the projected value.</returns>
        public Either<TLeft2, TRight> MapLeft<TLeft2>(Func<TLeft, TLeft2> map) =>
            either.IsLeft(out var left, out var right)
                ? Either<TLeft2, TRight>.Left(map(left))
                : Either<TLeft2, TRight>.Right(right);

        /// <summary>
        /// Projects the left value of an <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping function.
        /// </summary>
        /// <typeparam name="TLeft2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TLeft"/>.</typeparam>
        /// <param name="mapAsync">An async function to project the value of type <typeparamref name="TLeft"/> to a new value of type <typeparamref name="TLeft2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft2, TRight}"/> instance holding the projected value.</returns>
        public async Task<Either<TLeft2, TRight>> MapLeftAsync<TLeft2>(Func<TLeft, Task<TLeft2>> mapAsync) =>
            either.IsLeft(out var left, out var right)
                ? Either<TLeft2, TRight>.Left(await mapAsync(left))
                : Either<TLeft2, TRight>.Right(right);
    }

    extension<TLeft, TRight>(Task<Either<TLeft, TRight>> eitherTask)
    {
        /// <summary>
        /// Projects the left value of an async <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping function.
        /// </summary>
        /// <typeparam name="TLeft2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TLeft"/>.</typeparam>
        /// <param name="map">A function to project the value of type <typeparamref name="TLeft"/> to a new value of type <typeparamref name="TLeft2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft2, TRight}"/> instance holding the projected value.</returns>
        public async Task<Either<TLeft2, TRight>> MapLeft<TLeft2>(Func<TLeft, TLeft2> map) =>
            (await eitherTask).MapLeft(map);

        /// <summary>
        /// Projects the left value of an async <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping function.
        /// </summary>
        /// <typeparam name="TLeft2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TLeft"/>.</typeparam>
        /// <param name="mapAsync">An async function to project the value of type <typeparamref name="TLeft"/> to a new value of type <typeparamref name="TLeft2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft2, TRight}"/> instance holding the projected value.</returns>
        public async Task<Either<TLeft2, TRight>> MapLeftAsync<TLeft2>(Func<TLeft, Task<TLeft2>> mapAsync) =>
            await (await eitherTask).MapLeftAsync(mapAsync);
    }
}