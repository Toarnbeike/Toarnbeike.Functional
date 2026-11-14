namespace Toarnbeike.Options;

[Obsolete("Test the obsolete HasValue property")]
public class OptionObsoleteTests
{
    [Test]
    public void HasValue_ShouldBeTrue_WhenOptionHasValue()
    {
        var option = Option<string>.Some("test");
        option.HasValue.ShouldBeTrue();
    }

    [Test]
    public void HasValue_ShouldBeFalse_WhenOptionHasNoValue()
    {
        var option = Option<string>.None;
        option.HasValue.ShouldBeFalse();
    }
}