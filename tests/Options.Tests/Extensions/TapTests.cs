namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Tests for the <see cref="TapExtensions"/> class.
/// </summary>
public class TapTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;
    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None);

    [Test]
    public void Tap_Should_ExecuteAction_WhenOptionIsSome()
    {
        var executed = false;
        _some.Tap(x => executed = true);
        executed.ShouldBeTrue();
    }

    [Test]
    public void Tap_Should_NotExecuteAction_WhenOptionIsNone()
    {
        var executed = false;
        _none.Tap(x => executed = true);
        executed.ShouldBeFalse();
    }

    [Test]
    public async Task TapAsync_Should_ExecuteAction_WhenOptionIsSome()
    {
        var executed = false;
        await _some.TapAsync(x => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeTrue();
    }

    [Test]
    public async Task TapAsync_Should_NotExecuteAction_WhenOptionIsNone()
    {
        var executed = false;
        await _none.TapAsync(x => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeFalse();
    }

    [Test]
    public async Task Tap_Should_ExecuteAction_WhenOptionTaskIsSome()
    {
        var executed = false;
        await _someAsync.Tap(x => executed = true);
        executed.ShouldBeTrue();
    }

    [Test]
    public async Task Tap_Should_NotExecuteAction_WhenOptionTaskIsNone()
    {
        var executed = false;
        await _noneAsync.Tap(x => executed = true);
        executed.ShouldBeFalse();
    }

    [Test]
    public async Task TapAsync_Should_ExecuteAction_WhenOptionTaskIsSome()
    {
        var executed = false;
        await _someAsync.TapAsync(x => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeTrue();
    }

    [Test]
    public async Task TapAsync_Should_NotExecuteAction_WhenOptionTaskIsNone()
    {
        var executed = false;
        await _noneAsync.TapAsync(x => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeFalse();
    }
}
