namespace Toarnbeike.Eithers.Extensions;

public class MatchExtensionsTests
{
    private readonly Either<string, int> _left = Either<string, int>.Left("message");
    private readonly Task<Either<string, int>> _leftAsync = Task.FromResult(Either<string, int>.Left("message"));
    private readonly Either<string, int> _right = Either<string, int>.Right(1);
    private readonly Task<Either<string, int>> _rightAsync = Task.FromResult(Either<string, int>.Right(1));

    private int _leftCounter = 0;
    private int _rightCounter = 0;
    

    private void IncreaseLeft(string left) => _leftCounter++;
    private void IncreaseRight(int right) => _rightCounter++;
    
    private Task IncreaseLeftAsync(string left)
    {
        _leftCounter++;
        return Task.CompletedTask;
    }

    private Task IncreaseRightAsync(int right)
    {
        _rightCounter++;
        return Task.CompletedTask;
    }


    
    [Fact]
    public void Match_Should_PerformLeftAction_WhenLeft()
    {
        _leftCounter = 0;
        _rightCounter = 0;
        _left.Match(IncreaseLeft, IncreaseRight);
        _leftCounter.ShouldBe(1);
        _rightCounter.ShouldBe(0);
    }
    
    [Fact]
    public void Match_Should_PerformRightAction_WhenRight()
    {
        _leftCounter = 0;
        _rightCounter = 0;
        _right.Match(IncreaseLeft, IncreaseRight);
        _leftCounter.ShouldBe(0);
        _rightCounter.ShouldBe(1);
    }
    
    [Fact]
    public async Task MatchAsync_Should_PerformLeftAction_WhenLeft()
    {
        _leftCounter = 0;
        _rightCounter = 0;
        await _left.MatchAsync(IncreaseLeftAsync, IncreaseRightAsync);
        _leftCounter.ShouldBe(1);
        _rightCounter.ShouldBe(0);
    }
    
    [Fact]
    public async Task MatchAsync_Should_PerformRightAction_WhenRight()
    {
        _leftCounter = 0;
        _rightCounter = 0;
        await _right.MatchAsync(IncreaseLeftAsync, IncreaseRightAsync);
        _leftCounter.ShouldBe(0);
        _rightCounter.ShouldBe(1);
    }
    
    [Fact]
    public void Match_Should_ApplyLeft_WhenLeft()
    {
        var result = _left.Match(left => left.Length, right => right * 2);
        result.ShouldBe(7);
    }
    
    [Fact]
    public void Match_Should_ApplyRight_WhenRight()
    {
        var result = _right.Match(left => left.Length, right => right * 2);
        result.ShouldBe(2);
    }
    
    [Fact]
    public async Task MatchAsync_Should_ApplyLeft_WhenLeft()
    {
        var result = await _left.MatchAsync(left => Task.FromResult(left.Length), right => Task.FromResult(right * 2));
        result.ShouldBe(7);
    }
    
    [Fact]
    public async Task MatchAsync_Should_ApplyRight_WhenRight()
    {
        var result = await _right.MatchAsync(left => Task.FromResult(left.Length), right => Task.FromResult(right * 2));
        result.ShouldBe(2);
    }
    
    [Fact]
    public async Task Match_Should_PerformLeftAction_WhenLeftAsync()
    {
        _leftCounter = 0;
        _rightCounter = 0;
        await _leftAsync.Match(IncreaseLeft, IncreaseRight);
        _leftCounter.ShouldBe(1);
        _rightCounter.ShouldBe(0);
    }
    
    [Fact]
    public async Task Match_Should_PerformRightAction_WhenRightAsync()
    {
        _leftCounter = 0;
        _rightCounter = 0;
        await _rightAsync.Match(IncreaseLeft, IncreaseRight);
        _leftCounter.ShouldBe(0);
        _rightCounter.ShouldBe(1);
    }
    
    [Fact]
    public async Task MatchAsync_Should_PerformLeftAction_WhenLeftAsync()
    {
        _leftCounter = 0;
        _rightCounter = 0;
        await _leftAsync.MatchAsync(IncreaseLeftAsync, IncreaseRightAsync);
        _leftCounter.ShouldBe(1);
        _rightCounter.ShouldBe(0);
    }
    
    [Fact]
    public async Task MatchAsync_Should_PerformRightAction_WhenRightAsync()
    {
        _leftCounter = 0;
        _rightCounter = 0;
        await _rightAsync.MatchAsync(IncreaseLeftAsync, IncreaseRightAsync);
        _leftCounter.ShouldBe(0);
        _rightCounter.ShouldBe(1);
    }
    
    [Fact]
    public async Task Match_Should_ApplyLeft_WhenLeftAsync()
    {
        var result = await _leftAsync.Match(left => left.Length, right => right * 2);
        result.ShouldBe(7);
    }
    
    [Fact]
    public async Task Match_Should_ApplyRight_WhenRightAsync()
    {
        var result = await _rightAsync.Match(left => left.Length, right => right * 2);
        result.ShouldBe(2);
    }
    
    [Fact]
    public async Task MatchAsync_Should_ApplyLeft_WhenLeftAsync()
    {
        var result = await _leftAsync.MatchAsync(left => Task.FromResult(left.Length), right => Task.FromResult(right * 2));
        result.ShouldBe(7);
    }
    
    [Fact]
    public async Task MatchAsync_Should_ApplyRight_WhenRightAsync()
    {
        var result = await _rightAsync.MatchAsync(left => Task.FromResult(left.Length), right => Task.FromResult(right * 2));
        result.ShouldBe(2);
    }
}