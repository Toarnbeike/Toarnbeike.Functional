namespace Toarnbeike.Options;

public class OptionTryTests
{
    [Fact]
    public void Try_Should_ReturnSome_WhenFunctionSucceeds()
    {
        var result = Option.Try(() => "success");
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe("success");
    }

    [Fact]
    public void Try_Should_ReturnNone_WhenFunctionReturnsNull()
    {
        var result = Option.Try(() => (string?)null);
        result.TryGetValue(out var _).ShouldBeFalse();
    }

    [Fact]
    public void Try_Should_ReturnNone_WhenFunctionThrows()
    {
        var result = Option.Try<string>(() => throw new InvalidOperationException("Test exception"));
        result.TryGetValue(out var _).ShouldBeFalse();
    }

    [Fact]
    public void Try_Should_LogException_WhenProvided()
    {
        string? loggedException = null;
        var result = Option.Try<string>(() => throw new InvalidOperationException("Test exception"), ex => loggedException = ex.Message);
        result.HasValue.ShouldBeFalse();
        loggedException.ShouldNotBeNull();
        loggedException.ShouldBe("Test exception");
    }

    [Fact]
    public void Try_Should_DoNothingWithLogException_WhenNoExceptionOccurs()
    {
        string? loggedException = null;
        var result = Option.Try(() => "success", _ => throw new ShouldAssertException("Should not be invoked."));
        result.TryGetValue(out _).ShouldBeTrue();
        loggedException.ShouldBeNull();
    }

    [Fact]
    public void Try_Should_FilterExceptions_WhenExceptionFilterIsProvided()
    {
        var result = Option.Try<string>(() => throw new InvalidOperationException("Test exception"), ex => ex is InvalidOperationException);
        result.TryGetValue(out var _).ShouldBeFalse();
    }

    [Fact]
    public void Try_Should_RethrowExceptions_WhenExceptionIsNotFiltered()
    {
        Should.Throw<InvalidOperationException>(() =>
        {
            Option.Try<string>(() => throw new InvalidOperationException("Test exception"), ex => ex is ArgumentNullException);
        });
    }

    [Fact]
    public async Task TryAsync_Should_ReturnSome_WhenFunctionSucceeds()
    {
        var result = await Option.TryAsync(async () => await Task.FromResult("success"));
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe("success");
    }

    [Fact]
    public async Task TryAsync_Should_ReturnNone_WhenFunctionReturnsNull()
    {
        var result = await Option.TryAsync(async () => await Task.FromResult((string?)null));
        result.TryGetValue(out var _).ShouldBeFalse();
    }

    [Fact]
    public async Task TryAsync_Should_ReturnNone_WhenFunctionThrows()
    {
        var result = await Option.TryAsync(ThrowAsyncTask);
        result.TryGetValue(out var _).ShouldBeFalse();
    }

    [Fact]
    public async Task TryAsync_Should_LogException_WhenProvided()
    {
        string? loggedException = null;
        var result = await Option.TryAsync(ThrowAsyncTask, ex => loggedException = ex.Message);
        result.HasValue.ShouldBeFalse();
        loggedException.ShouldNotBeNull();
        loggedException.ShouldBe("Test exception");
    }

    [Fact]
    public async Task TryAsync_Should_DoNothingWithLogException_WhenNoExceptionOccurs()
    {
        string? loggedException = null;
        var result = await Option.TryAsync(() => Task.FromResult("success"), _ => throw new ShouldAssertException("Should not be invoked."));
        result.TryGetValue(out _).ShouldBeTrue();
        loggedException.ShouldBeNull();
    }

    [Fact]
    public async Task TryAsync_Should_FilterExceptions_WhenExceptionFilterIsProvided()
    {
        var result = await Option.TryAsync(ThrowAsyncTask, ex => ex is InvalidOperationException);
        result.TryGetValue(out var _).ShouldBeFalse();
    }

    [Fact]
    public async Task TryAsync_Should_RethrowExceptions_WhenExceptionIsNotFiltered()
    {
        await Should.ThrowAsync<InvalidOperationException>(async () => await Option.TryAsync(ThrowAsyncTask, ex => ex is ArgumentException));
    }

    private static async Task<string> ThrowAsyncTask()
    {
        await Task.Yield();
        throw new InvalidOperationException("Test exception");
    }
}