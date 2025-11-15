namespace Toarnbeike.Eithers.Extensions.Unsafe;

public class GetOrThrowExtensionsTests
{
    private readonly Either<string, int> _left = Either<string, int>.Left("message");
    private readonly Task<Either<string, int>> _leftAsync = Task.FromResult(Either<string, int>.Left("message"));
    private readonly Either<string, int> _right = Either<string, int>.Right(1);
    private readonly Task<Either<string, int>> _rightAsync = Task.FromResult(Either<string, int>.Right(1));
    
    [Fact]
    public void GetOrThrow_Should_Return_WhenRight()
    {
        _right.GetOrThrow().ShouldBe(1);
    }

    [Fact]
    public void GetOrThrow_Should_Throw_WhenLeft()
    {
        Should.Throw<InvalidOperationException>(() => _left.GetOrThrow());
    }

    [Fact]
    public void GetOrThrow_Should_ThrowCustom_WhenLeft()
    {
        var excFunc = () => new Exception("exception");
        Should.Throw<Exception>(() => _left.GetOrThrow(excFunc)).Message.ShouldBe("exception");
    }
    
    [Fact]
    public async Task GetOrThrowAsync_Should_Return_WhenRight()
    {
        (await _rightAsync.GetOrThrowAsync()).ShouldBe(1);
    }

    [Fact]
    public async Task GetOrThrowAsync_Should_Throw_WhenLeft()
    {
        await Should.ThrowAsync<InvalidOperationException>(() => _leftAsync.GetOrThrowAsync());
    }

    [Fact]
    public async Task GetOrThrowAsync_Should_ThrowCustom_WhenLeft()
    {
        var excFunc = () => new Exception("exception");
        (await Should.ThrowAsync<Exception>(() => _leftAsync.GetOrThrowAsync(excFunc)))
            .Message.ShouldBe("exception");
    }
}