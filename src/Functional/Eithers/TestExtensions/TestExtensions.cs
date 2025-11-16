namespace Toarnbeike.Eithers.TestExtensions;

/// <summary>
/// Assertion extensions for <see cref="Either{TLeft, TRight}"/>
/// </summary>
public static class TestExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Asserts that the current <see cref="Either{TLeft, TRight}"/> holds a left value.
        /// </summary>
        /// <returns>The actual left value.</returns>
        /// <exception cref="FunctionalAssertException">Thrown if the either monad holds a right value.</exception>
        public TLeft ShouldBeLeft()
        {
            return either.IsLeft(out var left)
                ? left
                : throw new FunctionalAssertException(
                    $"Expected left ({typeof(TLeft).Name}) value, but got right ({typeof(TRight).Name}) value.");
        }

        /// <summary>
        /// Asserts that the current <see cref="Either{TLeft, TRight}"/> holds a left value with the specified value.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <exception cref="FunctionalAssertException">Thrown if the either monad holds a right value.</exception>
        /// <exception cref="FunctionalAssertException">Thrown if the expected left value and actual left value don't match.</exception>
        public void ShouldBeLeftWithValue(TLeft expected)
        {
            var actual = either.ShouldBeLeft();
            if (!EqualityComparer<TLeft>.Default.Equals(actual, expected))
            {
                throw new FunctionalAssertException($"Expected '{expected}', but got '{actual}'.");
            }
        }

        /// <summary>
        /// Asserts that the current <see cref="Either{TLeft, TRight}"/> holds a right value.
        /// </summary>
        /// <returns>The actual left value.</returns>
        /// <exception cref="FunctionalAssertException">Thrown if the either monad holds a left value.</exception>
        public TRight ShouldBeRight()
        {
            return either.IsRight(out var right)
                ? right
                : throw new FunctionalAssertException(
                    $"Expected right ({typeof(TRight).Name}) value, but got left ({typeof(TLeft).Name}) value.");
        }

        /// <summary>
        /// Asserts that the current <see cref="Either{TLeft, TRight}"/> holds a right value with the specified value.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <exception cref="FunctionalAssertException">Thrown if the either monad holds a left value.</exception>
        /// <exception cref="FunctionalAssertException">Thrown if the expected right value and actual right value don't match.</exception>
        public void ShouldBeRightWithValue(TRight expected)
        {
            var actual = either.ShouldBeRight();
            if (!EqualityComparer<TRight>.Default.Equals(actual, expected))
            {
                throw new FunctionalAssertException($"Expected '{expected}', but got '{actual}'.");
            }
        }
    }
}