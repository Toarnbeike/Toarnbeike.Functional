using System.Collections;

namespace Toarnbeike.Results.TestExtensions;

public static class TestExtensions
{
    /// <summary>
    /// Provides extension methods for testing <see cref="Result"/> objects in unit tests.
    /// </summary>
    extension(Result result)
    {
        /// <summary>
        /// Verifies that the <see cref="Result"/> is in a successful state during unit tests.
        /// </summary>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result"/> is a failure. The exception includes details about the failure.
        /// </exception>
        public void ShouldBeSuccess()
        {
            if (result.TryGetFailure(out var failure))
            {
                throw new FunctionalAssertException($"Expected success, but got failure: {failure}");
            }
        }

        /// <summary>
        /// Verifies that the <see cref="Result"/> is in a failed state during unit tests and retrieves the associated <see cref="Failure"/> information.
        /// </summary>
        /// <returns>
        /// The <see cref="Failure"/> object representing the reason for failure if the <see cref="Result"/> is in a failed state.
        /// </returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result"/> is not a failure. The exception includes details about the success state.
        /// </exception>
        public Failure ShouldBeFailure()
        {
            return result.TryGetFailure(out var actual)
                ? actual
                : throw new FunctionalAssertException("Expected failure, but got success.");
        }

        /// <summary>
        /// Verifies that the <see cref="Result"/> is in a failure state with the specified failure code during unit tests.
        /// </summary>
        /// <param name="code">The expected failure code to match against the <see cref="Failure.Code"/>.</param>
        /// <returns>
        /// The <see cref="Failure"/> instance associated with the <see cref="Result"/> if the failure code matches.
        /// </returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result"/> is not a failure or the failure code does not match the expected value.
        /// </exception>
        public Failure ShouldBeFailureWithCode(string code)
        {
            var actual = result.ShouldBeFailure();
            return actual.Code == code
                ? actual
                : throw new FunctionalAssertException($"Expected failure with '{code}', but was '{actual.Code}'");
        }

        /// <summary>
        /// Verifies that the <see cref="Result"/> is in a failure state with the specified message during unit tests.
        /// </summary>
        /// <param name="message">The expected failure message.</param>
        /// <returns>The <see cref="Failure"/> containing the expected message.</returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result"/> is not a failure or its message does not match the expected value.
        /// </exception>
        public Failure ShouldBeFailureWithMessage(string message)
        {
            var actual = result.ShouldBeFailure();
            return actual.Message == message
                ? actual
                : throw new FunctionalAssertException($"Expected failure with '{message}', but was '{actual.Message}'");
        }

        /// <summary>
        /// Verifies that the <see cref="Result"/> is a failure and satisfies a specified condition during unit tests.
        /// </summary>
        /// <param name="predicate"> A function that defines the condition to be satisfied by the failure. </param>
        /// <param name="description"> A description of the condition being verified, used in the exception message if the condition is not met. </param>
        /// <returns>
        /// The <see cref="Failure"/> instance if the <see cref="Result"/> is a failure and satisfies the specified condition.
        /// </returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result"/> is not a failure or the condition defined by <paramref name="predicate"/> is not satisfied. The exception includes details about the failure and the unmet condition.
        /// </exception>
        public Failure ShouldBeFailureThatSatisfies(Func<Failure, bool> predicate, string description)
        {
            var actual = result.ShouldBeFailure();
            return predicate(actual)
                ? actual
                : throw new FunctionalAssertException($"Expected failure that satisfies '{description}', but got {actual}.");
        }

        /// <summary>
        /// Verifies that the <see cref="Result"/> represents a failure of the specified type <typeparamref name="TFailure"/> during unit tests.
        /// </summary>
        /// <typeparam name="TFailure">The expected type of the failure.</typeparam>
        /// <returns>The failure object of type <typeparamref name="TFailure"/>.</returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result"/> is not a failure or when the failure is not of the specified type <typeparamref name="TFailure"/>.
        /// </exception>
        public TFailure ShouldBeFailureOfType<TFailure>() where TFailure : Failure
        {
            var actual = result.ShouldBeFailure();
            return actual as TFailure
                   ?? throw new FunctionalAssertException($"Expected failure of type {typeof(TFailure).Name}, but was {actual.GetType().Name}");
        }
    }

    /// <summary>
    /// Provides extension methods for asserting the states and values of <see cref="Result{TValue}"/> in unit tests.
    /// </summary>
    extension<TValue>(Result<TValue> result)
    {
        /// <summary>
        /// Asserts that the <see cref="Result{TValue}"/> is in a successful state and retrieves its value during unit tests.
        /// </summary>
        /// <returns> The value of the <see cref="Result{TValue}"/> if it is successful. </returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result{TValue}"/> is in a failure state. The exception contains details about the failure.
        /// </exception>
        public TValue ShouldBeSuccess()
        {
            return result.TryGetContents(out var value, out var failure)
                ? value 
                : throw new FunctionalAssertException($"Expected success, but got failure: {failure}");
        }

