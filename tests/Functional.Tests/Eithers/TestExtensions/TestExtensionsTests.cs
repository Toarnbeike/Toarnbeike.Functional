namespace Toarnbeike.Eithers.TestExtensions;

public class TestExtensionsTests
{
    [Fact]
    public void ShouldBeLeft_Should_ReturnLeft_WhenLeft()
    {
        var either = Either<string, int>.Left("message");
        var actual = either.ShouldBeLeft();
        actual.ShouldBe("message");
    }

    [Fact]
    public void ShouldBeLeft_Should_ThrowFunctionalAssertException_WhenRight()
    {
        var either = Either<string, int>.Right(1);
        var exception = Should.Throw<FunctionalAssertException>(() => either.ShouldBeLeft());
        exception.Message.ShouldBe("Expected left (String) value, but got right (Int32) value.");
    }

    [Fact]
    public void ShouldBeLeftWithValue_Should_Succeed_WhenCorrectLeftValue()
    {
        var either = Either<string, int>.Left("message");
        either.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public void ShouldBeLeftWithValue_Should_ThrowFunctionalAssertException_WhenRight()
    {
        var either = Either<string, int>.Right(1);
        var exception = Should.Throw<FunctionalAssertException>(() => either.ShouldBeLeftWithValue("any"));
        exception.Message.ShouldBe("Expected left (String) value, but got right (Int32) value.");
    }

    [Fact]
    public void ShouldBeLeftWithValue_Should_ThrowFunctionalAssertException_WhenIncorrectValue()
    {
        var either = Either<string, int>.Left("message");
        var exception = Should.Throw<FunctionalAssertException>(() => either.ShouldBeLeftWithValue("wrong"));
        exception.Message.ShouldBe("Expected 'wrong', but got 'message'.");
    }
    
    [Fact]
    public void ShouldBeRight_Should_ReturnRight_WhenRight()
    {
        var either = Either<string, int>.Right(1);
        var actual = either.ShouldBeRight();
        actual.ShouldBe(1);
    }

    [Fact]
    public void ShouldBeRight_Should_ThrowFunctionalAssertException_WhenLeft()
    {
        var either = Either<string, int>.Left("message");
        var exception = Should.Throw<FunctionalAssertException>(() => either.ShouldBeRight());
        exception.Message.ShouldBe("Expected right (Int32) value, but got left (String) value.");
    }

    [Fact]
    public void ShouldBeRightWithValue_Should_Succeed_WhenCorrectRightValue()
    {
        var either = Either<string, int>.Right(1);
        either.ShouldBeRightWithValue(1);
    }

    [Fact]
    public void ShouldBeRightWithValue_Should_ThrowFunctionalAssertException_WhenLeft()
    {
        var either = Either<string, int>.Left("message");
        var exception = Should.Throw<FunctionalAssertException>(() => either.ShouldBeRightWithValue(0));
        exception.Message.ShouldBe("Expected right (Int32) value, but got left (String) value.");
    }

    [Fact]
    public void ShouldBeRigthWithValue_Should_ThrowFunctionalAssertException_WhenIncorrectValue()
    {
        var either = Either<string, int>.Right(1);
        var exception = Should.Throw<FunctionalAssertException>(() => either.ShouldBeRightWithValue(-1));
        exception.Message.ShouldBe("Expected '-1', but got '1'.");
    }
}