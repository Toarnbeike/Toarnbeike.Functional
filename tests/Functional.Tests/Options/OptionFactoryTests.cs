namespace Toarnbeike.Options;

public class OptionFactoryTests
{
    [Fact]
    public void None_Should_ReturnOptionWithNoContent()
    {
        var option = Option.None;

        option.ShouldBeOfType<Option<NoContent>>();
        option.HasValue.ShouldBeFalse();
        option.TryGetValue(out var _).ShouldBeFalse();
    }

    [Fact]
    public void None_Should_ImplicitlyConvertToOptionOfTValue()
    {
        Option<string> option = Option.None;

        option.HasValue.ShouldBeFalse();
        option.TryGetValue(out var _).ShouldBeFalse();
    }

    [Fact]
    public void Some_Should_CreateOptionWithValue()
    {
        var option = Option.Some("test");

        option.HasValue.ShouldBeTrue();
        option.TryGetValue(out var actual).ShouldBeTrue();
        actual.ShouldBe("test");
    }
}