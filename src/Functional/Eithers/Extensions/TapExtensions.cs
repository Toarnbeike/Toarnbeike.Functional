namespace Toarnbeike.Eithers.Extensions;

/// <summary>
/// Provides extension methods for performing side-effects on <see cref="Either{TLeft, TRight}"/>.
/// </summary>
public static class TapExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Executes the specified action if the current <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TRight"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="onRight">An action to perform if the <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TRight"/>.</param>
        /// <returns>The unchanged <see cref="Either{TLeft, TRight}"/> instance.</returns>
        public Either<TLeft, TRight> Tap(Action<TRight> onRight)
        {
            if (either.IsRight(out var right))
            {
                onRight(right);
            }

            return either;
        }

        /// <summary>
        /// Executes the specified async action if the current <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TRight"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="onRightAsync">An async action to perform if the <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TRight"/>.</param>
        /// <returns>The unchanged <see cref="Either{TLeft, TRight}"/> instance.</returns>
        public async Task<Either<TLeft, TRight>> TapAsync(Func<TRight, Task> onRightAsync)
        {
            if (either.IsRight(out var right))
            {
                await onRightAsync(right);
            }

            return either;
        }
    }

    extension<TLeft, TRight>(Task<Either<TLeft, TRight>> eitherTask)
    {
        /// <summary>
        /// Executes the specified action if the current async <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TRight"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="onRight">An action to perform if the <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TRight"/>.</param>
        /// <returns>The unchanged <see cref="Either{TLeft, TRight}"/> instance.</returns>
        public async Task<Either<TLeft, TRight>> Tap(Action<TRight> onRight) =>
            (await eitherTask).Tap(onRight);

        /// <summary>
        /// Executes the specified async action if the current async <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TRight"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="onRightAsync">An async action to perform if the <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TRight"/>.</param>
        /// <returns>The unchanged <see cref="Either{TLeft, TRight}"/> instance.</returns>
        public async Task<Either<TLeft, TRight>> TapAsync(Func<TRight, Task> onRightAsync) =>
            await (await eitherTask).TapAsync(onRightAsync);
    }
}