namespace Toarnbeike.Results.Collections;

public class PartitionExtensionsTests : CollectionExtensionsTestBase
{
    [Fact]
    public void Failures_Should_ReturnEmpty_WhenCollectionIsAllSuccess()
    {
        AllSuccessResults.Failures().ShouldBeEmpty();
    }

    [Fact]
    public void Failures_Should_ReturnFailures_WhenCollectionHasFailures()
    {
        FailingResults.Failures().Count().ShouldBe(2);
    }

    [Fact]
    public void Failures_Should_ReturnEmpty_WhenCollectionOfTIsAllSuccess()
    {
        AllSuccessCollection.Failures().ShouldBeEmpty();
    }

    [Fact]
    public void Failures_Should_ReturnFailures_WhenCollectionOfTHasFailures()
    {
        MixedCollection.Failures().Count().ShouldBe(2);
    }
    
    [Fact]
    public async Task Failures_Should_ReturnEmpty_WhenAsyncCollectionIsAllSuccess()
    {
        (await AllSuccessResultsAsync.Failures()).ShouldBeEmpty();
    }

    [Fact]
    public async Task Failures_Should_ReturnFailures_WhenAsyncCollectionHasFailures()
    {
        (await FailingResultsAsync.Failures()).Count().ShouldBe(2);
    }

    [Fact]
    public async Task Failures_Should_ReturnEmpty_WhenAsyncCollectionOfTIsAllSuccess()
    {
        (await AllSuccessCollectionAsync.Failures()).ShouldBeEmpty();
    }

    [Fact]
    public async Task Failures_Should_ReturnFailures_WhenAsyncCollectionOfTHasFailures()
    {
        (await MixedCollectionAsync.Failures()).Count().ShouldBe(2);
    }
    
    [Fact]
    public void SuccessValues_Should_ReturnEmpty_WhenCollectionOfTIsAllSuccess()
    {
        AllSuccessCollection.SuccessValues().Count().ShouldBe(3);
    }

    [Fact]
    public void SuccessValues_Should_ReturnSuccessValues_WhenCollectionOfTHasSuccessValues()
    {
        MixedCollection.SuccessValues().Count().ShouldBe(2);
    }

    [Fact]
    public async Task SuccessValues_Should_ReturnEmpty_WhenAsyncCollectionOfTIsAllSuccess()
    {
        (await AllSuccessCollectionAsync.SuccessValues()).Count().ShouldBe(3);
    }

    [Fact]
    public async Task SuccessValues_Should_ReturnSuccessValues_WhenAsyncCollectionOfTHasSuccessValues()
    {
        (await MixedCollectionAsync.SuccessValues()).Count().ShouldBe(2);
    }
    
    [Fact]
    public void Partition_Should_ReturnEmpty_WhenCollectionOfTIsAllSuccess()
    {
        var tuple = AllSuccessCollection.Partition();
        tuple.Failures.Count().ShouldBe(0);
        tuple.Successes.Count().ShouldBe(3);
    }

    [Fact]
    public void Partition_Should_ReturnPartition_WhenCollectionOfTHasPartition()
    {
        var tuple = MixedCollection.Partition();
        tuple.Failures.Count().ShouldBe(2);
        tuple.Successes.Count().ShouldBe(2);
    }

    [Fact]
    public async Task Partition_Should_ReturnEmpty_WhenAsyncCollectionOfTIsAllSuccess()
    {
        var tuple = await AllSuccessCollectionAsync.Partition();
        tuple.Failures.Count().ShouldBe(0);
        tuple.Successes.Count().ShouldBe(3);
    }

    [Fact]
    public async Task Partition_Should_ReturnPartition_WhenAsyncCollectionOfTHasPartition()
    {
        var tuple = await MixedCollectionAsync.Partition();
        tuple.Failures.Count().ShouldBe(2);
        tuple.Successes.Count().ShouldBe(2);
    }
}