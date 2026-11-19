using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Tests for the <see cref="MapExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class MapTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new TestFailure("original"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new TestFailure("original")));

    private readonly string _value = "Map";
    private readonly Func<string> _valueFunc = () => "Map";
    private readonly Func<string> _forbiddenFunc = () => throw new InvalidOperationException("This function should not be called");
    private readonly Func<Task<string>> _valueFuncAsync = () => Task.FromResult("Map");
    private readonly Func<Task<string>> _forbiddenFuncAsync = () => throw new InvalidOperationException("This function should not be called");

    [Test]
    public void Map_Should_ReturnSuccessFromValue_WhenResultIsSuccess()
    {
        var result = _success.Map(_value);
        result.ShouldBeSuccess().ShouldBe("Map");
    }

    [Test]
    public void Map_Should_ReturnFailureFromValue_WhenResultIsFailure()
    {
        var result = _failure.Map(_value);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public void Map_Should_ReturnSuccessFromValueFunc_WhenResultIsSuccess()
    {
        var result = _success.Map(_valueFunc);
        result.ShouldBeSuccess().ShouldBe("Map");
    }

    [Test]
    public void Map_Should_ReturnFailureFromValueFunc_WhenResultIsFailure()
    {
        var result = _failure.Map(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task MapAsync_Should_ReturnSuccess_WhenResultIsSuccess()
    {
        var result = await _success.MapAsync(_valueFuncAsync);
        result.ShouldBeSuccess().ShouldBe("Map");
    }

    [Test]
    public async Task MapAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.MapAsync(_forbiddenFuncAsync);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task Map_Should_ReturnSuccessFromValue_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.Map(_value);
        result.ShouldBeSuccess().ShouldBe("Map");
    }

    [Test]
    public async Task Map_Should_ReturnFailureFromValue_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Map(_value);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task Map_Should_ReturnSuccessFromValueFunc_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.Map(_valueFunc);
        result.ShouldBeSuccess().ShouldBe("Map");
    }

    [Test]
    public async Task Map_Should_ReturnFailureFromValueFunc_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Map(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task MapAsync_Should_ReturnSuccess_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.MapAsync(_valueFuncAsync);
        result.ShouldBeSuccess().ShouldBe("Map");
    }

    [Test]
    public async Task MapAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.MapAsync(_forbiddenFuncAsync);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }
}
