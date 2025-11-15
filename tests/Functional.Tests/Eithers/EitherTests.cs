using Toarnbeike.Eithers.TestExtensions;

namespace Toarnbeike.Eithers;

public class EitherTests
{
    [Fact]
    public void Left_Should_InstantiateAsLeft()
    {
        Either<string, int>.Left("message").ShouldBeLeftWithValue("message");
    }

    [Fact]
    public void Left_Should_Throw_WhenValueIsNull()
    {
        Should.Throw<ArgumentNullException>(() => Either<string?, int>.Left(null));
    }
    
    [Fact]
    public void Right_Should_InstantiateAsRight()
    {
        Either<string, int>.Right(1).ShouldBeRightWithValue(1);
    }

    [Fact]
    public void Right_Should_Throw_WhenValueIsNull()
    {
        Should.Throw<ArgumentNullException>(() => Either<string, int?>.Right(null));
    }

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
}