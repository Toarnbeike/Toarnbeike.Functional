using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Options.Linq;

public class OptionLinqQuerySyntaxTests
{
    [Test]
    public void QuerySyntax_ReturnsSome_WhenAllStepsSucceed()
    {
        Option<int> result =
            from x in Option.Some(10)
            where x > 5
            from y in Option.Some(x * 2)
            select y + 1;

        result.ShouldBeSome().ShouldBe(21);
    }

    [Test]
    public void QuerySyntax_ReturnsNone_WhenWhereFiltersOut()
    {
        Option<int> result =
            from x in Option.Some(3)
            where x > 5
            select x * 2;

        result.ShouldBeNone();
    }

    [Test]
    public void QuerySyntax_ReturnsNone_WhenAnyBindIsNone()
    {
        Option<int> result =
            from x in Option.Some(10)
            from y in Option<int>.None
            select x + y;

        result.ShouldBeNone();
    }
}
