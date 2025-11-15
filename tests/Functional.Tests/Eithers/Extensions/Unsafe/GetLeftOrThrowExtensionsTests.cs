namespace Toarnbeike.Eithers.Extensions.Unsafe;

public class GetLeftOrThrowExtensionsTests
{
    private readonly Either<string, int> _left = Either<string, int>.Left("message");
    private readonly Task<Either<string, int>> _leftAsync = Task.FromResult(Either<string, int>.Left("message"));
    private readonly Either<string, int> _right = Either<string, int>.Right(1);
    private readonly Task<Either<string, int>> _rightAsync = Task.FromResult(Either<string, int>.Right(1));

    [Fact]
    public void GetLeftOrThrow_Should_Return_WhenLeft()
    {
        _left.GetLeftOrThrow().ShouldBe("message");
    }

    [Fact]
    public void GetLeftOrThrow_Should_Throw_WhenRight()
    {
        Should.Throw<InvalidOperationException>(() => _right.GetLeftOrThrow());
    }

    [Fact]
    public void GetLeftOrThrow_Should_ThrowCustom_WhenRight()
    {
        var excFunc = () => new Exception("exception");
        Should.Throw<Exception>(() => _right.GetLeftOrThrow(excFunc)).Message.ShouldBe("exception");
    }
    
    [Fact]
    public async Task GetLeftOrThrowAsync_Should_Return_WhenLeft()
    {
        (await _leftAsync.GetLeftOrThrowAsync()).ShouldBe("message");
    }

    [Fact]
    public async Task GetLeftOrThrowAsync_Should_Throw_WhenRight()
    {
        await Should.ThrowAsync<InvalidOperationException>(() => _rightAsync.GetLeftOrThrowAsync());
    }

    [Fact]
    public async Task GetLeftOrThrowAsync_Should_ThrowCustom_WhenRight()
    {
        var excFunc = () => new Exception("exception");
        (await Should.ThrowAsync<Exception>(() => _rightAsync.GetLeftOrThrowAsync(excFunc)))
            .Message.ShouldBe("exception");
    }
}