using Toarnbeike.Eithers.TestExtensions;

namespace Toarnbeike.Eithers.Extensions;

public class SwapExtensionsTests
{
    private readonly Either<string, int> _left = Either<string, int>.Left("message");
    private readonly Task<Either<string, int>> _leftAsync = Task.FromResult(Either<string, int>.Left("message"));
    private readonly Either<string, int> _right = Either<string, int>.Right(1);
    private readonly Task<Either<string, int>> _rightAsync = Task.FromResult(Either<string, int>.Right(1));

    [Fact]
    public void Swap_Should_CreateRight_WhenLeft()
    {
        var result = _left.Swap();
        result.ShouldBeRightWithValue("message");
    }

    [Fact]
    public void Swap_Should_CreateLeft_WhenRight()
    {
        var result = _right.Swap();
        result.ShouldBeLeftWithValue(1);
    }
    
    [Fact]
    public async Task SwapAsync_Should_CreateRight_WhenLeft()
    {
        var result = await _leftAsync.SwapAsync();
        result.ShouldBeRightWithValue("message");
    }

    [Fact]
    public async Task SwapAsync_Should_CreateLeft_WhenRight()
    {
        var result = await _rightAsync.SwapAsync();
        result.ShouldBeLeftWithValue(1);
    }
}