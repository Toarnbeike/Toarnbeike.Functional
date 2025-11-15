using Toarnbeike.Eithers.TestExtensions;

namespace Toarnbeike.Eithers.Extensions;

public class MapExtensionsTests
{
    private readonly Either<string, int> _left = Either<string, int>.Left("message");
    private readonly Task<Either<string, int>> _leftAsync = Task.FromResult(Either<string, int>.Left("message"));
    private readonly Either<string, int> _right = Either<string, int>.Right(1);
    private readonly Task<Either<string, int>> _rightAsync = Task.FromResult(Either<string, int>.Right(1));

    private readonly Func<int, int> _rightFunc = right => right * 2;
    private readonly Func<int, Task<int>> _rightFuncAsync = right => Task.FromResult(right * 2);

    [Fact]
    public void Map_Should_RemainLeft_WhenLeft()
    {
        var result = _left.Map(_rightFunc);
        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public void Map_Should_MapRight_WhenRight()
    {
        var result = _right.Map(_rightFunc);
        result.ShouldBeRightWithValue(2);
    }

    [Fact]
    public async Task MapAsync_Should_RemainLeft_WhenLeft()
    {
        var result = await _left.MapAsync(_rightFuncAsync);
        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public async Task MapAsync_Should_MapRight_WhenRight()
    {
        var result = await _right.MapAsync(_rightFuncAsync);
        result.ShouldBeRightWithValue(2);
    }

    [Fact]
    public async Task Map_Should_RemainLeft_WhenLeftAsync()
    {
        var result = await _leftAsync.Map(_rightFunc);
        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public async Task Map_Should_MapRight_WhenRightAsync()
    {
        var result = await _rightAsync.Map(_rightFunc);
        result.ShouldBeRightWithValue(2);
    }

    [Fact]
    public async Task MapAsync_Should_RemainLeft_WhenLeftAsync()
    {
        var result = await _leftAsync.MapAsync(_rightFuncAsync);
        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public async Task MapAsync_Should_MapRight_WhenRightAsync()
    {
        var result = await _rightAsync.MapAsync(_rightFuncAsync);
        result.ShouldBeRightWithValue(2);
    }
}