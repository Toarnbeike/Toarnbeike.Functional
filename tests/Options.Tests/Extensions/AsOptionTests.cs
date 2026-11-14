namespace Toarnbeike.Options.Extensions;

public class AsOptionTests
{
    private readonly string? _some = "abc";
    private readonly string? _none = null;
    private readonly int? _someValue = 0;
    private readonly int? _noneValue = null;

    private readonly Task<string?> _someAsync = Task.FromResult<string?>("abc");
    private readonly Task<string?> _noneAsync = Task.FromResult<string?>(null);
    private readonly Task<int?> _someValueAsync = Task.FromResult<int?>(1);
    private readonly Task<int?> _noneValueAsync = Task.FromResult<int?>(null);

    [Test]
    public void AsOption_Should_ReturnSome_WhenStringIsNotNull()
    {
        var result = _some.AsOption();
        result.ShouldBeOfType<Option<string>>();
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe("abc");
    }

    [Test]
    public void AsOption_Should_ReturnNone_WhenStringIsNull()
    {
        var result = _none.AsOption();
        result.ShouldBeOfType<Option<string>>();
        result.ShouldBe(Option.None);
    }

    [Test]
    public async Task AsOptionAsync_Should_ReturnSome_WhenStringIsNotNull()
    {
        var result = await _someAsync.AsOption();
        result.ShouldBeOfType<Option<string>>();
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe("abc");
    }

    [Test]
    public async Task AsOptionAsync_Should_ReturnNone_WhenStringIsNull()
    {
        var result = await _noneAsync.AsOption();
        result.ShouldBeOfType<Option<string>>();
        result.ShouldBe(Option.None);
    }

    [Test]
    public void AsOption_Should_ReturnSome_WhenIntIsNotNull()
    {
        var result = _someValue.AsOption();
        result.ShouldBeOfType<Option<int>>();
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(0);
    }

    [Test]
    public void AsOption_Should_ReturnNone_WhenIntIsNull()
    {
        var result = _noneValue.AsOption();
        result.ShouldBeOfType<Option<int>>();
        result.ShouldBe(Option.None);
    }

    [Test]
    public async Task AsOptionAsync_Should_ReturnSome_WhenIntIsNotNull()
    {
        var result = await _someValueAsync.AsOption();
        result.ShouldBeOfType<Option<int>>();
        result.TryGetValue(out var value).ShouldBeTrue();
        value.ShouldBe(1);
    }

    [Test]
    public async Task AsOptionAsync_Should_ReturnNone_WhenIntIsNull()
    {
        var result = await _noneValueAsync.AsOption();
        result.ShouldBeOfType<Option<int>>();
        result.ShouldBe(Option.None);
    }
}