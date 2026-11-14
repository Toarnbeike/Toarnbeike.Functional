using Toarnbeike.Options;

namespace Toarnbeike.Functional.TestExtensions;

/// <summary>
/// Provides assertion methods for Option{TValue} to use in tests.
/// </summary>
public static class OptionTestExtensions
{
    /// <param name="option">The option to assert on.</param>
    /// <typeparam name="TValue">The value type inside the Option.</typeparam>
    extension<TValue>(Option<TValue> option) where TValue : notnull
    {
        /// <summary>
        /// Asserts that the option is Some; throws an exception if it is None.
        /// </summary>
        /// <param name="message">Optional custom failure message.</param>
        /// <returns>The inner value if present.</returns>
        /// <exception cref="AssertionFailedException">Thrown if the Option is None.</exception>
        public TValue ShouldBeSome(string? message = null)
        {
            return option.TryGetValue(out var value)
                ? value
                : throw new AssertionFailedException(message ?? $"Expected Option<{typeof(TValue).Name}> to be Some, but it was None.");
        }

        /// <summary>
        /// Asserts that the option is None; throws an exception if it is Some.
        /// </summary>
        /// <param name="message">Optional custom failure message.</param>
        /// <exception cref="AssertionFailedException">Thrown if the Option is Some.</exception>
        public void ShouldBeNone(string? message = null)
        {
            if (option.TryGetValue(out var value))
            {
                throw new AssertionFailedException(message ?? $"Expected Option<{typeof(TValue).Name}> to be None, but it was Some with value {value}.");
            }
        }
    }
}