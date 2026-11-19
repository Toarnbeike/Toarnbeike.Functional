using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

public class CombineBindTests
{
    private readonly Result<int> _firstSuccess = 1;
    private readonly Result<int> _firstFailure = new TestFailure("first");

    private readonly Task<Result<int>> _firstSuccessTask, _firstFailureTask;

    private readonly Result<string> _secondSuccess = "success";
    private readonly Result<string> _secondFailure = new TestFailure("second");

    private readonly Task<Result<string>> _secondSuccessTask, _secondFailureTask;

    private readonly Func<int, string, Result<string>> _bindFunc = (first, second) => $"{first} {second}";
    private readonly Func<int, string, Result<string>> _forbiddenFunc = (_, _) => throw new InvalidOperationException("This function should not be called");

    private readonly Func<int, string, Task<Result<string>>> _bindTaskFunc, _forbiddenTaskFunc;

    public CombineBindTests()
    {
        _firstSuccessTask = Task.FromResult(_firstSuccess);
        _firstFailureTask = Task.FromResult(_firstFailure);
        _secondSuccessTask = Task.FromResult(_secondSuccess);
        _secondFailureTask = Task.FromResult(_secondFailure);
        _bindTaskFunc = (first, second) => Task.FromResult(_bindFunc(first, second));
        _forbiddenTaskFunc = (_, _) => throw new InvalidOperationException("This function should not be called");
    }

    [Test]
    public void CombineBind_ShouldReturnSuccess_WhenBothResultsAreSuccess()
    {
        var result = _firstSuccess.CombineBind(_secondSuccess, _bindFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public void CombineBind_ShouldReturnFailure_WhenFirstResultIsFailure()
    {
        var result = _firstFailure.CombineBind(_secondSuccess, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public void CombineBind_ShouldReturnFailure_WhenSecondResultIsFailure()
    {
        var result = _firstSuccess.CombineBind(_secondFailure, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnSuccess_WhenBothResultsAreSuccess()
    {
        var result = await _firstSuccess.CombineBindAsync(_secondSuccess, _bindTaskFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnFailure_WhenFirstResultIsFailure()
    {
        var result = await _firstFailure.CombineBindAsync(_secondSuccess, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnFailure_WhenSecondResultIsFailure()
    {
        var result = await _firstSuccess.CombineBindAsync(_secondFailure, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public async Task CombineBind_ShouldReturnSuccess_WhenBothResultTasksAreSuccess()
    {
        var result = await _firstSuccessTask.CombineBind(_secondSuccessTask, _bindFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineBind_ShouldReturnFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _firstFailureTask.CombineBind(_secondSuccessTask, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public async Task CombineBind_ShouldReturnFailure_WhenSecondResultTaskIsFailure()
    {
        var result = await _firstSuccessTask.CombineBind(_secondFailureTask, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnSuccess_WhenBothResultTasksAreSuccess()
    {
        var result = await _firstSuccessTask.CombineBindAsync(_secondSuccessTask, _bindTaskFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _firstFailureTask.CombineBindAsync(_secondSuccessTask, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnFailure_WhenSecondResultTaskIsFailure()
    {
        var result = await _firstSuccessTask.CombineBindAsync(_secondFailureTask, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }
}