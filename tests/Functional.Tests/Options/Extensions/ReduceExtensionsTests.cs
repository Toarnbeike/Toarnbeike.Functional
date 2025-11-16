namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Tests for the <see cref="ReduceExtensions"/>
/// </summary>
public class ReduceExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    private readonly Func<int> _orElse = () => 2;
    private readonly Func<Task<int>> _orElseAsync = () => Task.FromResult(2);
    private readonly Func<int> _orElseException = () => throw new ShouldAssertException("Func should not be called.");
    private readonly Func<Task<int>> _orElseExceptionAsync;

    public ReduceExtensionsTests() 
    {
        _orElseExceptionAsync = () => Task.FromResult(_orElseException());
    }

    [Fact]
    public void Reduce_ShouldReturnValue_WhenOptionIsSome_WithDefault()
    {
        _some.Reduce(2).ShouldBe(1);
    }

    [Fact]
    public void Reduce_ShouldReturnValue_WhenOptionIsSome_WithFallbackFunc()
    {
        _some.Reduce(_orElseException).ShouldBe(1);
    }

    [Fact]
    public void Reduce_ShouldReturnAlternative_WhenOptionIsNone_WithDefault()
    {
        _none.Reduce(2).ShouldBe(2);
    }

    [Fact]
    public void Reduce_ShouldReturnAlternative_WhenOptionIsNone_WithFallbackFunc()
    {
        _none.Reduce(_orElse).ShouldBe(2);
    }

    [Fact]
    public async Task ReduceAsync_ShouldReturnValue_WhenOptionIsSome()
    {
        var result = await _some.ReduceAsync(_orElseExceptionAsync);
        result.ShouldBe(1);
    }

    [Fact]
    public async Task ReduceAsync_ShouldReturnAlternative_WhenOptionIsNone()
    {
        var result = await _none.ReduceAsync(_orElseAsync);
        result.ShouldBe(2);
    }

    [Fact]
    public async Task Reduce_ShouldReturnValue_WhenTaskOptionIsSome_WithDefault()
    {
        var result = await _someAsync.Reduce(2);
        result.ShouldBe(1);
    }

    [Fact]
    public async Task Reduce_ShouldReturnAlternative_WhenTaskOptionIsNone_WithDefault()
    {
        var result = await _noneAsync.Reduce(2);
        result.ShouldBe(2);
    }

    [Fact]
    public async Task Reduce_ShouldReturnValue_WhenTaskOptionIsSome_WithFallbackFunc()
    {
        var result = await _someAsync.Reduce(_orElseException);
        result.ShouldBe(1);
    }

    [Fact]
    public async Task Reduce_ShouldReturnAlternative_WhenTaskOptionIsNone_WithFallbackFunc()
    {
        var result = await _noneAsync.Reduce(_orElse);
        result.ShouldBe(2);
    }

    [Fact]
    public async Task ReduceAsync_ShouldReturnValue_WhenTaskOptionIsSome()
    {
        var result = await _someAsync.ReduceAsync(_orElseExceptionAsync);
        result.ShouldBe(1);
    }

    [Fact]
    public async Task ReduceAsync_ShouldReturnAlternative_WhenTaskOptionIsNone()
    {
        var result = await _noneAsync.ReduceAsync(_orElseAsync);
        result.ShouldBe(2);
    }
}
