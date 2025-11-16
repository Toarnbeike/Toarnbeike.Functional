using Toarnbeike.Options.TestExtensions;

namespace Toarnbeike.Options.Collection;

public class CollectionExtensionOnGenericEnumerableTests
{
    private readonly IEnumerable<int> _optionsWithValues = [0, 1, 2, 3, 4];
    private readonly IEnumerable<int> _optionsWithoutValues = [];

    private readonly Func<int, bool> _lessThenTwo = x => x < 2;
    private readonly Func<int, bool> _noMatches = x => x > 4;
    private readonly Func<int, bool> _throwingPredicate = _ => throw new ShouldAssertException("Method should not be called.");

    [Fact]
    public void FirstOrNone_Should_ReturnFirstValue_WhenExists()
    {
        var first = _optionsWithValues.FirstOrNone();
        first.ShouldBeSomeWithValue(0);
    }

    [Fact]
    public void FirstOrNone_Should_ReturnNone_WhenNoValues()
    {
        var first = _optionsWithoutValues.FirstOrNone();
        first.ShouldBeNone();
    }

    [Fact]
    public void FirstOrNone_Should_ReturnFirstValue_MatchingPredicate()
    {
        var first = _optionsWithValues.FirstOrNone(_lessThenTwo);
        first.ShouldBeSomeWithValue(0);
    }

    [Fact]
    public void FirstOrNone_Should_ReturnNone_WhenNoValuesMatchPredicate()
    {
        var first = _optionsWithValues.FirstOrNone(_noMatches);
        first.ShouldBeNone();
    }

    [Fact]
    public void FirstOrNone_Should_ReturnNone_WhenNoValues_WithoutCheckingPredicate()
    {
        var first = _optionsWithoutValues.FirstOrNone(_throwingPredicate);
        first.ShouldBeNone();
    }

    [Fact]
    public void LastOrNone_Should_ReturnLastValue_WhenExists()
    {
        var last = _optionsWithValues.LastOrNone();
        last.ShouldBeSomeWithValue(4);
    }

    [Fact]
    public void LastOrNone_Should_ReturnNone_WhenNoValues()
    {
        var last = _optionsWithoutValues.LastOrNone();
        last.ShouldBeNone();
    }

    [Fact]
    public void LastOrNone_Should_ReturnLastValue_MatchingPredicate()
    {
        var last = _optionsWithValues.LastOrNone(_lessThenTwo);
        last.ShouldBeSomeWithValue(1);
    }

    [Fact]
    public void LastOrNone_Should_ReturnNone_WhenNoValuesMatchPredicate()
    {
        var last = _optionsWithValues.LastOrNone(_noMatches);
        last.ShouldBeNone();
    }

    [Fact]
    public void LastOrNone_Should_ReturnNone_WhenNoValues_WithoutCheckingPredicate()
    {
        var last = _optionsWithoutValues.LastOrNone(_throwingPredicate);
        last.ShouldBeNone();
    }
}