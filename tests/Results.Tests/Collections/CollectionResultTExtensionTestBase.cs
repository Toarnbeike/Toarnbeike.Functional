using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results.Collections;

public abstract class CollectionResultTExtensionTestBase
{
    protected readonly List<Result<int>> AllSuccessCollection = [1, 2, 3];
    protected readonly List<Result<int>> MixedCollection = [1, new TestFailure("code1"), 3, new TestFailure("code2")];
    protected readonly List<Result<int>> EmptyCollection = [];

    protected readonly List<Task<Result<int>>> AllSuccessTaskCollection = [Task.FromResult(Result.Success(1)), Task.FromResult(Result.Success(2)), Task.FromResult(Result.Success(3))];
    protected readonly List<Task<Result<int>>> MixedTaskCollection = [Task.FromResult(Result.Success(1)), Task.FromResult(Result.Failure<int>(new TestFailure("code1"))),
        Task.FromResult(Result.Success(3)), Task.FromResult(Result.Failure<int>(new TestFailure("code2")))];
    protected readonly List<Task<Result<int>>> EmptyTaskCollection = [];
}