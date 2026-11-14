using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Options.Collections;

/// <summary>
/// Tests are deliberately written to use an <see langword="int"/> because with structs 
/// a distinction is made between default and not having a value.
/// E.g. the first element of an <see cref="IEnumerable{int}"/> might return 0,
/// which is explicitly not the same as <c>Option{int}.None()</c>.
/// </summary>
public class EnumerableOptionExtensionsTests
{
    private readonly IEnumerable<int> _collectionWithValues = [0, 1, 2, 3, 4];
    private readonly IEnumerable<int?> _nullableCollectionWithStructs = [null, 1, 2, null, 4];
    private readonly IEnumerable<int> _optionsWithoutValues = [];
    private readonly IEnumerable<int?> _nullableCollectionWithoutValues = [null, null, null];

    private readonly Func<int, bool> _lessThenTwo = x => x < 2;
    private readonly Func<int, bool> _noMatches = x => x > 4;
    private readonly Func<int, bool> _throwingPredicate = _ => throw new ShouldAssertException("Method should not be called.");
    private readonly Func<int?, bool> _throwingNullablePredicate = _ => throw new ShouldAssertException("Method should not be called.");


    [Test]
    public void FirstOrNone_Should_ReturnFirstValue_WhenExists()
    {
        var first = _collectionWithValues.FirstOrNone();
        first.ShouldBe(0);
    }

    [Test]
    public void FirstOrNone_Should_ReturnNone_WhenNoValues()
    {
        var first = _optionsWithoutValues.FirstOrNone();
        first.ShouldBeNone();
    }

    [Test]
    public void FirstOrNone_Should_ReturnFirstValue_FromNullable_WhenExists()
    {
        var first = _nullableCollectionWithStructs.FirstOrNone();
        first.ShouldBe(1);
    }

    [Test]
    public void FirstOrNone_Should_ReturnNone_FromNullable_WhenNoValues()
    {
        var first = _nullableCollectionWithoutValues.FirstOrNone();
        first.ShouldBeNone();
    }

    [Test]
    public void FirstOrNone_Should_ReturnFirstValue_MatchingPredicate()
    {
        var first = _collectionWithValues.FirstOrNone(_lessThenTwo);
        first.ShouldBe(0);
    }

    [Test]
    public void FirstOrNone_Should_ReturnNone_WhenNoValuesMatchPredicate()
    {
        var first = _collectionWithValues.FirstOrNone(_noMatches);
        first.ShouldBeNone();
    }

    [Test]
    public void FirstOrNone_Should_ReturnNone_WhenNoValues_WithoutCheckingPredicate()
    {
        var first = _optionsWithoutValues.FirstOrNone(_throwingPredicate);
        first.ShouldBeNone();
    }

    [Test]
    public void FirstOrNone_Should_ReturnFirstValue_FromNullable_MatchingPredicate()
    {
        var first = _nullableCollectionWithStructs.FirstOrNone(x => x > 2);
        first.ShouldBe(4);
    }

    [Test]
    public void FirstOrNone_Should_ReturnNone_FromNullable_WhenNoValuesMatchPredicate()
    {
        var first = _nullableCollectionWithStructs.FirstOrNone(x => x > 4);
        first.ShouldBeNone();
    }

    [Test]
    public void FirstOrNone_Should_ReturnNone_FromNullable_WhenNoValues_WithoutCheckingPredicate()
    {
        var first = _nullableCollectionWithoutValues.FirstOrNone(_throwingNullablePredicate);
        first.ShouldBeNone();
    }

    [Test]
    public void LastOrNone_Should_ReturnLastValue_WhenExists()
    {
        var last = _collectionWithValues.LastOrNone();
        last.ShouldBe(4);
    }

    [Test]
    public void LastOrNone_Should_ReturnNone_WhenNoValues()
    {
        var last = _optionsWithoutValues.LastOrNone();
        last.ShouldBeNone();
    }

    [Test]
    public void LastOrNone_Should_ReturnLastValue_FromNullable_WhenExists()
    {
        var last = _nullableCollectionWithStructs.LastOrNone();
        last.ShouldBe(4);
    }

    [Test]
    public void LastOrNone_Should_ReturnNone_FromNullable_WhenNoValues()
    {
        var last = _nullableCollectionWithoutValues.LastOrNone();
        last.ShouldBeNone();
    }

