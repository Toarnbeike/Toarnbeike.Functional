using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Collections;

public abstract class CollectionResultExtensionTestBase
{
    protected readonly List<Result> AllSuccessCollection = [Result.Success(), Result.Success()];
    protected readonly List<Result> MixedCollection = [new TestFailure("code1"), Result.Success(), new TestFailure("code2")];
    protected readonly List<Result> EmptyCollection = [];

    protected readonly List<Task<Result>> AllSuccessTaskCollection = [Task.FromResult(Result.Success()), Task.FromResult(Result.Success())];
    protected readonly List<Task<Result>> MixedTaskCollection = [Task.FromResult(Result.Failure(new TestFailure("code1"))), Task.FromResult(Result.Success()), Task.FromResult(Result.Failure(new TestFailure("code2")))];
    protected readonly List<Task<Result>> EmptyTaskCollection = [];
}