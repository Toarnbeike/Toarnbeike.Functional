using Toarnbeike.Options.TestExtensions;

namespace Toarnbeike.Options.Collection;

public class OptionCollectionExtensionsTests
{
    private readonly IEnumerable<Option<int>> _optionsWithValues = [1, 2, Option.None, 4];
    private readonly IEnumerable<Option<int>> _optionsWithoutValues = [Option.None, Option.None, Option.None];
    private readonly IEnumerable<Option<int>> _optionsWithAllValues = [0, 1, 2, 3];
    private readonly Func<int, bool> _greaterThenTwo = x => x > 2;
    private readonly Func<int, bool> _noMatches = x => x > 4;
    private readonly Func<int, bool> _throwingPredicate = _ => throw new ShouldAssertException("Method should not be called.");

    [Fact]
    public void Values_Should_ReturnOnlyValues()
    {
        var result = _optionsWithValues.Sequence().ToList();

        result.ShouldNotBeNull();
        var list = result.ToList();
        list.Count.ShouldBe(3);
        list.ShouldBe([1, 2, 4]);
    }

    [Fact]
    public void Values_Should_ReturnEmpty_WhenNoValues()
    {
        var result = _optionsWithoutValues.Sequence().ToList();

        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }

    [Fact]
    public void WhereValues_Should_ReturnFilteredValues()
    {
        var result = _optionsWithValues.Sequence(_greaterThenTwo).ToList();
        result.ShouldNotBeNull();
        var list = result.ToList();
        list.Count.ShouldBe(1);
        list.ShouldBe([4]);
    }

    [Fact]
    public void WhereValues_Should_ReturnEmpty_WhenNoValuesMatch()
    {
        var result = _optionsWithValues.Sequence(_noMatches).ToList();
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }

    [Fact]
    public void WhereValues_Should_ReturnEmpty_WhenNoValuesWithoutCheckingPredicate()
    {
        var result = _optionsWithoutValues.Sequence(_throwingPredicate).ToList();
        result.ShouldNotBeNull();
        result.ShouldBeEmpty();
    }

    [Fact]
    public void SelectValues_Should_ProjectValues()
    {
        var result = _optionsWithValues.Sequence(x => x * 2).ToList();
        result.ShouldNotBeNull();
        var list = result.ToList();
        list.Count.ShouldBe(3);
        list.ShouldBe([2, 4, 8]);
    }

    [Fact]
    public void CountValues_Should_ReturnCountOfValues()
    {
        var count = _optionsWithValues.CountValues();
        count.ShouldBe(3);
    }

    [Fact]
    public void CountValues_Should_ReturnZero_WhenNoValues()
    {
        var count = _optionsWithoutValues.CountValues();
        count.ShouldBe(0);
    }

    [Fact]
    public void CountValues_Should_ReturnCountOfValues_WithPredicate()
    {
        var count = _optionsWithValues.CountValues(x => x > 2);
        count.ShouldBe(1);
    }

    [Fact]
    public void CountValues_Should_ReturnZero_WhenNoPredicateMatches()
    {
        var count = _optionsWithValues.CountValues(x => x > 4);
        count.ShouldBe(0);
    }

    [Fact]
    public void CountValues_Should_ReturnZero_WhenNoValues_WithoutCheckingPredicate()
    {
        var count = _optionsWithoutValues.CountValues(_throwingPredicate);
        count.ShouldBe(0);
    }

    [Fact]
    public void AnyValues_Should_ReturnTrue_WhenAnyValueExists()
    {
        var hasValues = _optionsWithValues.AnyValues();
        hasValues.ShouldBeTrue();
    }

    [Fact]
    public void AnyValues_Should_ReturnFalse_WhenNoValues()
    {
        var hasValues = _optionsWithoutValues.AnyValues();
        hasValues.ShouldBeFalse();
    }

    [Fact]
    public void AnyValues_Should_ReturnTrue_WhenAnyValueMatchesPredicate()
    {
        var hasValues = _optionsWithValues.AnyValues(x => x > 2);
        hasValues.ShouldBeTrue();
    }

    [Fact]
    public void AnyValues_Should_ReturnFalse_WhenNoValuesMatchesPredicate()
    {
        var hasValues = _optionsWithValues.AnyValues(x => x > 4);
        hasValues.ShouldBeFalse();
    }

    [Fact]
    public void AnyValues_Should_ReturnFalse_WhenNoValues_WithoutCheckingPredicate()
    {
        var hasValues = _optionsWithoutValues.AnyValues(_throwingPredicate);
        hasValues.ShouldBeFalse();
    }

    [Fact]
    public void AllValues_Should_ReturnTrue_WhenAllValuesExist()
    {
        var allHaveValues = _optionsWithAllValues.AllValues();
        allHaveValues.ShouldBeTrue();
    }

    [Fact]
    public void AllValues_Should_ReturnFalse_WhenNotAllValuesExist()
    {
        var allHaveValues = _optionsWithValues.AllValues();
        allHaveValues.ShouldBeFalse();
    }

    [Fact]
    public void AllValues_Should_ReturnFalse_WhenNoValues()
    {
        var allHaveValues = _optionsWithoutValues.AllValues();
        allHaveValues.ShouldBeFalse();
    }

    [Fact]
    public void AllValues_Should_ReturnTrue_WhenAllValuesMatchPredicate()
    {
        var allMatch = _optionsWithAllValues.AllValues(x => x < 4);
        allMatch.ShouldBeTrue();
    }

    [Fact]
    public void AllValues_Should_ReturnFalse_WhenNotAllValuesMatchPredicate()
    {
        var allMatch = _optionsWithAllValues.AllValues(x => x < 2);
        allMatch.ShouldBeFalse();
    }

    [Fact]
    public void AllValues_Should_ReturnFalse_WhenNoValues_WithoutCheckingPredicate()
    {
        var allHaveValues = _optionsWithoutValues.AllValues(_throwingPredicate);
        allHaveValues.ShouldBeFalse();
    }

    [Fact]
    public void FirstOrNone_Should_ReturnFirstValue_WhenExists()
    {
        var first = _optionsWithValues.FirstOrNone();
        first.ShouldBeSomeWithValue(1);
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
        var first = _optionsWithValues.FirstOrNone(_greaterThenTwo);
        first.ShouldBeSomeWithValue(4);
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
        var last = _optionsWithValues.LastOrNone(_greaterThenTwo);
        last.ShouldBeSomeWithValue(4);
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