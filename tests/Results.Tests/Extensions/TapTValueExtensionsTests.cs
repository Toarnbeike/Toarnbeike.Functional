using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Tests for the <see cref="TapExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class TapTValueExtensionsTests
{
    private readonly Result<int> _success = Result.Success(3);
    private readonly Result<int> _failure = new TestFailure("original");

    private readonly Task<Result<int>> _successTask, _failureTask;

    public TapTValueExtensionsTests()
    {
        _successTask = Task.FromResult(_success);
        _failureTask = Task.FromResult(_failure);
    }

    [Test]
    public void Tap_ShouldExecute_WhenResultIsSuccess()
    {
        var actual = 0;
        _success.Tap(value => actual = value);
        actual.ShouldBe(3);
    }

    [Test]
    public void Tap_ShouldNotExecute_WhenResultIsFailure()
    {
        var actual = 0;
        _failure.Tap(value => actual = value);
        actual.ShouldBe(0);
    }

    [Test]
    public async Task TapAsync_ShouldExecute_WhenResultIsSuccess()
    {
        var actual = 0;
        await _success.TapAsync(async value => await Task.Run(() => actual = value));
        actual.ShouldBe(3);
    }

    [Test]
    public async Task TapAsync_ShouldNotExecute_WhenResultIsFailure()
    {
        var actual = 0;
        await _failure.TapAsync(async value => await Task.Run(() => actual = value));
        actual.ShouldBe(0);
    }

    [Test]
    public async Task Tap_ShouldExecute_WhenResultTaskIsSuccess()
    {
        var actual = 0;
        await _successTask.Tap(value => actual = value);
        actual.ShouldBe(3);
    }

    [Test]
    public async Task Tap_ShouldNotExecute_WhenResultTaskIsFailure()
    {
        var actual = 0;
        await _failureTask.Tap(value => actual = value);
        actual.ShouldBe(0);
    }

    [Test]
    public async Task TapAsync_ShouldExecute_WhenResultTaskIsSuccess()
    {
        var actual = 0;
        await _successTask.TapAsync(async value => await Task.Run(() => actual = value));
        actual.ShouldBe(3);
    }

    [Test]
    public async Task TapAsync_ShouldNotExecute_WhenResultTaskIsFailure()
    {
        var actual = 0;
        await _failureTask.TapAsync(async value => await Task.Run(() => actual = value));
        actual.ShouldBe(0);
    }
}
