using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Tests for the <see cref="BindExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class BindResultTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new TestFailure("original"));

    private readonly Task<Result> _successTask, _failureTask;
    private readonly Func<Result<int>> _successFunc = () => 42;
    private readonly Func<Result<int>> _failureFunc = () => new TestFailure("bind");
    private readonly Func<Result<int>> _forbiddenFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Task<Result<int>>> _successTaskFunc, _failureTaskFunc, _forbiddenTaskFunc;

    public BindResultTests()
    {
        _successTask = Task.FromResult(_success);
        _failureTask = Task.FromResult(_failure);
        _successTaskFunc = () => Task.FromResult(_successFunc());
        _failureTaskFunc = () => Task.FromResult(_failureFunc());
        _forbiddenTaskFunc = () => throw new InvalidOperationException("This function should not be called");
    }

    [Test]
    public void Bind_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = _success.Bind(_successFunc);
        result.ShouldBeSuccess().ShouldBe(42);
    }

    [Test]
    public void Bind_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = _success.Bind(_failureFunc);
        result.ShouldBeFailure().Message.ShouldBe("bind");
    }

    [Test]
    public void Bind_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Bind(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = await _success.BindAsync(_successTaskFunc);
        result.ShouldBeSuccess().ShouldBe(42);
    }

    [Test]
    public async Task BindAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = await _success.BindAsync(_failureTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("bind");
    }

    [Test]
    public async Task BindAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.BindAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task Bind_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.Bind(_successFunc);
        result.ShouldBeSuccess().ShouldBe(42);
    }

    [Test]
    public async Task Bind_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.Bind(_failureFunc);
        result.ShouldBeFailure().Message.ShouldBe("bind");
    }

    [Test]
    public async Task Bind_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Bind(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.BindAsync(_successTaskFunc);
        result.ShouldBeSuccess().ShouldBe(42);
    }

    [Test]
    public async Task BindAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.BindAsync(_failureTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("bind");
    }

    [Test]
    public async Task BindAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.BindAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }
}

/// <summary>
/// Tests for the <see cref="BindExtensions"/> on a <see cref="Result{TIn}"/>.
/// </summary>
public class BindResultTValueExtensionsTests
{
    private readonly Result<double> _success, _failure;
    private readonly Task<Result<double>> _successTask, _failureTask;
    private readonly Func<double, Result<int>> _successFunc, _failureFunc, _forbiddenFunc;
    private readonly Func<double, Task<Result<int>>> _successTaskFunc, _failureTaskFunc, _forbiddenTaskFunc;

    public BindResultTValueExtensionsTests()
    {
        _success = 1.3;
        _failure = new TestFailure("original");

        _successTask = Task.FromResult(_success);
        _failureTask = Task.FromResult(_failure);

        _successFunc = value => (int)Math.Round(value, 0);
        _failureFunc = _ => new TestFailure("bind");
        _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

        _successTaskFunc = value => Task.FromResult(_successFunc(value));
        _failureTaskFunc = _ => Task.FromResult(_failureFunc(0));
        _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");
    }

    [Test]
    public void Bind_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = _success.Bind(_successFunc);
        result.ShouldBeSuccess().ShouldBe(1);
    }

    [Test]
    public void Bind_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = _success.Bind(_failureFunc);
        result.ShouldBeFailure().Message.ShouldBe("bind");
    }

    [Test]
    public void Bind_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Bind(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = await _success.BindAsync(_successTaskFunc);
        result.ShouldBeSuccess().ShouldBe(1);
    }

    [Test]
    public async Task BindAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = await _success.BindAsync(_failureTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("bind");
    }

    [Test]
    public async Task BindAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.BindAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task Bind_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.Bind(_successFunc);
        result.ShouldBeSuccess().ShouldBe(1);
    }

    [Test]
    public async Task Bind_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.Bind(_failureFunc);
        result.ShouldBeFailure().Message.ShouldBe("bind");
    }

    [Test]
    public async Task Bind_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Bind(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.BindAsync(_successTaskFunc);
        result.ShouldBeSuccess().ShouldBe(1);
    }

    [Test]
    public async Task BindAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.BindAsync(_failureTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("bind");
    }

    [Test]
    public async Task BindAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.BindAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }
}
