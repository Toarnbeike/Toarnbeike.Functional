using Toarnbeike.Results;

namespace Toarnbeike.Functional.TestExtensions;

public static class ResultTestExtensions
{
    extension(Result result)
    {
        public void ShouldBeSuccess()
        {
            if (result.TryGetFailure(out var failure))
            {
                throw new AssertionFailedException($"Expected Success but got Failure with message: {failure.Message}.");
            }
        }

        public Failure ShouldBeFailure()
        {
            if (!result.TryGetFailure(out var failure))
            {
                throw new AssertionFailedException("Expected Failure but got Success.");
            }
            return failure;
        }

        public TestFailure ShouldBeTestFailure()
        {
            return result switch
            {
                Success => throw new AssertionFailedException("Expected a TestFailure but got Success."),
                Failure failure => failure.ShouldBeTestFailure(),
            };
        }
    }

    extension<TValue>(Result<TValue> result) where TValue : notnull
    {
        public TValue ShouldBeSuccess() => result switch
        {
            TValue value => value,
            Failure failure => throw new AssertionFailedException($"Expected Success but got Failure with message: {failure.Message}.")
        };

        public Failure ShouldBeFailure() => result switch
        {
            TValue value => throw new AssertionFailedException($"Expected Failure but got Success with value: {value}."),
            Failure failure => failure
        };

        public void ShouldBeTestFailure()
        {
            var _ = result switch
            {
                TValue => throw new AssertionFailedException($"Expected a TestFailure but got Success with value of type {typeof(TValue).Name}."),
                Failure failure => failure.ShouldBeTestFailure(),
            };
        }
    }

    public static TFailure ShouldBeFailureOfType<TFailure>(this IResult result) where TFailure : Failure
    {
        if (!result.TryGetFailure(out var failure))
        {
            throw new AssertionFailedException($"Expected Failure of type {typeof(TFailure).Name} but got Success.");
        }

        if (failure is not TFailure typedFailure)
        {
            throw new AssertionFailedException($"Expected Failure of type {typeof(TFailure).Name} but got {failure.GetType().Name}.");
        }

        return typedFailure;
    }

    public static TestFailure ShouldBeTestFailure(this Failure failure)
    {
        if (failure is not TestFailure testFailure)
        {
            throw new AssertionFailedException($"Expected a TestFailure but got {failure.GetType().Name}.");
        }
        return testFailure;
    }
}