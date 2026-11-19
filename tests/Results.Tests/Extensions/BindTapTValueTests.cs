using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Tests for the <see cref="BindTapExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class BindTapTValueTests
{
    private readonly Result<double> _success = Result.Success(1.3);
    private readonly Result<double> _failure = new TestFailure("original");

    private readonly Task<Result<double>> _successTask, _failureTask;

    private readonly Func<double, Result> _successFunc = value => Result.Success();
    private readonly Func<double, Result> _failureFunc = _ => new TestFailure("BindTap");
    private readonly Func<double, Result> _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Result<int>> _successOfTFunc = value => (int)value;
    private readonly Func<double, Result<int>> _failureOfTFunc = _ => new TestFailure("BindTap");
    private readonly Func<double, Result<int>> _forbiddenOfTFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result>> _successTaskFunc, _failureTaskFunc, _forbiddenTaskFunc;
    private readonly Func<double, Task<Result<int>>> _successOfTTaskFunc, _failureOfTTaskFunc, _forbiddenOfTTaskFunc;

    public BindTapTValueTests()
    {
        _successTask = Task.FromResult(_success);
        _failureTask = Task.FromResult(_failure);
        _successTaskFunc = value => Task.FromResult(_successFunc(value));
        _failureTaskFunc = value => Task.FromResult(_failureFunc(value));
        _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");
        _successOfTTaskFunc = value => Task.FromResult(_successOfTFunc(value));
        _failureOfTTaskFunc = value => Task.FromResult(_failureOfTFunc(value));
        _forbiddenOfTTaskFunc = _ => throw new InvalidOperationException("This function should not be called");
    }

    [Test]
    public void BindTap_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = _success.BindTap(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = _success.BindTap(_failureFunc);
        result.ShouldBeFailure().Message.ShouldBe("BindTap");
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.BindTap(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public void BindTap_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = _success.BindTap(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = _success.BindTap(_failureOfTFunc);
        result.ShouldBeFailure().Message.ShouldBe("BindTap");
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = _failure.BindTap(_forbiddenOfTFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = await _success.BindTapAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = await _success.BindTapAsync(_failureTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.BindTapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _success.BindTapAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = await _success.BindTapAsync(_failureOfTTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = await _failure.BindTapAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindTap_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.BindTap(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.BindTap(_failureFunc);
        result.ShouldBeFailure().Message.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.BindTap(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindTap_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.BindTap(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.BindTap(_failureOfTFunc);
        result.ShouldBeFailure().Message.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.BindTap(_forbiddenOfTFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.BindTapAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.BindTapAsync(_failureTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.BindTapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.BindTapAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.BindTapAsync(_failureOfTTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("BindTap");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.BindTapAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }
}
