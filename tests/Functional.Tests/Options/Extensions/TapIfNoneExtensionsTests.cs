using Toarnbeike.Options.TestExtensions;

namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Tests for the <see cref="TapIfNoneExtensions"/> class.
/// </summary>
public class TapIfNoneExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;
    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    [Fact]
    public void TapIfNone_Should_NotExecuteAction_WhenOptionIsSome()
    {
        var executed = false;
        _some.TapIfNone(() => executed = true).ShouldBeSomeWithValue(1);
        executed.ShouldBeFalse();
    }

    [Fact]
    public void TapIfNone_Should_ExecuteAction_WhenOptionIsNone()
    {
        var executed = false;
        _none.TapIfNone(() => executed = true).ShouldBeNone();
        executed.ShouldBeTrue();
    }

    [Fact]
    public async Task TapIfNoneAsync_Should_NotExecuteAction_WhenOptionIsSome()
    {
        var executed = false;
        await _some.TapIfNoneAsync(() => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeFalse();
    }

    [Fact]
    public async Task TapIfNoneAsync_Should_ExecuteAction_WhenOptionIsNone()
    {
        var executed = false;
        await _none.TapIfNoneAsync(() => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeTrue();
    }

    [Fact]
    public async Task TapIfNone_Should_NotExecuteAction_WhenOptionTaskIsSome()
    {
        var executed = false;
        await _someAsync.TapIfNone(() => executed = true);
        executed.ShouldBeFalse();
    }

    [Fact]
    public async Task TapIfNone_Should_ExecuteAction_WhenOptionTaskIsNone()
    {
        var executed = false;
        await _noneAsync.TapIfNone(() => executed = true);
        executed.ShouldBeTrue();
    }

    [Fact]
    public async Task TapIfNoneAsync_Should_NotExecuteAction_WhenOptionTaskIsSome()
    {
        var executed = false;
        await _someAsync.TapIfNoneAsync(() => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeFalse();
    }

    [Fact]
    public async Task TapIfNoneAsync_Should_ExecuteAction_WhenOptionTaskIsNone()
    {
        var executed = false;
        await _noneAsync.TapIfNoneAsync(() => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeTrue();
    }
}
