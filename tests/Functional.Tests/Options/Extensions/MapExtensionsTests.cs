using Toarnbeike.Options.TestExtensions;

namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Test for the <see cref="MapExtensions"/> class.
/// </summary>
public class MapExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    private readonly Func<int, double> _selectHalf = x => x / 2.0;
    private readonly Func<int, double> _selectException = x => throw new ShouldAssertException("Func should not be called.");

    private readonly Func<int, Task<double>> _selectHalfAsync;
    private readonly Func<int, Task<double>> _selectExceptionAsync;

    public MapExtensionsTests()
    {
        _selectHalfAsync = x => Task.FromResult(_selectHalf(x));
        _selectExceptionAsync = x => Task.FromResult(_selectException(x));
    }

    [Fact]
    public void Map_Should_ReturnSome_WhenOptionIsSome()
    {
        var result = _some.Map(_selectHalf);
        result.ShouldBeSomeWithValue(0.5);
    }

    [Fact]
    public void Map_Should_ReturnNone_WhenOptionIsNone()
    {
        var result = _none.Map(_selectException);
        result.ShouldBeNone();
    }

    [Fact]
    public async Task MapAsync_Should_ReturnSome_WhenOptionIsSome()
    {
        var result = await _some.MapAsync(_selectHalfAsync);
        result.ShouldBeSomeWithValue(0.5);
    }

    [Fact]
    public async Task MapAsync_Should_ReturnNone_WhenOptionIsNone()
    {
        var result = await _none.MapAsync(_selectExceptionAsync);
        result.ShouldBeNone();
    }

    [Fact]
    public async Task Map_Should_ReturnSome_WhenOptionTaskIsSome()
    {
        var result = await _someAsync.Map(_selectHalf);
        result.ShouldBeSomeWithValue(0.5);
    }

    [Fact]
    public async Task Map_Should_ReturnNone_WhenOptionTaskIsNone()
    {
        var result = await _noneAsync.Map(_selectException);
        result.ShouldBeNone();
    }

    [Fact]
    public async Task MapAsync_Should_ReturnSome_WhenOptionTaskIsSome()
    {
        var result = await _someAsync.MapAsync(_selectHalfAsync);
        result.ShouldBeSomeWithValue(0.5);
    }

    [Fact]
    public async Task MapAsync_Should_ReturnNone_WhenOptionTaskIsNone()
    {
        var result = await _noneAsync.MapAsync(_selectExceptionAsync);
        result.ShouldBeNone();
    }
}
