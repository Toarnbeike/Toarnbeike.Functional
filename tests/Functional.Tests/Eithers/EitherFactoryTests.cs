using Toarnbeike.Eithers.TestExtensions;

namespace Toarnbeike.Eithers;

public class EitherFactoryTests
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
}