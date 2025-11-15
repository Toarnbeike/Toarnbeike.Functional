namespace Toarnbeike.Eithers.Extensions;

/// <summary>
/// Provides extension methods for working with bi-directional maps (BiMap).
/// </summary>
public static class BiMapExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Projects the values of an <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping functions.
        /// </summary>
        /// <typeparam name="TLeft2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TLeft"/>.</typeparam>
        /// <typeparam name="TRight2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TRight"/>.</typeparam>
        /// <param name="mapL">A function to project the value of type <typeparamref name="TLeft"/> to a new value of type <typeparamref name="TLeft2"/>.</param>
        /// <param name="mapR">A function to project the value of type <typeparamref name="TRight"/> to a new value of type <typeparamref name="TRight2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft2, TRight2}"/> instance holding the projected value.</returns>
        public Either<TLeft2, TRight2> Map<TLeft2, TRight2>(Func<TLeft, TLeft2> mapL, Func<TRight, TRight2> mapR) =>
            either.IsLeft(out var left, out var right)
                ? Either<TLeft2, TRight2>.Left(mapL(left))
                : Either<TLeft2, TRight2>.Right(mapR(right));

        /// <summary>
        /// Projects the values of an <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping functions.
        /// </summary>
        /// <typeparam name="TLeft2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TLeft"/>.</typeparam>
        /// <typeparam name="TRight2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TRight"/>.</typeparam>
        /// <param name="mapLAsync">An async function to project the value of type <typeparamref name="TLeft"/> to a new value of type <typeparamref name="TLeft2"/>.</param>
        /// <param name="mapRAsync">An async function to project the value of type <typeparamref name="TRight"/> to a new value of type <typeparamref name="TRight2"/>.</param>
        public async Task<Either<TLeft2, TRight2>> MapAsync<TLeft2, TRight2>(Func<TLeft, Task<TLeft2>> mapLAsync, Func<TRight, Task<TRight2>> mapRAsync) =>
            either.IsLeft(out var left, out var right)
                ? Either<TLeft2, TRight2>.Left(await mapLAsync(left))
                : Either<TLeft2, TRight2>.Right(await mapRAsync(right));
    }

    extension<TLeft, TRight>(Task<Either<TLeft, TRight>> eitherTask)
    {
        /// <summary>
        /// Projects the values of an async <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping functions.
        /// </summary>
        /// <typeparam name="TLeft2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TLeft"/>.</typeparam>
        /// <typeparam name="TRight2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TRight"/>.</typeparam>
        /// <param name="mapL">A function to project the value of type <typeparamref name="TLeft"/> to a new value of type <typeparamref name="TLeft2"/>.</param>
        /// <param name="mapR">A function to project the value of type <typeparamref name="TRight"/> to a new value of type <typeparamref name="TRight2"/>.</param>
        /// <returns>A new <see cref="Either{TLeft2, TRight2}"/> instance holding the projected value.</returns>
        public async Task<Either<TLeft2, TRight2>> Map<TLeft2, TRight2>(Func<TLeft, TLeft2> mapL, Func<TRight, TRight2> mapR) =>
            (await eitherTask).Map(mapL, mapR);

        /// <summary>
        /// Projects the values of an async <see cref="Either{TLeft, TRight}"/> to new values using the provided mapping functions.
        /// </summary>
        /// <typeparam name="TLeft2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TLeft"/>.</typeparam>
        /// <typeparam name="TRight2">The type of the projected value when the original <see cref="Either{TLeft, TRight}"/> holds a value of type <typeparamref name="TRight"/>.</typeparam>
        /// <param name="mapLAsync">An async function to project the value of type <typeparamref name="TLeft"/> to a new value of type <typeparamref name="TLeft2"/>.</param>
        /// <param name="mapRAsync">An async function to project the value of type <typeparamref name="TRight"/> to a new value of type <typeparamref name="TRight2"/>.</param>
        public async Task<Either<TLeft2, TRight2>> MapAsync<TLeft2, TRight2>(Func<TLeft, Task<TLeft2>> mapLAsync, Func<TRight, Task<TRight2>> mapRAsync) =>
            await (await eitherTask).MapAsync(mapLAsync, mapRAsync);
    }
}