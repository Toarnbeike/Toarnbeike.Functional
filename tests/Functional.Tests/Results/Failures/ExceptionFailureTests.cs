namespace Toarnbeike.Results.Failures;

/// <summary>
/// Tests for the <see cref="ExceptionFailure"/> record.
/// </summary>
public class ExceptionFailureTests
{
    [Fact]
    public void ExceptionFailure_Should_BeCreatedFromAnException()
    {
        var exception = new InvalidOperationException("Invalid");
        var failure = new ExceptionFailure(exception);

        failure.Exception.ShouldBeOfType<InvalidOperationException>();
        failure.Code.ShouldBe("exception:InvalidOperationException");
        failure.Message.ShouldBe("Invalid");
        failure.ExceptionType.ShouldBe("InvalidOperationException");
    }

    [Fact]
    public void ExceptionFailure_Should_BeAbleToChangeBaseProperties_UsingWithSyntax()
    {
        var exception = new InvalidOperationException("Invalid");
        var firstFailure = new ExceptionFailure(exception);

        var newFailure = firstFailure with { Code = "Something else" };
        newFailure.Code.ShouldBe("Something else");
    }
}
