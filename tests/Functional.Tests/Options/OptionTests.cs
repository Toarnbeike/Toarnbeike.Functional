namespace Toarnbeike.Options;

public class OptionTests
{
    private const string TestValue = "test";

    [Fact]
    public void Some_Should_CreateOptionWithValue()
    {
        var option = Option<string>.Some(TestValue);

        option.HasValue.ShouldBeTrue();
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(TestValue);
    }

    [Fact]
    public void Some_Should_ThrowArgumentNullException_WhenValueIsNull()
    {
        Should.Throw<ArgumentNullException>(() => Option<string>.Some(null!));
    }

    [Fact]
    public void None_Should_CreateOptionWithoutValue()
    {
        var option = Option<string>.None();

        option.HasValue.ShouldBeFalse();
        option.TryGetValue(out _).ShouldBeFalse();
    }

    [Fact]
    public void Value_Should_ImplicitlyConvertToOption()
    {
        Option<string> option = TestValue;

        option.HasValue.ShouldBeTrue();
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(TestValue);
    }

    [Fact]
    public void TryGetValue_Should_ReturnTrue_WhenOptionHasValue()
    {
        var option = Option<string>.Some(TestValue);
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(TestValue);
    }

    [Fact]
    public void TryGetValue_Should_ReturnFalse_WhenOptionHasNoValue()
    {
        var option = Option<string>.None();
        option.TryGetValue(out _).ShouldBeFalse();
    }

    [Fact]
    public void EqualValue_Should_ReturnTrue_WhenOptionHasEqualValue()
    {
        var option = Option<string>.Some(TestValue);
        option.EqualValue(TestValue).ShouldBeTrue();
    }

    [Fact]
    public void EqualValue_Should_ReturnFalse_WhenValuesDoNotMatch()
    {
        var option = Option<string>.Some(TestValue);
        option.EqualValue("something else").ShouldBeFalse();
    }

    [Fact]
    public void EqualValue_Should_ReturnFalse_WhenOptionHasNoValue()
    {
        var option = Option<string>.None();
        option.EqualValue(TestValue).ShouldBeFalse();
    }

    [Fact]
    public void EqualValue_Should_ReturnFalse_WhenComparedWithNull()
    {
        var option = Option<string>.Some("value");
        option.EqualValue(null!).ShouldBeFalse();
    }

    [Fact]
    public void ToString_Should_ReturnValue_WhenOptionHasValue()
    {
        var option = Option<string>.Some(TestValue);
        option.ToString().ShouldBe(TestValue);
    }

    [Fact]
    public void ToString_Should_ReturnEmptyString_WhenOptionHasNoValue()
    {
        var option = Option<string>.None();
        option.ToString().ShouldBe(string.Empty);
    }

    [Fact]
    public void ToString_Should_ReturnTypeName_WhenValueToStringIsNull()
    {
        var option = Option.Some(new NullToStringObject());
        option.ToString().ShouldBe(nameof(NullToStringObject));
    }

    /// <summary>
    /// Test class to verify behavior of <see cref="Option{TValue}.ToString()"/> when <c>TValue.ToString()</c> returns <see langword="null"/>.
    /// </summary>
    private class NullToStringObject
    {
        public override string? ToString() => null;
    }
}