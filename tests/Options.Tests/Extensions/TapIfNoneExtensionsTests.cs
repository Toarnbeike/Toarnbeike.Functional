namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Tests for the <see cref="TapIfNoneExtensions"/> class.
/// </summary>
public class TapIfNoneExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;
    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None);

    [Test]
    public void TapIfNone_Should_NotExecuteAction_WhenOptionIsSome()
    {
        var executed = false;
        _some.TapIfNone(() => executed = true);
        executed.ShouldBeFalse();
    }

    [Test]
    public void TapIfNone_Should_ExecuteAction_WhenOptionIsNone()
    {
        var executed = false;
        _none.TapIfNone(() => executed = true);
        executed.ShouldBeTrue();
    }

    [Test]
    public async Task TapIfNoneAsync_Should_NotExecuteAction_WhenOptionIsSome()
    {
        var executed = false;
        await _some.TapIfNoneAsync(() => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeFalse();
    }

    [Test]
    public async Task TapIfNoneAsync_Should_ExecuteAction_WhenOptionIsNone()
    {
        var executed = false;
        await _none.TapIfNoneAsync(() => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeTrue();
    }

    [Test]
    public async Task TapIfNone_Should_NotExecuteAction_WhenOptionTaskIsSome()
    {
        var executed = false;
        await _someAsync.TapIfNone(() => executed = true);
        executed.ShouldBeFalse();
    }

    [Test]
    public async Task TapIfNone_Should_ExecuteAction_WhenOptionTaskIsNone()
    {
        var executed = false;
        await _noneAsync.TapIfNone(() => executed = true);
        executed.ShouldBeTrue();
    }

    [Test]
    public async Task TapIfNoneAsync_Should_NotExecuteAction_WhenOptionTaskIsSome()
    {
        var executed = false;
        await _someAsync.TapIfNoneAsync(() => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeFalse();
    }

    [Test]
    public async Task TapIfNoneAsync_Should_ExecuteAction_WhenOptionTaskIsNone()
    {
        var executed = false;
        await _noneAsync.TapIfNoneAsync(() => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeTrue();
    }
}
