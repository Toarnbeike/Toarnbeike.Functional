using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

public class MatchTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = new TestFailure("original");
    private readonly Result<string> _successTValue = Result.Success("Success");
    private readonly Result<string> _failureTValue = new TestFailure("original");
    private readonly Task<Result> _successTask, _failureTask;
    private readonly Task<Result<string>> _successTValueTask, _failureTValueTask;

    public MatchTests()
    {
        _successTask = Task.FromResult(_success);
        _failureTask = Task.FromResult(_failure);
        _successTValueTask = Task.FromResult(_successTValue);
        _failureTValueTask = Task.FromResult(_failureTValue);
    }

    [Test]
    public void Match_Should_ReturnTrue_WhenResultIsSuccess()
    {
        var isSuccess = false;
        _success.Match(() => isSuccess = true, _ => { });
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public void Match_Should_ReturnFalse_WhenResultIsFailure()
    {
        var isFailure = false;
        _failure.Match(() => { }, _ => isFailure = true);
        isFailure.ShouldBeTrue();
    }

    [Test]
    public async Task Match_Should_ReturnTrue_WhenResultTaskIsSuccess()
    {
        var isSuccess = false;
        await _successTask.Match(() => isSuccess = true, _ => { });
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task Match_Should_ReturnFalse_WhenResultTaskIsFailure()
    {
        var isFailure = false;
        await _failureTask.Match(() => { }, _ => isFailure = true);
        isFailure.ShouldBeTrue();
    }

    [Test]
    public void Match_Should_ReturnTrue_WhenResultIsTValue()
    {
        var isSuccess = false;
        _successTValue.Match(_ => isSuccess = true, _ => { });
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public void Match_Should_ReturnFalse_WhenResultIsTValueFailure()
    {
        var isFailure = false;
        _failureTValue.Match(_ => { }, _ => isFailure = true);
        isFailure.ShouldBeTrue();
    }

    [Test]
    public async Task Match_Should_ReturnTrue_WhenResultTValueTaskIsSuccess()
    {
        var isSuccess = false;
        await _successTValueTask.Match(_ => isSuccess = true, _ => { });
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task Match_Should_ReturnFalse_WhenResultTValueTaskIsFailure()
    {
        var isFailure = false;
        await _failureTValueTask.Match(_ => { }, _ => isFailure = true);
        isFailure.ShouldBeTrue();
    }
}
