namespace Toarnbeike.Eithers.Extensions;

/// <summary>
/// Swap the left and right values of an <see cref="Either{TLeft, TRight}"/>
/// </summary>
public static class SwapExtensions
{
    /// <summary>
    /// Swaps the left and right values of an <see cref="Either{TLeft, TRight}"/>.
    /// If the <see cref="Either{TLeft, TRight}"/> holds a left value, it will be converted
    /// into a right value, and vice versa.
    /// </summary>
    /// <typeparam name="TLeft">The type of the left value.</typeparam>
    /// <typeparam name="TRight">The type of the right value.</typeparam>
    /// <param name="either">The <see cref="Either{TLeft, TRight}"/> instance to swap.</param>
    /// <returns>An <see cref="Either{TRight, TLeft}"/> with the left and right values swapped.</returns>
    public static Either<TRight, TLeft> Swap<TLeft, TRight>(this Either<TLeft, TRight> either) =>
        either.IsLeft(out var left, out var right)
            ? Either<TRight, TLeft>.Right(left)
            : Either<TRight, TLeft>.Left(right);

    /// <summary>
    /// Swaps the left and right values of an async <see cref="Either{TLeft, TRight}"/>.
    /// If the <see cref="Either{TLeft, TRight}"/> holds a left value, it will be converted
    /// into a right value, and vice versa.
    /// </summary>
    /// <typeparam name="TLeft">The type of the left value.</typeparam>
    /// <typeparam name="TRight">The type of the right value.</typeparam>
    /// <param name="eitherTask">The async <see cref="Either{TLeft, TRight}"/> instance to swap.</param>
    /// <returns>An <see cref="Either{TRight, TLeft}"/> with the left and right values swapped.</returns>
    public static async Task<Either<TRight, TLeft>> SwapAsync<TLeft, TRight>(this Task<Either<TLeft, TRight>> eitherTask) =>
        (await eitherTask).Swap();
}