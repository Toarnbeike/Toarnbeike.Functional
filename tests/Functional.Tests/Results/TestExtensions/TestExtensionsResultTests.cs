namespace Toarnbeike.Results.TestExtensions;

public class TestExtensionsResultTests
{
    private readonly TestFailure _testFailure = new("test", "Test failure");
    
    [Fact]
    public void ShouldBeSuccess_Should_DoNothing_WhenSuccess()
    {
        var result = Result.Success();
        Should.NotThrow(() => result.ShouldBeSuccess());
    }

    [Fact]
    public void ShouldBeSuccess_Should_ThrowFunctionalAssertException_WhenFailure()
    {
        var result = Result.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeSuccess());
    }

    [Fact]
    public void ShouldBeFailure_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result.Success();
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailure());
    }

    [Fact]
    public void ShouldBeFailure_Should_ReturnFailure_WhenFailure()
    {
        var result = Result.Failure(_testFailure);
        result.ShouldBeFailure().ShouldBe(_testFailure);
    }
    
    [Fact]
    public void ShouldBeFailureWithCode_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result.Success();
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureWithCode("test"));
    }
    
    [Fact]
    public void ShouldBeFailureWithCode_Should_ReturnFailure_WhenFailureWithWrongCode()
    {
        var result = Result.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureWithCode("abc"));
    }
    
    [Fact]
    public void ShouldBeFailureWithCode_Should_ReturnFailure_WhenFailureWithRightCode()
    {
        var result = Result.Failure(_testFailure);
        result.ShouldBeFailureWithCode(_testFailure.Code).ShouldBe(_testFailure);
    }
    
    [Fact]
    public void ShouldBeFailureWithMessage_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result.Success();
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureWithMessage("test"));
    }
    
    [Fact]
    public void ShouldBeFailureWithMessage_Should_ReturnFailure_WhenFailureWithWrongMessage()
    {
        var result = Result.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureWithMessage("abc"));
    }
    
    [Fact]
    public void ShouldBeFailureWithMessage_Should_ReturnFailure_WhenFailureWithRightMessage()
    {
        var result = Result.Failure(_testFailure);
        result.ShouldBeFailureWithMessage(_testFailure.Message).ShouldBe(_testFailure);
    }
    
    [Fact]
    public void ShouldBeFailureThatSatisfies_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result.Success();
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureThatSatisfies(
            _ => throw new InvalidOperationException("Test exception"), "description"));
    }
    
    [Fact]
    public void ShouldBeFailureThatSatisfies_Should_ReturnFailure_WhenFailureWithWrongMessage()
    {
        var result = Result.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureThatSatisfies(
            failure => failure.Message == "abc", "description"));
    }
    
    [Fact]
    public void ShouldBeFailureThatSatisfies_Should_ReturnFailure_WhenFailureWithRightMessage()
    {
        var result = Result.Failure(_testFailure);
        result.ShouldBeFailureThatSatisfies(failure => failure.Code == _testFailure.Code, "code match")
            .ShouldBe(_testFailure);
    }

    [Fact]
    public void ShouldBeFailureOfType_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result.Success();
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureOfType<TestFailure>());
    }

    [Fact]
    public void ShouldBeFailureOfType_Should_ThrowFunctionalAssertException_WhenWrongFailureType()
    {
        var result = Result.Failure(new Failure("abc", "Test failure"));
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureOfType<TestFailure>());
    }

    [Fact]
    public void ShouldBeFailureOfType_Should_ReturnConvertedFailure_WhenRightFailureType()
    {
        var result = Result.Failure(_testFailure);
        var returned = result.ShouldBeFailureOfType<TestFailure>();
        returned.ShouldBeOfType<TestFailure>();
    }
}