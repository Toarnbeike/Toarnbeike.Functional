using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Tests for the <see cref="MapExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class MapTValueTests
{
    private readonly Result<double> _success = 1.3;
    private readonly Result<double> _failure = new TestFailure("original");

    private readonly Task<Result<double>> _successTask;
    private readonly Task<Result<double>> _failureTask;

    private readonly Func<double, int> _mapFunc = value => (int)(value * 2);
    private readonly Func<double, int> _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<int>> _mapTaskFunc;
    private readonly Func<double, Task<int>> _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    public MapTValueTests()
    {
        _successTask = Task.FromResult(_success);
        _failureTask = Task.FromResult(_failure);
        _mapTaskFunc = value => Task.FromResult(_mapFunc(value));
    }

    [Test]
    public void Map_Should_ReturnValue_WhenResultIsSuccess()
    {
        var result = _success.Map(_mapFunc);
        result.ShouldBeSuccess().ShouldBe(2);
    }

    [Test]
    public void Map_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Map(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task MapAsync_Should_ReturnValue_WhenResultIsSuccess()
    {
        var result = await _success.MapAsync(_mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe(2);
    }

    [Test]
    public async Task MapAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.MapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task Map_Should_ReturnValue_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.Map(_mapFunc);
        result.ShouldBeSuccess().ShouldBe(2);
    }

    [Test]
    public async Task Map_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Map(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task MapAsync_Should_ReturnValue_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.MapAsync(_mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe(2);
    }

    [Test]
    public async Task MapAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.MapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }
}