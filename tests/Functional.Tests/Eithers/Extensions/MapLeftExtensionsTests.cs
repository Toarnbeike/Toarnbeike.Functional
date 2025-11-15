using Toarnbeike.Eithers.TestExtensions;

namespace Toarnbeike.Eithers.Extensions;

public class MapLeftExtensionsTests
{
    private readonly Either<string, int> _left = Either<string, int>.Left("message");
    private readonly Task<Either<string, int>> _leftAsync = Task.FromResult(Either<string, int>.Left("message"));
    private readonly Either<string, int> _right = Either<string, int>.Right(1);
    private readonly Task<Either<string, int>> _rightAsync = Task.FromResult(Either<string, int>.Right(1));

    private readonly Func<string, string> _leftFunc = left => left + " MapLeftped";
    private readonly Func<string, Task<string>> _leftFuncAsync = left => Task.FromResult(left + " MapLeftped");

    [Fact]
    public void MapLeft_Should_MapLeft_WhenLeft()
    {
        var result = _left.MapLeft(_leftFunc);
        result.ShouldBeLeftWithValue("message MapLeftped");
    }

    [Fact]
    public void MapLeft_Should_RemainRight_WhenRight()
    {
        var result = _right.MapLeft(_leftFunc);
        result.ShouldBeRightWithValue(1);
    }

    [Fact]
    public async Task MapLeftAsync_Should_MapLeft_WhenLeft()
    {
        var result = await _left.MapLeftAsync(_leftFuncAsync);
        result.ShouldBeLeftWithValue("message MapLeftped");
    }

    [Fact]
    public async Task MapLeftAsync_Should_RemainRight_WhenRight()
    {
        var result = await _right.MapLeftAsync(_leftFuncAsync);
        result.ShouldBeRightWithValue(1);
    }

    [Fact]
    public async Task MapLeft_Should_MapLeft_WhenLeftAsync()
    {
        var result = await _leftAsync.MapLeft(_leftFunc);
        result.ShouldBeLeftWithValue("message MapLeftped");
    }

    [Fact]
    public async Task MapLeft_Should_RemainRight_WhenRightAsync()
    {
        var result = await _rightAsync.MapLeft(_leftFunc);
        result.ShouldBeRightWithValue(1);
    }

    [Fact]
    public async Task MapLeftAsync_Should_MapLeft_WhenLeftAsync()
    {
        var result = await _leftAsync.MapLeftAsync(_leftFuncAsync);
        result.ShouldBeLeftWithValue("message MapLeftped");
    }

    [Fact]
    public async Task MapLeftAsync_Should_RemainRight_WhenRightAsync()
    {
        var result = await _rightAsync.MapLeftAsync(_leftFuncAsync);
        result.ShouldBeRightWithValue(1);
    }
}