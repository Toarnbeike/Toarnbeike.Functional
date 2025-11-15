namespace Toarnbeike.Eithers.Extensions;

/// <summary>
/// Provides extension methods for matching on <see cref="Either{TLeft, TRight}"/>.
/// </summary>
public static class MatchExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Matches the current <see cref="Either{TLeft, TRight}"/> by invoking the appropriate action
        /// depending on whether it contains a left or right value.
        /// </summary>
        /// <param name="onLeft">The action to invoke if the instance contains a left value.</param>
        /// <param name="onRight">The action to invoke if the instance contains a right value.</param>
        public void Match(Action<TLeft> onLeft, Action<TRight> onRight)
        {
            if (either.IsLeft(out var left, out var right))
            {
                onLeft(left);
            }
            else
            {
                onRight(right);
            }
        }

        /// <summary>
        /// Matches the current <see cref="Either{TLeft, TRight}"/> by invoking the appropriate action
        /// depending on whether it contains a left or right value.
        /// </summary>
        /// <param name="onLeft">The async action to invoke if the instance contains a left value.</param>
        /// <param name="onRightAsync">The async action to invoke if the instance contains a right value.</param>
        public async Task MatchAsync(Func<TLeft, Task> onLeft, Func<TRight, Task> onRightAsync)
        {
            if (either.IsLeft(out var left, out var right))
            {
                await onLeft(left);
            }
            else
            {
                await onRightAsync(right);
            }
        }
        
        /// <summary>
        /// Converts the current <see cref="Either{TLeft, TRight}"/> to an TOut by invoking the appropriate mappping
        /// depending on whether it contains a left or right value.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting value.</typeparam>
        /// <param name="onLeft">The mapping to invoke if the instance contains a left value.</param>
        /// <param name="onRight">The mapping to invoke if the instance contains a right value.</param>
        /// <returns> An instance of TOut.</returns>
        public TOut Match<TOut>(Func<TLeft, TOut> onLeft, Func<TRight, TOut> onRight) =>
            either.IsLeft(out var left, out var right)
                ? onLeft(left)
                : onRight(right);

        /// <summary>
        /// Converts the current <see cref="Either{TLeft, TRight}"/> to an TOut by invoking the appropriate async mappping
        /// depending on whether it contains a left or right value.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting value.</typeparam>
        /// <param name="onLeftAsync">The async mapping to invoke if the instance contains a left value.</param>
        /// <param name="onRightAsync">The async mapping to invoke if the instance contains a right value.</param>
        /// <returns> An instance of TOut.</returns>
        public async Task<TOut> MatchAsync<TOut>(Func<TLeft, Task<TOut>> onLeftAsync, Func<TRight, Task<TOut>> onRightAsync) =>
            either.IsLeft(out var left, out var right)
                ? await onLeftAsync(left)
                : await onRightAsync(right);
    }

    extension<TLeft, TRight>(Task<Either<TLeft, TRight>> eitherTask)
    {
        /// <summary>
        /// Matches the current async <see cref="Either{TLeft, TRight}"/> by invoking the appropriate action
        /// depending on whether it contains a left or right value.
        /// </summary>
        /// <param name="onLeft">The action to invoke if the instance contains a left value.</param>
        /// <param name="onRight">The action to invoke if the instance contains a right value.</param>
        public async Task Match(Action<TLeft> onLeft, Action<TRight> onRight) =>
            (await eitherTask).Match(onLeft, onRight);

        /// <summary>
        /// Matches the current async <see cref="Either{TLeft, TRight}"/> by invoking the appropriate action
        /// depending on whether it contains a left or right value.
        /// </summary>
        /// <param name="onLeft">The async action to invoke if the instance contains a left value.</param>
        /// <param name="onRightAsync">The async action to invoke if the instance contains a right value.</param>
        public async Task MatchAsync(Func<TLeft, Task> onLeft, Func<TRight, Task> onRightAsync) =>
            await (await eitherTask).MatchAsync(onLeft, onRightAsync);

        /// <summary>
        /// Converts the current async <see cref="Either{TLeft, TRight}"/> to an TOut by invoking the appropriate mappping
        /// depending on whether it contains a left or right value.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting value.</typeparam>
        /// <param name="onLeft">The mapping to invoke if the instance contains a left value.</param>
        /// <param name="onRight">The mapping to invoke if the instance contains a right value.</param>
        /// <returns> An instance of TOut.</returns>
        public async Task<TOut> Match<TOut>(Func<TLeft, TOut> onLeft, Func<TRight, TOut> onRight) =>
            (await eitherTask).Match(onLeft, onRight);

        /// <summary>
        /// Converts the current async <see cref="Either{TLeft, TRight}"/> to an TOut by invoking the appropriate async mappping
        /// depending on whether it contains a left or right value.
        /// </summary>
        /// <typeparam name="TOut">The type of the resulting value.</typeparam>
        /// <param name="onLeftAsync">The async mapping to invoke if the instance contains a left value.</param>
        /// <param name="onRightAsync">The async mapping to invoke if the instance contains a right value.</param>
        /// <returns> An instance of TOut.</returns>
        public async Task<TOut> MatchAsync<TOut>(Func<TLeft, Task<TOut>> onLeftAsync, Func<TRight, Task<TOut>> onRightAsync) =>
            await (await eitherTask).MatchAsync(onLeftAsync, onRightAsync);
    }
}