using Toarnbeike.Options;
using Shouldly;

namespace Toarnbeike.Functional.TestExtensions;

/// <summary>
/// Tests for the <see cref="TestExtensions"/> class.
/// </summary>
public class OptionTestExtensionsTests
{
    private readonly Option<int> _some = 1;
    private readonly Option<int> _none = Option.None;

    [Test]
    public void ShouldBeSome_Should_ReturnTValue_WhenOptionIsSome()
    {
        var result = _some.ShouldBeSome();
        result.ShouldBe(1);
    }

    [Test]
    public void ShouldBeSome_Should_ThrowException_WhenOptionIsNone()
    {
        var exception = Should.Throw<AssertionFailedException>(() => _none.ShouldBeSome());
        exception.Message.ShouldContain($"Option<{nameof(Int32)}>");
        exception.Message.ShouldContain("but it was None.");
    }

    [Test]
    public void ShouldBeSome_Should_ReturnCustomMessage_WhenProvided()
    {
        var message = "Custom failure message.";
        var exception = Should.Throw<AssertionFailedException>(() => _none.ShouldBeSome(message));
        exception.Message.ShouldBe(message);
    }

    [Test]
    public void ShouldBeNone_Should_NotThrow_WhenOptionIsNone()
    {
        Should.NotThrow(() => _none.ShouldBeNone());
    }

    [Test]
    public void ShouldBeNone_Should_ThrowException_WhenOptionIsSome()
    {
        var exception = Should.Throw<AssertionFailedException>(() => _some.ShouldBeNone());
        exception.Message.ShouldContain($"Option<{nameof(Int32)}>");
        exception.Message.ShouldContain("but it was Some with value 1.");
    }

    [Test]
    public void ShouldBeNone_Should_ReturnCustomMessage_WhenProvided()
    {
        var message = "Custom failure message.";
        var exception = Should.Throw<AssertionFailedException>(() => _some.ShouldBeNone(message));
        exception.Message.ShouldBe(message);
    }
}