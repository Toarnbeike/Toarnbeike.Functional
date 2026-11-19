using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results;

public class ResultTests
{
    [Test]
    public void Success_ShouldReturn_SuccessResult()
    {
        var result = Result.Success();
        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
    }

    [Test]
    public void Failure_ShouldReturn_FailureResult()
    {
        var result = Result.Failure(TestFailure.Instance);

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
    }

    [Test]
    public void Failure_ShouldImplicitlyCreate_FailureResult()
    {
        Result result = TestFailure.Instance;
        result.IsFailure.ShouldBeTrue();
    }

    [Test]
    public void TryGetFailure_ShouldReturn_True_WhenResultIsFailure()
    {
        var result = Result.Failure(TestFailure.Instance);

        result.TryGetFailure(out var actualFailure).ShouldBeTrue();
        actualFailure.ShouldNotBeNull();
        actualFailure.ShouldBeTestFailure();
    }

    [Test]
    public void TryGetFailure_ShouldReturn_False_WhenResultIsSuccess()
    {
        var result = Result.Success();
        result.TryGetFailure(out _).ShouldBeFalse();
    }
}
