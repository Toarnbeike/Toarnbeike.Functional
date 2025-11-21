namespace Toarnbeike.Results.Collections;

public abstract class CollectionExtensionsTestBase
{
    protected readonly List<Result> AllSuccessResults = [Result.Success(), Result.Success()];
    protected readonly List<Result> FailingResults = [
        Result.Failure(new Failure("code1", "message1")),
        Result.Success(), 
        Result.Failure(new Failure("code2", "message2"))];

    protected readonly List<Result<int>> AllSuccessCollection = [1, 2, 3];
    protected readonly List<Result<int>> MixedCollection = [
        1, 
        new Failure("first", "value is missing"),
        3,
        new Failure("second", "The second failure")];

    protected readonly List<Task<Result>> AllSuccessResultsAsync = [
        Task.FromResult(Result.Success()),
        Task.FromResult(Result.Success())];
    protected readonly List<Task<Result>> FailingResultsAsync = [
        Task.FromResult(Result.Failure(new Failure("code1", "message1"))),
        Task.FromResult(Result.Success()),
        Task.FromResult(Result.Failure(new Failure("code2", "message2")))];

    protected readonly List<Task<Result<int>>> AllSuccessCollectionAsync = [
        Task.FromResult(Result.Success(1)),
        Task.FromResult(Result.Success(2)),
        Task.FromResult(Result.Success(3))];
    protected readonly List<Task<Result<int>>> MixedCollectionAsync = [
        Task.FromResult(Result.Success(1)),
        Task.FromResult(Result<int>.Failure(new Failure("first", "value is missing"))), 
        Task.FromResult(Result.Success(3)),
        Task.FromResult(Result<int>.Failure(new Failure("second", "The second failure")))];
}