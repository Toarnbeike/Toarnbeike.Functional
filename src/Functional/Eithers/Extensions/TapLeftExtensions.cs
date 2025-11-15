namespace Toarnbeike.Eithers.Extensions;

/// <summary>
/// Provides extension methods for performing side-effects on <see cref="Either{TLeft, TRight}"/>.
/// </summary>
public static class TapLeftExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Executes the specified action if the current <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TLeft"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="onLeft">An action to perform if the <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TLeft"/>.</param>
        /// <returns>The unchanged <see cref="Either{TLeft, TRight}"/> instance.</returns>
        public Either<TLeft, TRight> TapLeft(Action<TLeft> onLeft)
        {
            if (either.IsLeft(out var left))
            {
                onLeft(left);
            }

            return either;
        }

        /// <summary>
        /// Executes the specified async action if the current <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TLeft"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="onLeftAsync">An async action to perform if the <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TLeft"/>.</param>
        /// <returns>The unchanged <see cref="Either{TLeft, TRight}"/> instance.</returns>
        public async Task<Either<TLeft, TRight>> TapLeftAsync(Func<TLeft, Task> onLeftAsync)
        {
            if (either.IsLeft(out var left))
            {
                await onLeftAsync(left);
            }

            return either;
        }
    }

    extension<TLeft, TRight>(Task<Either<TLeft, TRight>> eitherTask)
    {
        /// <summary>
        /// Executes the specified action if the current async <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TLeft"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="onLeft">An action to perform if the <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TLeft"/>.</param>
        /// <returns>The unchanged <see cref="Either{TLeft, TRight}"/> instance.</returns>
        public async Task<Either<TLeft, TRight>> TapLeft(Action<TLeft> onLeft) =>
            (await eitherTask).TapLeft(onLeft);
            
        /// <summary>
        /// Executes the specified async action if the current async <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TLeft"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="onLeftAsync">An async action to perform if the <see cref="Either{TLeft, TRight}"/>
        /// holds a value of type <typeparamref name="TLeft"/>.</param>
        /// <returns>The unchanged <see cref="Either{TLeft, TRight}"/> instance.</returns>
        public async Task<Either<TLeft, TRight>> TapLeftAsync(Func<TLeft, Task> onLeftAsync) =>
            await (await eitherTask).TapLeftAsync(onLeftAsync);
    }
}