using Toarnbeike.Eithers.TestExtensions;

namespace Toarnbeike.Eithers.Linq;

public class EitherLinqTests
{
    private readonly Either<string, int> _left  = Either<string, int>.Left("message");
    private readonly Either<string, int> _right = Either<string, int>.Right(3);

    [Fact]
    public void Linq_Select_Should_Map_Right()
    {
        var result =
            from x in _right
            select x * 2;

        result.ShouldBeRightWithValue(6);
    }

    [Fact]
    public void Linq_Select_Should_Preserve_Left()
    {
        var result =
            from x in _left
            select x * 2;

        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public void Linq_SelectMany_Should_Compose_Two_Rights()
    {
        var other = Either<string, int>.Right(5);

        var result =
            from x in _right
            from y in other
            select x + y;

        result.ShouldBeRightWithValue(8);
    }

    [Fact]
    public void Linq_SelectMany_Should_Stop_On_First_Left()
    {
        var other = Either<string, int>.Right(5);

        var result =
            from x in _left
            from y in other
            select x + y;

        result.ShouldBeLeftWithValue("message");
    }

    [Fact]
    public void Linq_SelectMany_Should_Propagate_Second_Left()
    {
        var other = Either<string, int>.Left("second");

        var result =
            from x in _right
            from y in other
            select x + y;

        result.ShouldBeLeftWithValue("second");
    }

    [Fact]
    public void Linq_Nested_Computation_With_Transformations()
    {
        var a = Either<string, int>.Right(2);
        var b = Either<string, int>.Right(3);
        var c = Either<string, int>.Right(4);

        var result =
            from x in a
            from y in b
            from z in c
            select x * y * z;

        result.ShouldBeRightWithValue(24);
    }

    [Fact]
    public void Linq_Nested_Computation_Stops_On_First_Left()
    {
        var a = Either<string, int>.Right(2);
        var b = Either<string, int>.Left("b failed");
        var c = Either<string, int>.Right(4);

        var result =
            from x in a
            from y in b
            from z in c
            select x * y * z;

        result.ShouldBeLeftWithValue("b failed");
    }

    [Fact]
    public void Linq_SelectMany_Should_Use_Map_Like_Projection()
    {
        var result =
            from x in _right
            from y in Either<string, int>.Right(x * 10)
            select y + 1;

        result.ShouldBeRightWithValue(31);
    }
}
