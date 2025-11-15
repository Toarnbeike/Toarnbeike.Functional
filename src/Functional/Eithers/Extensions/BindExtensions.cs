namespace Toarnbeike.Eithers.Extensions;

/// <summary>
/// Provides extension methods for working bindings.
/// </summary>
public static class BindExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Projects the value of an <see cref="Either{TLeft, TRight}"/> to a new <see cref="Either{TLeft, TRight2}"/> by applying the provided binding function to the right value.
        /// </summary>
        /// <typeparam name="TRight2">The type of the resulting value when the binding function is applied to the right value of the original <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="bind">A function that takes the value of type <typeparamref name="TRight"/> and returns a new <see cref="Either{TLeft, TRight2}"/>.</param>
        /// <returns>
        /// A new <see cref="Either{TLeft, TRight2}"/> where:
        /// - If the original <see cref="Either{TLeft, TRight}"/> contains a left value, the returned <see cref="Either{TLeft, TRight2}"/> will also contain the same left value.
        /// - If the original <see cref="Either{TLeft, TRight}"/> contains a right value, the provided binding function is applied to produce the new value.
        /// </returns>
        public Either<TLeft, TRight2> Bind<TRight2>(Func<TRight, Either<TLeft, TRight2>> bind) =>
            either.IsLeft(out var left, out var right)
                ? Either<TLeft, TRight2>.Left(left)
                : bind(right);

        /// <summary>
        /// Projects the value of an <see cref="Either{TLeft, TRight}"/> to a new <see cref="Either{TLeft, TRight2}"/> by applying the provided binding function to the right value.
        /// </summary>
        /// <typeparam name="TRight2">The type of the resulting value when the binding function is applied to the right value of the original <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="bindAsync">An async function that takes the value of type <typeparamref name="TRight"/> and returns a new <see cref="Either{TLeft, TRight2}"/>.</param>
        /// <returns>
        /// A new <see cref="Either{TLeft, TRight2}"/> where:
        /// - If the original <see cref="Either{TLeft, TRight}"/> contains a left value, the returned <see cref="Either{TLeft, TRight2}"/> will also contain the same left value.
        /// - If the original <see cref="Either{TLeft, TRight}"/> contains a right value, the provided binding function is applied to produce the new value.
        /// </returns>
        public async Task<Either<TLeft, TRight2>> BindAsync<TRight2>(Func<TRight, Task<Either<TLeft, TRight2>>> bindAsync) =>
            either.IsLeft(out var left, out var right)
                ? Either<TLeft, TRight2>.Left(left)
                : await bindAsync(right);
    }

    extension<TLeft, TRight>(Task<Either<TLeft, TRight>> eitherTask)
    {
        /// <summary>
        /// Projects the value of an async <see cref="Either{TLeft, TRight}"/> to a new <see cref="Either{TLeft, TRight2}"/> by applying the provided binding function to the right value.
        /// </summary>
        /// <typeparam name="TRight2">The type of the resulting value when the binding function is applied to the right value of the original <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="bind">A function that takes the value of type <typeparamref name="TRight"/> and returns a new <see cref="Either{TLeft, TRight2}"/>.</param>
        /// <returns>
        /// A new <see cref="Either{TLeft, TRight2}"/> where:
        /// - If the original <see cref="Either{TLeft, TRight}"/> contains a left value, the returned <see cref="Either{TLeft, TRight2}"/> will also contain the same left value.
        /// - If the original <see cref="Either{TLeft, TRight}"/> contains a right value, the provided binding function is applied to produce the new value.
        /// </returns>
        public async Task<Either<TLeft, TRight2>> Bind<TRight2>(Func<TRight, Either<TLeft, TRight2>> bind) =>
            (await eitherTask).Bind(bind);

        /// <summary>
        /// Projects the value of an async <see cref="Either{TLeft, TRight}"/> to a new <see cref="Either{TLeft, TRight2}"/> by applying the provided binding function to the right value.
        /// </summary>
        /// <typeparam name="TRight2">The type of the resulting value when the binding function is applied to the right value of the original <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="bindAsync">An async function that takes the value of type <typeparamref name="TRight"/> and returns a new <see cref="Either{TLeft, TRight2}"/>.</param>
        /// <returns>
        /// A new <see cref="Either{TLeft, TRight2}"/> where:
        /// - If the original <see cref="Either{TLeft, TRight}"/> contains a left value, the returned <see cref="Either{TLeft, TRight2}"/> will also contain the same left value.
        /// - If the original <see cref="Either{TLeft, TRight}"/> contains a right value, the provided binding function is applied to produce the new value.
        /// </returns>
        public async Task<Either<TLeft, TRight2>> BindAsync<TRight2>(Func<TRight, Task<Either<TLeft, TRight2>>> bindAsync) =>
            await (await eitherTask).BindAsync(bindAsync);
    }
}