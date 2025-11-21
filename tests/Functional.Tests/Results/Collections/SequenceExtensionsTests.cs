using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Collections;

public class SequenceExtensionsTests : CollectionExtensionsTestBase
{
    [Fact]
    public void Sequence_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = AllSuccessCollection.Sequence();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Fact]
    public void Sequence_ShouldReturnFailure_WhenCollectionContainsFailures()
    {
        var result = MixedCollection.Sequence();
        result.ShouldBeFailureWithCode("first");
    }
    
    [Fact]
    public async Task SequenceAsync_ShouldReturnSuccess_WhenAllResultsAreSuccessful()
    {
        var result = await AllSuccessCollectionAsync.Sequence();
        result.ShouldBeSuccessWithValue([1, 2, 3]);
    }

    [Fact]
    public async Task SequenceAsync_ShouldReturnFailure_WhenCollectionContainsFailures()
    {
        var result = await MixedCollectionAsync.Sequence();
        result.ShouldBeFailureWithCode("first");
    }
    
}