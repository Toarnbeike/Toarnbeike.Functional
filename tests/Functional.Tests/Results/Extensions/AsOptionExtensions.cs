using Shouldly;
using Toarnbeike.Functional.Results.Extensions;
using Toarnbeike.Optional.TestExtensions;

namespace Toarnbeike.Functional.Tests.Results.Extensions;

public class AsOptionExtensions
{
    [Test]
    public void AsOption_WithValue_ReturnsSome()
    {
        var result = Result.Success(42);
        var option = result.AsOption();
        option.ShouldBeSome().ShouldBe(42);
    }

    [Test]
    public void AsOption_WithFailure_ReturnsNone()
    {
        Result<int> result = new TestFailure("Original error");
        var option = result.AsOption();
        option.ShouldBeNone();
    }

    [Test]
    public async Task AsOptionAsync_WithValue_ReturnsSome()
    {
        var resultTask = Task.FromResult(Result.Success(42));
        var optionTask = resultTask.AsOptionAsync();
        var option = await optionTask;
        option.ShouldBeSome().ShouldBe(42);
    }

    [Test]
    public async Task AsOptionAsync_WithFailure_ReturnsNone()
    {
        Task<Result<int>> resultTask = Task.FromResult<Result<int>>(new TestFailure("Original error"));
        var optionTask = resultTask.AsOptionAsync();
        var option = await optionTask;
        option.ShouldBeNone();
    }
}
