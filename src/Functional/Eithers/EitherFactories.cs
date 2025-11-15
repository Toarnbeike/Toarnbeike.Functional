namespace Toarnbeike.Eithers;

public static class EitherFactories
{
    extension<TLeft, TRight>(Either<TLeft, TRight>) 
    {
        /// <summary>
        /// Creates a new instance of <see cref="Either{TLeft, TRight}"/> representing a left value.
        /// </summary>
        /// <param name="left">The left value to be encapsulated by the instance.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/> instance that holds the specified left value.</returns>
        public static Either<TLeft, TRight> Left(TLeft left) => new(left, default, false);
        
        /// <summary>
        /// Creates a new instance of <see cref="Either{TLeft, TRight}"/> that represents a right value.
        /// </summary>
        /// <param name="right">The value to be held by the right side of the instance.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/> containing the specified right value.</returns>
        public static Either<TLeft, TRight> Right(TRight right) => new(default, right, true);
    }
}