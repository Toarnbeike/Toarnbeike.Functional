using Toarnbeike.Options.TestExtensions;

namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Tests for the <see cref="OrElseExtensions"/>
/// </summary>
public class OrElseExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    private readonly Func<int> _alternative = () => 2;
    private readonly Func<Task<int>> _alternativeAsync = () => Task.FromResult(2);
    private readonly Func<int> _alternativeException = () => throw new ShouldAssertException("Func should not be called.");
    private readonly Func<Task<int>> _alternativeExceptionAsync;

    public OrElseExtensionsTests()
    {
        _alternativeExceptionAsync = () => Task.FromResult(_alternativeException());
    }

    [Fact]
    public void OrElse_ShouldReturnValue_WhenOptionIsSome_WithDefault()
    {
        _some.OrElse(2).ShouldBeSomeWithValue(1);
    }

    [Fact]
    public void OrElse_ShouldReturnValue_WhenOptionIsSome_WithFallbackFunc()
    {
        _some.OrElse(_alternativeException).ShouldBeSomeWithValue(1);
    }

    [Fact]
    public void OrElse_ShouldReturnAlternative_WhenOptionIsNone_WithDefault()
    {
        _none.OrElse(2).ShouldBeSomeWithValue(2);
    }

    [Fact]
    public void OrElse_ShouldReturnAlternative_WhenOptionIsNone_WithFallbackFunc()
    {
        _none.OrElse(_alternative).ShouldBeSomeWithValue(2);
    }

    [Fact]
    public async Task OrElseAsync_ShouldReturnValue_WhenOptionIsSome()
    {
        var result = await _some.OrElseAsync(_alternativeExceptionAsync);
        result.ShouldBeSomeWithValue(1);
    }

    [Fact]
    public async Task OrElseAsync_ShouldReturnAlternative_WhenOptionIsNone()
    {
        var result = await _none.OrElseAsync(_alternativeAsync);
        result.ShouldBeSomeWithValue(2);
    }

    [Fact]
    public async Task OrElse_ShouldReturnValue_WhenTaskOptionIsSome_WithDefault()
    {
        var result = await _someAsync.OrElse(2);
        result.ShouldBeSomeWithValue(1);
    }

    [Fact]
    public async Task OrElse_ShouldReturnAlternative_WhenTaskOptionIsNone_WithDefault()
    {
        var result = await _noneAsync.OrElse(2);
        result.ShouldBeSomeWithValue(2);
    }

    [Fact]
    public async Task OrElse_ShouldReturnValue_WhenTaskOptionIsSome_WithFallbackFunc()
    {
        var result = await _someAsync.OrElse(_alternativeException);
        result.ShouldBeSomeWithValue(1);
    }

    [Fact]
    public async Task OrElse_ShouldReturnAlternative_WhenTaskOptionIsNone_WithFallbackFunc()
    {
        var result = await _noneAsync.OrElse(_alternative);
        result.ShouldBeSomeWithValue(2);
    }

    [Fact]
    public async Task OrElseAsync_ShouldReturnValue_WhenTaskOptionIsSome()
    {
        var result = await _someAsync.OrElseAsync(_alternativeExceptionAsync);
        result.ShouldBeSomeWithValue(1);
    }

    [Fact]
    public async Task OrElseAsync_ShouldReturnAlternative_WhenTaskOptionIsNone()
    {
        var result = await _noneAsync.OrElseAsync(_alternativeAsync);
        result.ShouldBeSomeWithValue(2);
    }
}
