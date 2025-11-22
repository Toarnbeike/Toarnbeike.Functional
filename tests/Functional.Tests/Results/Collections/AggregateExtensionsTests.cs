using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Collections;

public class AggregateExtensionsTests : CollectionExtensionsTestBase
{
    [Fact]
    public void Aggregate_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = AllSuccessCollection.Aggregate();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Fact]
    public void Aggregate_ShouldReturnAggregateFailure_WhenCollectionContainsFailures()
    {
        var result = MixedCollection.Aggregate();
        var aggregateFailure = result.ShouldBeFailureOfType<IEnumerable<int>, AggregateFailure>();
        aggregateFailure.Failures.Count.ShouldBe(2);
    }

    [Fact]
    public void Aggregate_ShouldReturnSuccess_WhenAllNonGenericResultsAreSuccessful()
    {
        var result = AllSuccessResults.Aggregate();
        result.ShouldBeSuccess();
    }

    [Fact]
    public void Aggregate_ShouldReturnAggregateFailure_WhenNonGenericResultCollectionContainsFailures()
    {
        var result = FailingResults.Aggregate();
        var aggregateFailure = result.ShouldBeFailureOfType<AggregateFailure>();
        aggregateFailure.Failures.Count.ShouldBe(2);
    }
    
    [Fact]
    public async Task AggregateAsync_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = await AllSuccessCollectionAsync.Aggregate();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Fact]
    public async Task AggregateAsync_ShouldReturnAggregateFailure_WhenCollectionContainsFailures()
    {
        var result = await MixedCollectionAsync.Aggregate();
        var aggregateFailure = result.ShouldBeFailureOfType<IEnumerable<int>, AggregateFailure>();
        aggregateFailure.Failures.Count().ShouldBe(2);
    }

    [Fact]
    public async Task AggregateAsync_ShouldReturnSuccess_WhenAllNonGenericResultsAreSuccessful()
    {
        var result = await AllSuccessResultsAsync.Aggregate();
        result.ShouldBeSuccess();
    }

    [Fact]
    public async Task AggregateAsync_ShouldReturnAggregateFailure_WhenNonGenericResultCollectionContainsFailures()
    {
        var result = await FailingResultsAsync.Aggregate();
        var aggregateFailure = result.ShouldBeFailureOfType<AggregateFailure>();
        aggregateFailure.Failures.Count().ShouldBe(2);
    }
}