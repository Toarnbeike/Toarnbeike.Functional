namespace Toarnbeike.Eithers.Extensions;

public class TapLeftExtensionsTests
{
    private readonly Either<string, int> _left = Either<string, int>.Left("message");
    private readonly Task<Either<string, int>> _leftAsync = Task.FromResult(Either<string, int>.Left("message"));
    private readonly Either<string, int> _right = Either<string, int>.Right(1);
    private readonly Task<Either<string, int>> _rightAsync = Task.FromResult(Either<string, int>.Right(1));

    private int _counter = 0;

    private void Increase(string value) => _counter += value.Length;

    private Task IncreaseAsync(string value)
    {
        Increase(value);
        return Task.CompletedTask;
    }

    [Fact]
    public void TapLeft_Should_DoAction_WhenLeft()
    {
        _counter = 0;
        _left.TapLeft(Increase);
        _counter.ShouldBe(7);
    }

    [Fact]
    public void TapLeft_Should_DoNothing_WhenRight()
    {
        _counter = 0;
        _right.TapLeft(Increase);
        _counter.ShouldBe(0);
    }
    
    [Fact]
    public async Task TapLeftAsync_Should_DoNAction_WhenLeft()
    {
        _counter = 0;
        await _left.TapLeftAsync(IncreaseAsync);
        _counter.ShouldBe(7);
    }

    [Fact]
    public async Task TapLeftAsync_Should_DoNothing_WhenRight()
    {
        _counter = 0;
        await _right.TapLeftAsync(IncreaseAsync);
        _counter.ShouldBe(0);
    }
    
    [Fact]
    public async Task TapLeft_Should_DoAction_WhenLeftAsync()
    {
        _counter = 0;
        await _leftAsync.TapLeft(Increase);
        _counter.ShouldBe(7);
    }

    [Fact]
    public async Task TapLeft_Should_DoNothing_WhenRightAsync()
    {
        _counter = 0;
        await _rightAsync.TapLeft(Increase);
        _counter.ShouldBe(0);
    }
    
    [Fact]
    public async Task TapLeftAsync_Should_DoAction_WhenLeftAsync()
    {
        _counter = 0;
        await _leftAsync.TapLeftAsync(IncreaseAsync);
        _counter.ShouldBe(7);
    }

    [Fact]
    public async Task TapLeftAsync_Should_DoNothing_WhenRightAsync()
    {
        _counter = 0;
        await _rightAsync.TapLeftAsync(IncreaseAsync);
        _counter.ShouldBe(0);
    }
}