namespace Toarnbeike.Eithers.Extensions;

public class TapExtensionsTests
{
    private readonly Either<string, int> _left = Either<string, int>.Left("message");
    private readonly Task<Either<string, int>> _leftAsync = Task.FromResult(Either<string, int>.Left("message"));
    private readonly Either<string, int> _right = Either<string, int>.Right(1);
    private readonly Task<Either<string, int>> _rightAsync = Task.FromResult(Either<string, int>.Right(1));

    private int _counter = 0;

    private void Increase(int value) => _counter += value;

    private Task IncreaseAsync(int value)
    {
        Increase(value);
        return Task.CompletedTask;
    }

    [Fact]
    public void Tap_Should_DoNothing_WhenLeft()
    {
        _counter = 0;
        _left.Tap(Increase);
        _counter.ShouldBe(0);
    }

    [Fact]
    public void Tap_Should_DoAction_WhenRight()
    {
        _counter = 0;
        _right.Tap(Increase);
        _counter.ShouldBe(1);
    }
    
    [Fact]
    public async Task TapAsync_Should_DoNothing_WhenLeft()
    {
        _counter = 0;
        await _left.TapAsync(IncreaseAsync);
        _counter.ShouldBe(0);
    }

    [Fact]
    public async Task TapAsync_Should_DoAction_WhenRight()
    {
        _counter = 0;
        await _right.TapAsync(IncreaseAsync);
        _counter.ShouldBe(1);
    }
    
    [Fact]
    public async Task Tap_Should_DoNothing_WhenLeftAsync()
    {
        _counter = 0;
        await _leftAsync.Tap(Increase);
        _counter.ShouldBe(0);
    }

    [Fact]
    public async Task Tap_Should_DoAction_WhenRightAsync()
    {
        _counter = 0;
        await _rightAsync.Tap(Increase);
        _counter.ShouldBe(1);
    }
    
    [Fact]
    public async Task TapAsync_Should_DoNothing_WhenLeftAsync()
    {
        _counter = 0;
        await _leftAsync.TapAsync(IncreaseAsync);
        _counter.ShouldBe(0);
    }

    [Fact]
    public async Task TapAsync_Should_DoAction_WhenRightAsync()
    {
        _counter = 0;
        await _rightAsync.TapAsync(IncreaseAsync);
        _counter.ShouldBe(1);
    }
}