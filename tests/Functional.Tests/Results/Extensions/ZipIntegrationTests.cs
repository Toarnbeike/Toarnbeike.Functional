using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Extensions;

public class ZipIntegrationTests
{
    [Fact]
    public void TupleResult_Should_HandleOtherExtensionMethod_Sync()
    {

        var result = Result.Success(20)
            .Zip(value => value > 10 ? Result<decimal>.Success(5.3m) : new Failure("out of bound", "result should exceed 10"));

        result.ShouldBeSuccessWithValue((20, 5.3m));

        decimal value = 0;
        result.Tap(((int @base, decimal multiplier) tuple) => value = tuple.@base * tuple.multiplier);
        value.ShouldBe(106m);

        result.Check(((int @base, decimal multiplier) tuple) => tuple.multiplier > 5, () => new Failure("out of bound", "multiplier should exceed 5"));

        var actual = result
            .Map(((int @base, decimal multiplier) tuple) => tuple.@base * tuple.multiplier)
            .Match(success => success, _ => 0m);

        actual.ShouldBe(106m);
    }
}
