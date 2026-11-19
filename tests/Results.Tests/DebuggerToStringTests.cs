using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results;

public class DebuggerToStringTests
{
    [Test]
    public void Failure_DebuggerToString_Should_ReturnMessage()
    {
        Failure failure = TestFailure.Instance;
        failure.DebuggerToString().ShouldBe("Failure: TestFailure");
    }

    [Test]
    public void Result_DebuggerToString_Should_ReturnSuccess_WhenSuccess()
    {
        var result = Result.Success();
        result.DebuggerToString().ShouldBe("Success");
    }

    [Test]
    public void Result_DebuggerToString_Should_ReturnFailureAndMessage_WhenFailure()
    {
        Result<Success> result = TestFailure.Instance;
        result.DebuggerToString().ShouldBe("Failure: TestFailure");
    }

    [Test]
    public void ResultT_DebuggerToString_Should_ReturnSuccessAndValue_WhenSuccess()
    {
        var result = Result.Success(1);
        result.DebuggerToString().ShouldBe("Success: 1");
    }

    [Test]
    public void ResultT_DebuggerToString_Should_ReturnFailureAndMessage_WhenFailure()
    {
        Result<int> result = TestFailure.Instance;
        result.DebuggerToString().ShouldBe("Failure: TestFailure");
    }
}
