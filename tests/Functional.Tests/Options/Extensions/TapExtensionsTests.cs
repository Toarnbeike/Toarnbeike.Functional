using Toarnbeike.Options.TestExtensions;

namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Tests for the <see cref="TapExtensions"/> class.
/// </summary>
public class TapExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;
    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    [Fact]
    public void Tap_Should_ExecuteAction_WhenOptionIsSome()
    {
        var executed = false;
        _some.Tap(_ => executed = true).ShouldBeSomeWithValue(1);
        executed.ShouldBeTrue();
    }

    [Fact]
    public void Tap_Should_NotExecuteAction_WhenOptionIsNone()
    {
        var executed = false;
        _none.Tap(_ => executed = true).ShouldBeNone();
        executed.ShouldBeFalse();
    }

    [Fact]
    public async Task TapAsync_Should_ExecuteAction_WhenOptionIsSome()
    {
        var executed = false;
        await _some.TapAsync(_ => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeTrue();
    }

    [Fact]
    public async Task TapAsync_Should_NotExecuteAction_WhenOptionIsNone()
    {
        var executed = false;
        await _none.TapAsync(_ => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeFalse();
    }

    [Fact]
    public async Task Tap_Should_ExecuteAction_WhenOptionTaskIsSome()
    {
        var executed = false;
        await _someAsync.Tap(_ => executed = true);
        executed.ShouldBeTrue();
    }

    [Fact]
    public async Task Tap_Should_NotExecuteAction_WhenOptionTaskIsNone()
    {
        var executed = false;
        await _noneAsync.Tap(_ => executed = true);
        executed.ShouldBeFalse();
    }

    [Fact]
    public async Task TapAsync_Should_ExecuteAction_WhenOptionTaskIsSome()
    {
        var executed = false;
        await _someAsync.TapAsync(_ => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeTrue();
    }

    [Fact]
    public async Task TapAsync_Should_NotExecuteAction_WhenOptionTaskIsNone()
    {
        var executed = false;
        await _noneAsync.TapAsync(_ => { executed = true; return Task.CompletedTask; });
        executed.ShouldBeFalse();
    }
}
