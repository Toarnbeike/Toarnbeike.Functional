using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Collections;

public class TraverseExtensionTests
{
    private readonly List<int> _successCollection = [1, 2, 3];
    private readonly List<int> _failureCollection = [-1, 2, 3];
    private readonly IEnumerable<Task<int>> _successAsyncCollection = [Task.FromResult(1), Task.FromResult(2), Task.FromResult(3)];
    private readonly IEnumerable<Task<int>> _failureAsyncCollection = [Task.FromResult(-1), Task.FromResult(2), Task.FromResult(3)];
    
    [Fact]
    public void Traverse_Should_Return_Success_WhenAllResultsAreSuccess()
    {
        var result = _successCollection.Traverse(Bind);
        var actual = result.ShouldBeSuccess();
        actual.ShouldBeEquivalentTo(_successCollection);
    }

    [Fact]
    public void Traverse_Should_Return_Failure_WhenAResultIsAFailure()
    {
        var result = _failureCollection.Traverse(Bind);
        result.ShouldBeFailureWithMessage("Negative value");
    }

    [Fact]
    public async Task TraverseAsync_Should_Return_Success_WhenAllResultsAreSuccess()
    {
        var result = await _successCollection.TraverseAsync(BindAsync);
        var actual = result.ShouldBeSuccess();
        actual.ShouldBeEquivalentTo(_successCollection);
    }

    [Fact]
    public async Task TraverseAsync_Should_Return_Failure_WhenAResultIsAFailure()
    {
        var result = await _failureCollection.TraverseAsync(BindAsync);
        result.ShouldBeFailureWithMessage("Negative value");
    }
    
    [Fact]
    public async Task Traverse_Should_Return_Success_WhenAllResultsAreSuccess_OnTask()
    {
        var result = await _successAsyncCollection.Traverse(Bind);
        var actual = result.ShouldBeSuccess();
        actual.ShouldBeEquivalentTo(_successCollection);
    }

    [Fact]
    public async Task Traverse_Should_Return_Failure_WhenAResultIsAFailure_OnTask()
    {
        var result = await _failureAsyncCollection.Traverse(Bind);
        result.ShouldBeFailureWithMessage("Negative value");
    }

    [Fact]
    public async Task TraverseAsync_Should_Return_Success_WhenAllResultsAreSuccess_OnTask()
    {
        var result = await _successAsyncCollection.TraverseAsync(BindAsync);
        var actual = result.ShouldBeSuccess();
        actual.ShouldBeEquivalentTo(_successCollection);
    }

    [Fact]
    public async Task TraverseAsync_Should_Return_Failure_WhenAResultIsAFailure_OnTask()
    {
        var result = await _failureAsyncCollection.TraverseAsync(BindAsync);
        result.ShouldBeFailureWithMessage("Negative value");
    }
    
    private static Result<int> Bind(int x) =>
        x > 0 ? Result<int>.Success(x) : new Failure("Neg", "Negative value");
    
    private static async Task<Result<int>> BindAsync(int x) =>
        await Task.FromResult(Bind(x));
}