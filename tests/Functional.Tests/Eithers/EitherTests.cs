using Toarnbeike.Eithers.TestExtensions;

namespace Toarnbeike.Eithers;

public class EitherTests
{
    [Fact]
    public void IsLeft_Should_ReturnTrue_WhenLeft()
    {
        Either<string, int>.Left("message").IsLeft(out var left).ShouldBeTrue();
        left.ShouldBe("message");
    }
    
    [Fact]
    public void IsLeft_ShouldReturnFalse_WhenRight()
    {
        Either<string, int>.Right(1).IsLeft(out _).ShouldBeFalse();
    }

    [Fact]
    public void IsRight_Should_ReturnTrue_WhenRight()
    {
        Either<string, int>.Right(1).IsRight(out var right).ShouldBeTrue();
        right.ShouldBe(1);
    }

    [Fact]
    public void IsRight_Should_ReturnFalse_WhenLeft()
    {
        Either<string, int>.Left("message").IsRight(out _).ShouldBeFalse();
    }
    
    [Fact]
    public void IsLeftInternal_Should_ReturnTrue_WhenLeft()
    {
        Either<string, int>.Left("message").IsLeft(out var left, out _).ShouldBeTrue();
        left.ShouldBe("message");
    }
    
    [Fact]
    public void IsLeftInternal_Should_ReturnFalse_WhenRight()
    {
        Either<string, int>.Right(1).IsLeft(out _, out var right).ShouldBeFalse();
        right.ShouldBe(1);
    }

    [Fact]
    public void Either_WithNullLeft_ShouldThrow_WhenNotIsRight()
    {
        Should.Throw<ArgumentNullException>(() => new Either<string?, int>(null, 0, false));
    }

    [Fact]
    public void Either_WithNullRight_ShouldThrow_WhenIsRight()
    {
        Should.Throw<ArgumentNullException>(() => new Either<string, int?>(null, null, true));
    }

    [Fact]
    public void Either_ShouldBe_ImplicitlyCreated_FromRight()
    {
        Either<string, int> either = 1;
        either.ShouldBeRightWithValue(1);
    }

    [Fact] public void Either_ShouldBe_ExplicitlyCreated_FromLeft()
    {
        var either = (Either<string, int>) "message";
        either.ShouldBeLeftWithValue("message");
    }
}