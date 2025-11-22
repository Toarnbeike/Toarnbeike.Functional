namespace Toarnbeike.Results.TestExtensions;

public class TestExtensionsResultTValueTests
{
    private readonly TestFailure _testFailure = new("test", "Test failure");

    [Fact]
    public void ShouldBeSuccess_Should_ReturnValue_WhenSuccess()
    {
        var result = Result<int>.Success(42);
        var returned = result.ShouldBeSuccess();
        returned.ShouldBe(42);
    }

    [Fact]
    public void ShouldBeSuccess_Should_ThrowFunctionalAssertException_WhenFailure()
    {
        var result = Result<int>.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeSuccess());
    }

    [Fact]
    public void ShouldBeSuccessWithValue_Should_ReturnValue_WhenCorrectSuccess()
    {
        var result = Result<int>.Success(42);
        var returned = result.ShouldBeSuccessWithValue(42);
        returned.ShouldBe(42);
    }

    [Fact]
    public void ShouldBeSuccessWithValue_Should_ReturnValue_WhenCorrectSuccess_OfCollection()
    {
        var result = Result<IEnumerable<int>>.Success([1, 2, 3]);
        var returned = result.ShouldBeSuccessWithValue([1, 2, 3]);
        returned.ShouldBe([1, 2, 3]);
    }
    
    [Fact]
    public void ShouldBeSuccessWithValue_Should_ThrowFunctionalAssertException_WhenWrongSuccess()
    {
        var result = Result<int>.Success(42);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeSuccessWithValue(40));
    }

    [Fact]
    public void ShouldBeSuccessWithValue_Should_ThrowFunctionalAssertException_WhenWrongSuccess_OfCollection()
    {
        var result = Result<IEnumerable<int>>.Success([-1, 2, 3]);
        var exception = Should.Throw<FunctionalAssertException>(() => result.ShouldBeSuccessWithValue([1, 2, 3]));
        exception.Message.ShouldBe("Expected success result with value '[1, 2, 3]', but got '[-1, 2, 3]'.");
    }
    
    [Fact]
    public void ShouldBeSuccessWithValue_Should_ThrowFunctionalAssertException_WhenFailure()
    {
        var result = Result<int>.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeSuccessWithValue(42));
    }

    [Fact]
    public void ShouldBeSuccessThatSatisfies_Should_ReturnValue_WhenCorrectSuccess()
    {
        var result = Result<int>.Success(42);
        var returned = result.ShouldBeSuccessThatSatisfies(value => value > 0, "positive");
        returned.ShouldBe(42);
    }

    [Fact]
    public void ShouldBeSuccessThatSatisfies_Should_ThrowFunctionalAssertException_WhenWrongSuccess()
    {
        var result = Result<int>.Success(42);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeSuccessThatSatisfies(value => value < 0, "negative"));
    }

    [Fact]
    public void ShouldBeSuccessThatSatisfies_Should_ThrowFunctionalAssertException_WhenFailure()
    {
        var result = Result<int>.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeSuccessThatSatisfies(value => value > 0, "positive"));
    }
    
    [Fact]
    public void ShouldBeFailure_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result<int>.Success(42);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailure());
    }

    [Fact]
    public void ShouldBeFailure_Should_ReturnFailure_WhenFailure()
    {
        var result = Result<int>.Failure(_testFailure);
        result.ShouldBeFailure().ShouldBe(_testFailure);
    }
    
    [Fact]
    public void ShouldBeFailureWithCode_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result<int>.Success(42);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureWithCode("test"));
    }
    
    [Fact]
    public void ShouldBeFailureWithCode_Should_ReturnFailure_WhenFailureWithWrongCode()
    {
        var result = Result<int>.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureWithCode("abc"));
    }
    
    [Fact]
    public void ShouldBeFailureWithCode_Should_ReturnFailure_WhenFailureWithRightCode()
    {
        var result = Result<int>.Failure(_testFailure);
        result.ShouldBeFailureWithCode(_testFailure.Code).ShouldBe(_testFailure);
    }
    
    [Fact]
    public void ShouldBeFailureWithMessage_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result<int>.Success(42);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureWithMessage("test"));
    }
    
    [Fact]
    public void ShouldBeFailureWithMessage_Should_ReturnFailure_WhenFailureWithWrongMessage()
    {
        var result = Result<int>.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureWithMessage("abc"));
    }
    
    [Fact]
    public void ShouldBeFailureWithMessage_Should_ReturnFailure_WhenFailureWithRightMessage()
    {
        var result = Result<int>.Failure(_testFailure);
        result.ShouldBeFailureWithMessage(_testFailure.Message).ShouldBe(_testFailure);
    }
    
    [Fact]
    public void ShouldBeFailureThatSatisfies_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result<int>.Success(42);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureThatSatisfies(
            _ => throw new InvalidOperationException("Test exception"), "description"));
    }
    
    [Fact]
    public void ShouldBeFailureThatSatisfies_Should_ReturnFailure_WhenFailureWithWrongMessage()
    {
        var result = Result<int>.Failure(_testFailure);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureThatSatisfies(
            failure => failure.Message == "abc", "description"));
    }
    
    [Fact]
    public void ShouldBeFailureThatSatisfies_Should_ReturnFailure_WhenFailureWithRightMessage()
    {
        var result = Result<int>.Failure(_testFailure);
        result.ShouldBeFailureThatSatisfies(failure => failure.Code == _testFailure.Code, "code match")
            .ShouldBe(_testFailure);
    }

    [Fact]
    public void ShouldBeFailureOfType_Should_ThrowFunctionalAssertException_WhenSuccess()
    {
        var result = Result<int>.Success(42);
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureOfType<int, TestFailure>());
    }

    [Fact]
    public void ShouldBeFailureOfType_Should_ThrowFunctionalAssertException_WhenWrongFailureType()
    {
        var result = Result<int>.Failure(new Failure("abc", "Test failure"));
        Should.Throw<FunctionalAssertException>(() => result.ShouldBeFailureOfType<int, TestFailure>());
    }

    [Fact]
    public void ShouldBeFailureOfType_Should_ReturnConvertedFailure_WhenRightFailureType()
    {
        var result = Result<int>.Failure(_testFailure);
        var returned = result.ShouldBeFailureOfType<int, TestFailure>();
        returned.ShouldBeOfType<TestFailure>();
    }
}