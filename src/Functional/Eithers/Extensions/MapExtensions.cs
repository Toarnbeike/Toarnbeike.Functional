namespace Toarnbeike.Eithers.Extensions;

/// <summary>
/// Provides extension methods for working with right-hand maps.
/// </summary>
public static class MapExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Projects the right value of an <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping function.
        /// </summary>
        /// <typeparam name="TRight2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TRight"/>.</typeparam>
        /// <param name="map">A function to project the value of type <typeparamref name="TRight"/> to a new value of type <typeparamref name="TRight2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft, TRight2}"/> instance holding the projected value.</returns>
        public Either<TLeft, TRight2> Map<TRight2>(Func<TRight, TRight2> map) =>
            either.IsLeft(out var left, out var right)
                ? Either<TLeft, TRight2>.Left(left)
                : Either<TLeft, TRight2>.Right(map(right));

        /// <summary>
        /// Projects the right value of an <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping function.
        /// </summary>
        /// <typeparam name="TRight2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TRight"/>.</typeparam>
        /// <param name="mapAsync">An async function to project the value of type <typeparamref name="TRight"/> to a new value of type <typeparamref name="TRight2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft, TRight2}"/> instance holding the projected value.</returns>
        public async Task<Either<TLeft, TRight2>> MapAsync<TRight2>(Func<TRight, Task<TRight2>> mapAsync) =>
            either.IsLeft(out var left, out var right)
                ? Either<TLeft, TRight2>.Left(left)
                : Either<TLeft, TRight2>.Right(await mapAsync(right));
    }

    extension<TLeft, TRight>(Task<Either<TLeft, TRight>> eitherTask)
    {
        /// <summary>
        /// Projects the right value of an async <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping function.
        /// </summary>
        /// <typeparam name="TRight2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TRight"/>.</typeparam>
        /// <param name="map">A function to project the value of type <typeparamref name="TRight"/> to a new value of type <typeparamref name="TRight2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft, TRight2}"/> instance holding the projected value.</returns>
        public async Task<Either<TLeft, TRight2>> Map<TRight2>(Func<TRight, TRight2> map) =>
            (await eitherTask).Map(map);

        /// <summary>
        /// Projects the right value of an async <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping function.
        /// </summary>
        /// <typeparam name="TRight2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TRight"/>.</typeparam>
        /// <param name="mapAsync">An async function to project the value of type <typeparamref name="TRight"/> to a new value of type <typeparamref name="TRight2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft, TRight2}"/> instance holding the projected value.</returns>
        public async Task<Either<TLeft, TRight2>> MapAsync<TRight2>(Func<TRight, Task<TRight2>> mapAsync) =>
            await (await eitherTask).MapAsync(mapAsync);
    }
}

