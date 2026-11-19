using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Tests for the <see cref="TapExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class TapExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = new TestFailure("original");

    private readonly Task<Result> _successTask, _failureTask;

    public TapExtensionsTests()
    {
        _successTask = Task.FromResult(_success);
        _failureTask = Task.FromResult(_failure);
    }

    [Test]
    public void Tap_ShouldExecute_WhenResultIsSuccess()
    {
        var isExecuted = false;
        _success.Tap(() => isExecuted = true);
        isExecuted.ShouldBeTrue();
    }

    [Test]
    public void Tap_ShouldNotExecute_WhenResultIsFailure ()
    {
        var isExecuted = false;
        _failure.Tap(() => isExecuted = true);
        isExecuted.ShouldBeFalse();
    }

    [Test]
    public async Task TapAsync_ShouldExecute_WhenResultIsSuccess()
    {
        var isExecuted = false;
        await _success.TapAsync(async () => await Task.Run(() => isExecuted = true));
        isExecuted.ShouldBeTrue();
    }

    [Test]
    public async Task TapAsync_ShouldNotExecute_WhenResultIsFailure()
    {
        var isExecuted = false;
        await _failure.TapAsync(async () => await Task.Run(() => isExecuted = true));
        isExecuted.ShouldBeFalse();
    }

    [Test]
    public async Task Tap_ShouldExecute_WhenResultTaskIsSuccess()
    {
        var isExecuted = false;
        await _successTask.Tap(() => isExecuted = true);
        isExecuted.ShouldBeTrue();
    }

    [Test]
    public async Task Tap_ShouldNotExecute_WhenResultTaskIsFailure()
    {
        var isExecuted = false;
        await _failureTask.Tap(() => isExecuted = true);
        isExecuted.ShouldBeFalse();
    }

    [Test]
    public async Task TapAsync_ShouldExecute_WhenResultTaskIsSuccess()
    {
        var isExecuted = false;
        await _successTask.TapAsync(async () => await Task.Run(() => isExecuted = true));
        isExecuted.ShouldBeTrue();
    }

    [Test]
    public async Task TapAsync_ShouldNotExecute_WhenResultTaskIsFailure()
    {
        var isExecuted = false;
        await _failureTask.TapAsync(async () => await Task.Run(() => isExecuted = true));
        isExecuted.ShouldBeFalse();
    }
}
