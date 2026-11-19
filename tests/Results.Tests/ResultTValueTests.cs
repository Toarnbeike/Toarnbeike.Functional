using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results;

public class ResultTValueTests
{
    private readonly int _expectedValue = 42;

    [Test]
    public void Success_ShouldReturn_SuccessResult_WithValue()
    {
        var result = Result.Success(_expectedValue);

        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
    }

    [Test]
    public void Failure_ShouldReturn_FailureResult()
    {
        var result = Result.Failure<int>(TestFailure.Instance);

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
    }

    [Test]
    public void TryGetValue_ShouldReturn_True_WhenResultIsSuccess()
    {
        var result = Result.Success(_expectedValue);

        result.TryGetValue(out var actualValue).ShouldBeTrue();

        actualValue.ShouldBe(_expectedValue);
    }

    [Test]
    public void TryGetValue_ShouldReturn_False_WhenResultIsFailure()
    {
        var result = Result.Failure<int>(TestFailure.Instance);

        result.TryGetValue(out _).ShouldBeFalse();
    }

    [Test]
    public void TryGetFailure_ShouldReturn_True_WhenResultIsFailure()
    {
        var result = Result.Failure<int>(TestFailure.Instance);

        result.TryGetFailure(out var actualFailure).ShouldBeTrue();
        actualFailure.ShouldNotBeNull();
        actualFailure.Message.ShouldBe(TestFailure.Instance.Message);
    }

    [Test]
    public void TryGetFailure_ShouldReturn_False_WhenResultIsSuccess()
    {
        var result = Result.Success(_expectedValue);
        result.TryGetFailure(out _).ShouldBeFalse();
    }

    [Test]
    public void Success_ShouldReturn_SuccessResult_WithValue_EvenWithoutTypeInfo()
    {
        var result = Result.Success(_expectedValue);
        
        result.IsSuccess.ShouldBeTrue();
        result.ShouldBeOfType<Result<int>>();
        result.TryGetValue(out var actualValue).ShouldBeTrue();
        actualValue.ShouldBe(_expectedValue);
    }

    [Test]
    public void ImplicitConversion_ShouldConvert_FailureToResult()
    {
        Result<int> result = TestFailure.Instance;

        result.IsFailure.ShouldBeTrue();
    }

    [Test]
    public void ImplicitConversion_ShouldConvert_TValueToResultTValue()
    {
        Result<int> result = _expectedValue;

        result.IsFailure.ShouldBeFalse();
    }
}