        /// <summary>
        /// Asserts that the result is a success and that the contained value equals the expected value.
        /// </summary>
        /// <typeparam name="TValue">The expected type of the result value.</typeparam>
        /// <param name="expected">The expected value to compare with the result's value.</param>
        /// <param name="customMessage">Optional custom message for assertion failure.</param>
        /// <returns>The value contained in the successful result.</returns>
        /// <exception cref="FunctionalAssertException">Thrown when the result is a failure or the value does not match.</exception>
        public TValue ShouldBeSuccessWithValue(TValue expected, string? customMessage = null)
        {
            var actual = result.ShouldBeSuccess();

            // Special case: compare element-wise for IEnumerable (but not string)
            if (actual is IEnumerable actualEnum && expected is IEnumerable expectedEnum
                                                 && actual is not string && expected is not string)
            {
                if (!actualEnum.Cast<object>().SequenceEqual(expectedEnum.Cast<object>()))
                {
                    var actualStr = string.Join(", ", actualEnum.Cast<object>());
                    var expectedStr = string.Join(", ", expectedEnum.Cast<object>());
                    throw new FunctionalAssertException(customMessage
                                                        ?? $"Expected success result with value '[{expectedStr}]', but got '[{actualStr}]'.");
                }

                return actual;
            }

            if (!Equals(actual, expected))
            {
                throw new FunctionalAssertException(customMessage ?? $"Expected success result with value '{expected}', but got '{actual}'.");
            }

            return actual;
        }

        /// <summary>
        /// Asserts that the <see cref="Result{TValue}"/> is in a successful state and that its value satisfies the provided predicate during unit tests.
        /// </summary>
        /// <param name="predicate">A function that defines the condition the value of the <see cref="Result{TValue}"/> must satisfy.</param>
        /// <param name="description">A description of the condition to include in the exception message if the predicate fails.</param>
        /// <returns>The value of the <see cref="Result{TValue}"/> if it is successful and satisfies the predicate.</returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result{TValue}"/> is in a failure state or when its value does not satisfy the predicate. The exception includes the provided description and details about the failure.
        /// </exception>
        public TValue ShouldBeSuccessThatSatisfies(Func<TValue, bool> predicate, string description)
        {
            var actual = result.ShouldBeSuccess();
            return predicate(actual)
                ? actual
                : throw new FunctionalAssertException($"Expected result that satisfies {description}, but got '{actual}'");
        }

        /// <summary>
        /// Verifies that the <see cref="Result{TValue}"/> is in a failure state during unit tests.
        /// </summary>
        /// <returns> The <see cref="Failure"/> object representing the reason for the failure. </returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result{TValue}"/> is not a failure. The exception includes details about the actual state.
        /// </exception>
        public Failure ShouldBeFailure()
        {
            return result.TryGetFailure(out var actual)
                ? actual
                : throw new FunctionalAssertException("Expected failure, but got success.");
        }

        /// <summary>
        /// Asserts that the current <see cref="Result{TValue}"/> instance represents a failure with the specified code.
        /// </summary>
        /// <param name="code">The expected failure code to match against the <see cref="Failure.Code"/>.</param>
        /// <returns>The <see cref="Failure"/> instance associated with the result.</returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the result is not a failure or the failure code does not match the expected value.
        /// </exception>
        public Failure ShouldBeFailureWithCode(string code)
        {
            var actual = result.ShouldBeFailure();
            return actual.Code == code
                ? actual
                : throw new FunctionalAssertException($"Expected failure with '{code}', but was '{actual.Code}'");
        }

        /// <summary>
        /// Verifies that the <see cref="Result{TValue}"/> is in a failure state during unit tests and matches the specified failure message.
        /// </summary>
        /// <param name="message"> The expected failure message to be checked against the actual failure. </param>
        /// <returns> The <see cref="Failure"/> instance if the result is a failure and the message matches the expected value. </returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result{TValue}"/> is not a failure or if the failure message does not match the specified value.
        /// </exception>
        public Failure ShouldBeFailureWithMessage(string message)
        {
            var actual = result.ShouldBeFailure();
            return actual.Message == message
                ? actual
                : throw new FunctionalAssertException($"Expected failure with '{message}', but was '{actual.Message}'");
        }

        /// <summary>
        /// Asserts that the <see cref="Failure"/> result satisfies the specified predicate.
        /// </summary>
        /// <param name="predicate">A function that evaluates whether the failure meets certain conditions.</param>
        /// <param name="description">A description of the condition the failure is expected to satisfy, used for error reporting.</param>
        /// <returns>The <see cref="Failure"/> object if it satisfies the predicate.</returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the failure does not satisfy the provided predicate. The exception includes the provided description and details of the failure.
        /// </exception>
        public Failure ShouldBeFailureThatSatisfies(Func<Failure, bool> predicate, string description)
        {
            var actual = result.ShouldBeFailure();
            return predicate(actual)
                ? actual
                : throw new FunctionalAssertException($"Expected failure that satisfies '{description}', but got {actual}.");
        }

        /// <summary>
        /// Verifies that the <see cref="Result{TValue}"/> is in a failure state and the failure is of the specified type <typeparamref name="TFailure"/>.
        /// </summary>
        /// <typeparam name="TFailure">The expected type of the failure derived from <see cref="Failure"/>.</typeparam>
        /// <returns>The failure, cast to the specified type <typeparamref name="TFailure"/>.</returns>
        /// <exception cref="FunctionalAssertException">
        /// Thrown when the <see cref="Result{TValue}"/> is not a failure, or when the failure is not of the specified type <typeparamref name="TFailure"/>.
        /// </exception>
        public TFailure ShouldBeFailureOfType<TFailure>() where TFailure : Failure
        {
            var actual = result.ShouldBeFailure();
            return actual as TFailure
                   ?? throw new FunctionalAssertException($"Expected failure of type {typeof(TFailure).Name}, but was {actual.GetType().Name}");
        }
    }
}