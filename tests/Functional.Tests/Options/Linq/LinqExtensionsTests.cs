using Toarnbeike.Options.TestExtensions;

namespace Toarnbeike.Options.Linq;

public class LinqExtensionsTests
{
    private static Option<int> SomeInt(int value) => value;
    private static Option<int> NoneInt() => Option.None;

    [Fact]
    public void Select_OnSome_ProjectsValue()
    {
        var option =
            from x in SomeInt(10)
            select x * 2;

        option.ShouldBeSomeWithValue(20);
    }

    [Fact]
    public void Select_OnNone_YieldsNone()
    {
        var option =
            from x in NoneInt()
            select x * 2;

        option.ShouldBeNone();
    }

    [Fact]
    public void SelectMany_ChainsTwoSomes()
    {
        var option =
            from a in SomeInt(3)
            from b in SomeInt(4)
            select a + b;

        option.ShouldBeSomeWithValue(7);
    }

    [Fact]
    public void SelectMany_StopsOnFirstNone()
    {
        var option =
            from a in SomeInt(3)
            from b in NoneInt()
            select a + b;

        option.ShouldBeNone();
    }

    [Fact]
    public void SelectMany_CombinesProjectedTypes()
    {
        Option<string> GetName(int id) =>
            id == 1 ? "Alice" : Option.None;

        Option<int> GetAge(string name) =>
            name == "Alice" ? 30 : Option.None;

        var result =
            from id in SomeInt(1)
            from name in GetName(id)
            from age in GetAge(name)
            select $"{name} is {age}";

        result.ShouldBeSomeWithValue("Alice is 30");
    }
}