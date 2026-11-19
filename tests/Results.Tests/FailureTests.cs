using Toarnbeike.Functional.TestExtensions;

namespace Toarnbeike.Results;

public class FailureTests
{
    [Test]
    public void ToString_Should_ReturnMessage_ForFailure()
    {
        var failureIdentifier = Guid.NewGuid().ToString();
        Failure failure = new TestFailure(failureIdentifier);
        failure.Message.ShouldBe(failureIdentifier);
    }

    [Test]
    public void ValidationFailure_Should_ReturnFormattedMessage()
    {
        var propertyName = "TestProperty";
        var validationMessage = "must not be null";
        var validationFailure = new ValidationFailure(propertyName, validationMessage);
        var expectedMessage = $"Validation failed for property '{propertyName}': {validationMessage}";
        validationFailure.Message.ShouldBe(expectedMessage);
    }

    [Test]
    public void DomainFailure_Should_ReturnMessage()
    {
        var message = "Domain rule violated";
        Failure domainFailure = new DomainFailure(message);
        domainFailure.Message.ShouldBe(message);
    }

    [Test]
    public void ExceptionFailure_Should_ReturnFormattedMessage()
    {
        var exception = new InvalidOperationException("Invalid operation");
        Failure exceptionFailure = new ExceptionFailure(exception, "Additional context");
        var expectedMessage = $"Exception occurred: {exception.Message} - Additional context";
        exceptionFailure.Message.ShouldBe(expectedMessage);
    }

    [Test]
    public void ExceptionFailure_Should_ReturnMessage_WhenNoAdditionalContext()
    {
        var exception = new InvalidOperationException("Invalid operation");
        Failure exceptionFailure = new ExceptionFailure(exception);
        var expectedMessage = $"Exception occurred: {exception.Message}";
        exceptionFailure.Message.ShouldBe(expectedMessage);
    }

    [Test]
    public void SystemFailure_Should_ReturnFormattedMessage()
    {
        var systemName = "ExternalSystem";
        var message = "System error occurred";
        Failure systemFailure = new SystemFailure(systemName, message);
        var expectedMessage = $"System '{systemName}' reported an error: {message}";
        systemFailure.Message.ShouldBe(expectedMessage);
    }
}
