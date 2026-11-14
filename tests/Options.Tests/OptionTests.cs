namespace Toarnbeike.Options;

public class OptionTests
{
    private const string _testValue = "test";

    [Test]
    public void Some_Should_CreateOptionWithValue()
    {
        var option = Option<string>.Some(_testValue);

        option.IsSome.ShouldBeTrue();
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(_testValue);
    }

    [Test]
    public void Some_Should_ThrowArgumentNullException_WhenValueIsNull()
    {
        Should.Throw<ArgumentNullException>(() => Option<string>.Some(null!));
    }

    [Test]
    public void None_Should_CreateOptionWithoutValue()
    {
        var option = Option<string>.None;

        option.IsSome.ShouldBeFalse();
        option.TryGetValue(out var _).ShouldBeFalse();
    }

    [Test]
    public void IsSome_ShouldBeTrue_WhenOptionHasValue()
    {
        var option = Option<string>.Some(_testValue);
        option.IsSome.ShouldBeTrue();
    }

    [Test]
    public void IsSome_ShouldBeFalse_WhenOptionHasNoValue()
    {
        var option = Option<string>.None;
        option.IsSome.ShouldBeFalse();
    }
    
    [Test]
    public void IsNone_ShouldBeTrue_WhenOptionHasNoValue()
    {
        var option = Option<string>.None;
        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void IsNone_ShouldBeFalse_WhenOptionHasValue()
    {
        var option = Option<string>.Some(_testValue);
        option.IsNone.ShouldBeFalse();
    }

    [Test]
    public void Value_Should_ImplicitlyConvertToOption()
    {
        Option<string> option = _testValue;

        option.IsSome.ShouldBeTrue();
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(_testValue);
    }

    [Test]
    public void TryGetValue_Should_ReturnTrue_WhenOptionHasValue()
    {
        var option = Option<string>.Some(_testValue);
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe(_testValue);
    }

    [Test]
    public void TryGetValue_Should_ReturnFalse_WhenOptionHasNoValue()
    {
        var option = Option<string>.None;
        option.TryGetValue(out var _).ShouldBeFalse();
    }

    [Test]
    public void OptionNone_Should_ReturnOptionWithNoContent()
    {
        Option<string> option = Option.None;

        option.IsNone.ShouldBeTrue();
        option.TryGetValue(out var _).ShouldBeFalse();
    }
}