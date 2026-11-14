namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Tests for the <see cref="AsNullableExtensions"/> class.
/// </summary>
/// <remarks>Uses explicit types rather then var in the act stap to verify return type to be nullable.</remarks>
public class AsNullableTests
{
    private readonly Option<string> _some = "abc";
    private readonly Option<string> _none = Option.None;
    private readonly Option<int> _someValue = 1;
    private readonly Option<int> _noneValue = Option.None;

    private readonly Task<Option<string>> _someAsync = Task.FromResult(Option.Some("abc"));
    private readonly Task<Option<string>> _noneAsync = Task.FromResult(Option<string>.None);
    private readonly Task<Option<int>> _someValueAsync = Task.FromResult(Option.Some(1));
    private readonly Task<Option<int>> _noneValueAsync = Task.FromResult(Option<int>.None);

    [Test]
    public void AsNullable_Should_ReturnNull_WhenOptionIsNone()
    {
        string? result = _none.AsNullable();
        result.ShouldBeNull();
    }

    [Test]
    public void AsNullable_Should_ReturnValue_WhenOptionIsSome()
    {
        string? result = _some.AsNullable();
        result.ShouldBe("abc");
    }

    [Test]
    public void AsNullableValue_Should_ReturnNull_WhenOptionIsNone()
    {
        int? result = _noneValue.AsNullableValue();
        result.ShouldBeNull();
    }

    [Test]
    public void AsNullableValue_Should_ReturnValue_WhenOptionIsSome()
    {
        int? result = _someValue.AsNullableValue();
        result.ShouldBe(1);
    }

    [Test]
    public async Task AsNullableAsync_Should_ReturnNull_WhenOptionIsNone()
    {
        string? result = await _noneAsync.AsNullable();
        result.ShouldBeNull();
    }

    [Test]
    public async Task AsNullableAsync_Should_ReturnValueAsync_WhenOptionIsSome()
    {
        string? result = await _someAsync.AsNullable();
        result.ShouldBe("abc");
    }

    [Test]
    public async Task AsNullableValueAsync_Should_ReturnNull_WhenOptionIsNone()
    {
        int? result = await _noneValueAsync.AsNullableValue();
        result.ShouldBeNull();
    }

    [Test]
    public async Task AsNullableValueAsync_Should_ReturnValueAsync_WhenOptionIsSome()
    {
        int? result = await _someValueAsync.AsNullableValue();
        result.ShouldBe(1);
    }
}