    [Test]
    public void LastOrNone_Should_ReturnLastValue_MatchingPredicate()
    {
        var last = _collectionWithValues.LastOrNone(_lessThenTwo);
        last.ShouldBe(1);
    }

    [Test]
    public void LastOrNone_Should_ReturnNone_WhenNoValuesMatchPredicate()
    {
        var last = _collectionWithValues.LastOrNone(_noMatches);
        last.ShouldBeNone();
    }

    [Test]
    public void LastOrNone_Should_ReturnNone_WhenNoValues_WithoutCheckingPredicate()
    {
        var last = _optionsWithoutValues.LastOrNone(_throwingPredicate);
        last.ShouldBeNone();
    }

    [Test]
    public void LastOrNone_Should_ReturnLastValue_FromNullable_MatchingPredicate()
    {
        var last = _nullableCollectionWithStructs.LastOrNone(x => x < 4);
        last.ShouldBe(2);
    }

    [Test]
    public void LastOrNone_Should_ReturnNone_FromNullable_WhenNoValuesMatchPredicate()
    {
        var last = _nullableCollectionWithStructs.LastOrNone(x => x > 4);
        last.ShouldBeNone();
    }

    [Test]
    public void LastOrNone_Should_ReturnNone_FromNullable_WhenNoValues_WithoutCheckingPredicate()
    {
        var last = _nullableCollectionWithoutValues.LastOrNone(_throwingNullablePredicate);
        last.ShouldBeNone();
    }

    [Test]
    public void SingleOrNone_Should_ReturnSingleValue_WhenExists()
    {
        IEnumerable<int> collection = [1];
        var single = collection.SingleOrNone();
        single.ShouldBe(1);
    }

    [Test]
    public void SingleOrNone_Should_ReturnNone_WhenNoValues()
    {
        var single = _optionsWithoutValues.SingleOrNone();
        single.ShouldBeNone();
    }

    [Test]
    public void SingleOrNone_ShouldThrow_WhenMultipleValuesExist()
    {
        Should.Throw<InvalidOperationException>(() => _collectionWithValues.SingleOrNone());
    }

    [Test]
    public void SingleOrNone_Should_ReturnSingleValue_MatchingPredicate()
    {
        var single = _collectionWithValues.SingleOrNone(x => x == 2);
        single.ShouldBe(2);
    }

    [Test]
    public void SingleOrNone_Should_ReturnNone_WhenNoMatchingValues()
    {
        var single = _collectionWithValues.SingleOrNone(x => x < 0);
        single.ShouldBeNone();
    }

    [Test]
    public void SingleOrNone_ShouldThrow_WhenMultipleMatchingValuesExist()
    {
        Should.Throw<InvalidOperationException>(() => _collectionWithValues.SingleOrNone(x => x < 3));
    }

    [Test]
    public void SingleOrNone_Should_ReturnSingleValue_FromNullable_WhenExists()
    {
        IEnumerable<int?> collection = [null, 1, null];
        var single = collection.SingleOrNone();
        single.ShouldBe(1);
    }

    [Test]
    public void SingleOrNone_Should_ReturnNone_FromNullable_WhenNoValues()
    {
        var single = _nullableCollectionWithoutValues.SingleOrNone();
        single.ShouldBeNone();
    }

    [Test]
    public void SingleOrNone_ShouldThrow_FromNullable_WhenMultipleValuesExist()
    {
        Should.Throw<InvalidOperationException>(() => _nullableCollectionWithStructs.SingleOrNone());
    }

    [Test]
    public void SingleOrNone_Should_ReturnSingleValue_FromNullable_MatchingPredicate()
    {
        var single = _nullableCollectionWithStructs.SingleOrNone(x => x == 2);
        single.ShouldBe(2);
    }

    [Test]
    public void SingleOrNone_Should_ReturnNone_FromNullable_WhenNoMatchingValues()
    {
        var single = _nullableCollectionWithStructs.SingleOrNone(x => x < 0);
        single.ShouldBeNone();
    }

    [Test]
    public void SingleOrNone_ShouldThrow_FromNullable_WhenMultipleMatchingValuesExist()
    {
        Should.Throw<InvalidOperationException>(() => _nullableCollectionWithStructs.SingleOrNone(x => x < 4));
    }
}