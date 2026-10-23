using Shouldly;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Functional.Optional.Extensions;

public class AsResultExtensionTests
{
    [Test]
    public void AsResult_WithValue_ReturnsSuccess()
    {
        var option = Option.Some(42);
        var failureFactory = new Func<Failure>(() => new TestFailure("No value"));

        var result = option.AsResult(failureFactory);

        result.ShouldBeSuccess().ShouldBe(42);
    }

    [Test]
    public void AsResult_WithoutValue_ReturnsFailure()
    {
        var option = Option<int>.None();
        var failureFactory = new Func<Failure>(() => new TestFailure("No value"));
        var result = option.AsResult(failureFactory);
        result.ShouldBeFailureOfType<TestFailure>().Identifier.ShouldBe("No value");
    }

    [Test]
    public async Task AsResultAsync_WithValue_ReturnsSuccess()
    {
        var optionTask = Task.FromResult(Option.Some(42));
        var failureFactory = new Func<Failure>(() => new TestFailure("No value"));
        var resultTask = optionTask.AsResultAsync(failureFactory);
        var result = await resultTask;
        result.ShouldBeSuccess().ShouldBe(42);
    }

    [Test]
    public async Task AsResultAsync_WithoutValue_ReturnsFailure()
    {
        var optionTask = Task.FromResult(Option<int>.None());
        var failureFactory = new Func<Failure>(() => new TestFailure("No value"));
        var resultTask = optionTask.AsResultAsync(failureFactory);
        var result = await resultTask;
        result.ShouldBeFailureOfType<TestFailure>().Identifier.ShouldBe("No value");
    }
}
