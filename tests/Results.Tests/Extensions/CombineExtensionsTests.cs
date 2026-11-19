using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

public class CombineTests
{
    private readonly Result<int> _firstSuccess = 1;
    private readonly Result<int> _firstFailure = new TestFailure("first");

    private readonly Task<Result<int>> _firstSuccessTask, _firstFailureTask;

    private readonly Result<string> _secondSuccess = "success"; 
    private readonly Result<string> _secondFailure = new TestFailure("second");

    private readonly Task<Result<string>> _secondSuccessTask, _secondFailureTask;

    private readonly Func<int, string, string> _mapFunc = (first, second) => $"{first} {second}";
    private readonly Func<int, string, string> _forbiddenFunc = (_,_) => throw new InvalidOperationException("This function should not be called");

    private readonly Func<int, string, Task<string>> _mapTaskFunc, _forbiddenTaskFunc;

    public CombineTests()
    {
        _firstSuccessTask = Task.FromResult(_firstSuccess);
        _firstFailureTask = Task.FromResult(_firstFailure);
        _secondSuccessTask = Task.FromResult(_secondSuccess);
        _secondFailureTask = Task.FromResult(_secondFailure);
        _mapTaskFunc = (first, second) => Task.FromResult(_mapFunc(first, second));
        _forbiddenTaskFunc = (_, _) => throw new InvalidOperationException("This function should not be called");
    }

    [Test]
    public void Combine_ShouldReturnSuccess_WhenBothResultsAreSuccess()
    {
        var result = _firstSuccess.Combine(_secondSuccess, _mapFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public void Combine_ShouldReturnFailure_WhenFirstResultIsFailure()
    {
        var result = _firstFailure.Combine(_secondSuccess, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public void Combine_ShouldReturnFailure_WhenSecondResultIsFailure()
    {
        var result = _firstSuccess.Combine(_secondFailure, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnSuccess_WhenBothResultsAreSuccess()
    {
        var result = await _firstSuccess.CombineAsync(_secondSuccess, _mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnFailure_WhenFirstResultIsFailure()
    {
        var result = await _firstFailure.CombineAsync(_secondSuccess, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnFailure_WhenSecondResultIsFailure()
    {
        var result = await _firstSuccess.CombineAsync(_secondFailure, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public async Task Combine_ShouldReturnSuccess_WhenBothResultTasksAreSuccess()
    {
        var result = await _firstSuccessTask.Combine(_secondSuccessTask, _mapFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task Combine_ShouldReturnFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _firstFailureTask.Combine(_secondSuccessTask, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public async Task Combine_ShouldReturnFailure_WhenSecondResultTaskIsFailure()
    {
        var result = await _firstSuccessTask.Combine(_secondFailureTask, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnSuccess_WhenBothResultTasksAreSuccess()
    {
        var result = await _firstSuccessTask.CombineAsync(_secondSuccessTask, _mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _firstFailureTask.CombineAsync(_secondSuccessTask, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnFailure_WhenSecondResultTaskIsFailure()
    {
        var result = await _firstSuccessTask.CombineAsync(_secondFailureTask, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }
}
