using Toarnbeike.Eithers.TestExtensions;

namespace Toarnbeike.Eithers.Extensions;

public class BindExtensionsTests
{
    private readonly Either<string, int> _left = Either<string, int>.Left("message");
    private readonly Task<Either<string, int>> _leftAsync = Task.FromResult(Either<string, int>.Left("message"));
    private readonly Either<string, int> _right = Either<string, int>.Right(1);
    private readonly Task<Either<string, int>> _rightAsync = Task.FromResult(Either<string, int>.Right(1));
    
    private readonly Func<int, Either<string, int>> _bind = right => Either<string, int>.Right(right * 2);
    private readonly Func<int, Task<Either<string, int>>> _bindAsync = right => Task.FromResult(Either<string, int>.Right(right * 2));

    [Fact]
    public void Bind_Should_RemainLeft_WhenLeft()
    {
        var result = _left.Bind(_bind);
        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public void Bind_Should_TakeBindResult_WhenRight()
    {
        var result = _right.Bind(_bind);
        result.ShouldBeRightWithValue(2);
    }
    
    [Fact]
    public async Task BindAsync_Should_RemainLeft_WhenLeft()
    {
        var result = await _left.BindAsync(_bindAsync);
        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public async Task BindAsync_Should_TakeBindAsyncResult_WhenRight()
    {
        var result = await _right.BindAsync(_bindAsync);
        result.ShouldBeRightWithValue(2);
    }
    
    [Fact]
    public async Task Bind_Should_RemainLeft_WhenLeftAsync()
    {
        var result = await _leftAsync.Bind(_bind);
        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public async Task Bind_Should_TakeBindResult_WhenRightAsync()
    {
        var result = await _rightAsync.Bind(_bind);
        result.ShouldBeRightWithValue(2);
    }
    
    [Fact]
    public async Task BindAsync_Should_RemainLeft_WhenLeftAsync()
    {
        var result = await _leftAsync.BindAsync(_bindAsync);
        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public async Task BindAsync_Should_TakeBindAsyncResult_WhenRightAsync()
    {
        var result = await _rightAsync.BindAsync(_bindAsync);
        result.ShouldBeRightWithValue(2);
    }
}