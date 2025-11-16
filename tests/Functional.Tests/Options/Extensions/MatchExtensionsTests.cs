namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Tests for the <see cref="MatchExtensions"/> class.
/// </summary>
public class MatchExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    private readonly Task<Option<int>> _someAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneAsync = Task.FromResult(Option<int>.None());

    private readonly Func<int, double> _matchValue = x => x / 2.0;
    private readonly Func<double> _matchNone = () => 1.5;
    private readonly Func<int, double> _matchValueException ;
    private readonly Func<double> _matchNoneException = () => throw new ShouldAssertException("Func should not be called.");

    private readonly Func<int, Task<double>> _matchValueAsync;
    private readonly Func<Task<double>> _matchNoneAsync;
    private readonly Func<int, Task<double>> _matchValueExceptionAsync;
    private readonly Func<Task<double>> _matchNoneExceptionAsync;

    public MatchExtensionsTests()
    {
        _matchValueException = _ => _matchNoneException();
        _matchValueAsync = x => Task.FromResult(_matchValue(x));
        _matchNoneAsync = () => Task.FromResult(_matchNone());
        _matchValueExceptionAsync = x => Task.FromResult(_matchNoneException());
        _matchNoneExceptionAsync = () => Task.FromResult(_matchNoneException());
    }

    [Fact]
    public void Match_Should_ReturnSuccessFuncValue_WhenOptionIsSome()
    {
        var result = _some.Match(_matchValue, _matchNoneException);
        result.ShouldBe(0.5);
    }

    [Fact]
    public void Match_Should_ReturnFailureFuncValue_WhenOptionIsNone()
    {
        var result = _none.Match(_matchValueException, _matchNone);
        result.ShouldBe(1.5);
    }

    [Fact]
    public async Task MatchAsync_Should_ReturnSuccessFuncValue_WhenOptionIsSome()
    {
        var result = await _some.MatchAsync(_matchValueAsync, _matchNoneExceptionAsync);
        result.ShouldBe(0.5);
    }

    [Fact]
    public async Task MatchAsync_Should_ReturnFailureFuncValue_WhenOptionInNone()
    {
        var result = await _none.MatchAsync(_matchValueExceptionAsync, _matchNoneAsync);
        result.ShouldBe(1.5);
    }

    [Fact]
    public async Task Match_Should_ReturnSuccessFuncValue_WhenOptionTaskIsSome()
    {
        var result = await _someAsync.Match(_matchValue, _matchNoneException);
        result.ShouldBe(0.5);
    }

    [Fact]
    public async Task Match_Should_ReturnFailureFuncValue_WhenOptionTaskIsNone()
    {
        var result = await _noneAsync.Match(_matchValueException, _matchNone);
        result.ShouldBe(1.5);
    }

    [Fact]
    public async Task MatchAsync_Should_ReturnSuccessFuncValue_WhenOptionTaskIsSome()
    {
        var result = await _someAsync.MatchAsync(_matchValueAsync, _matchNoneExceptionAsync);
        result.ShouldBe(0.5);
    }

    [Fact]
    public async Task MatchAsync_Should_ReturnFailureFuncValue_WhenOptionTaskInNone()
    {
        var result = await _noneAsync.MatchAsync(_matchValueExceptionAsync, _matchNoneAsync);
        result.ShouldBe(1.5);
    }
}
