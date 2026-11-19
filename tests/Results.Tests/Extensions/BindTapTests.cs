using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Tests for the <see cref="BindTapExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class BindTapTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = new TestFailure("original");

    private readonly Task<Result> _successTask, _failureTask;

    private readonly Func<Result> _successFunc = Result.Success;
    private readonly Func<Result> _failureFunc = () => new TestFailure("BindTap");
    private readonly Func<Result> _forbiddenFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Result<int>> _successOfTFunc = () => Result.Success(42);
    private readonly Func<Result<int>> _failureOfTFunc = () => new TestFailure("BindTap");
    private readonly Func<Result<int>> _forbiddenOfTFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Task<Result>> _successTaskFunc, _failureTaskFunc, _forbiddenTaskFunc;

    private readonly Func<Task<Result<int>>> _successOfTTaskFunc, _failureOfTTaskFunc, _forbiddenOfTTaskFunc;

    public BindTapTests()
    {
        _successTask = Task.FromResult(_success);
        _failureTask = Task.FromResult(_failure);
        
        _successTaskFunc = () => Task.FromResult(_successFunc());
        _failureTaskFunc = () => Task.FromResult(_failureFunc());
        _forbiddenTaskFunc = () => throw new InvalidOperationException("This function should not be called");

        _successOfTTaskFunc = () => Task.FromResult(_successOfTFunc());
        _failureOfTTaskFunc = () => Task.FromResult(_failureOfTFunc());
        _forbiddenOfTTaskFunc = () => throw new InvalidOperationException("This function should not be called");
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
