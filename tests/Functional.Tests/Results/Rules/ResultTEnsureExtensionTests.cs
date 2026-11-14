using Shouldly;
using Toarnbeike.Functional.Results.Rules;
using Toarnbeike.Optional.TestExtensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Functional.Tests.Results.Rules;

public class ResultEnsureExtensionTests
{
    private readonly Option<int> _some = 42;
    private readonly Option<int> _none = Option.None;

    [Test]
    public void EnsureSome_WithSuccessResult_ValidatesSuccessfully()
    {
        var input = Result.Success();
        var result = input.EnsureSome(() => _some).AtLeast(40);
        result.ShouldBeSuccess().ShouldBeSome().ShouldBe(42);
    }

    [Test]
    public void EnsureSome_WithSuccessResult_ReturnsFailureForNone()
    {
        var input = Result.Success();
        var result = input.EnsureSome(() => _none).AtLeast(40);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("Option was None");
    }

    [Test]
    public void EnsureSome_WithSuccessResult_FailsValidation()
    {
        var input = Result.Success();
        var result = input.EnsureSome(() => _some).AtLeast(50);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("50");
    }

    [Test]
    public void EnsureSome_WithFailureResult_ReturnsFailure()
    {
        Result input = new TestFailure("Original error");
        var result = input.EnsureSome(() => _some).AtLeast(40);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original error");
    }

    [Test]
    public void EnsureSome_WithSuccessResult_FailsValidation_WithCustomMessage()
    {
        var input = Result.Success();
        var result = input.EnsureSome(() => _some).AtLeast(50, "Custom error message");
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("Custom error message");
    }

    [Test]
    public void EnsureWhenSome_WithSuccessResult_ValidatesSuccessfully()
    {
        var input = Result.Success();
        var result = input.EnsureWhenSome(() => _some).AtLeast(40);
        result.ShouldBeSuccess().ShouldBeSome().ShouldBe(42);
    }

    [Test]
    public void EnsureWhenSome_WithSuccessResult_ReturnsSuccessForNone()
    {
        var input = Result.Success();
        var result = input.EnsureWhenSome(() => _none).AtLeast(40);
        result.ShouldBeSuccess();
        var output = result.ShouldBeSuccess();
        output.ShouldBeOfType<Option<int>>();
    }

    [Test]
    public void EnsureWhenSome_WithSuccessResult_FailsValidation()
    {
        var input = Result.Success();
        var result = input.EnsureWhenSome(() => _some).AtLeast(50);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("50");
    }

    [Test]
    public void EnsureWhenSome_WithFailureResult_ReturnsFailure()
    {
        Result input = new TestFailure("Original error");
        var result = input.EnsureWhenSome(() => _some).AtLeast(40);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original error");
    }

    [Test]
    public void EnsureWhenSome_WithSuccessResult_FailsValidation_WithCustomMessage()
    {
        var input = Result.Success();
        var result = input.EnsureWhenSome(() => _some).AtLeast(50, "Custom error message");
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("Custom error message");
    }
}

public class ResultTEnsureExtensionTests_WithValueSelector
{
    private record NestedOption(Option<int> InnerValue)
    {
        public static NestedOption Some => new(Option.Some(42));
        public static NestedOption None => new(Option<int>.None());
    }

    [Test]
    public void EnsureSome_WithSomeValue_ValidatesSuccessfully()
    {
        var input = Result.Success(NestedOption.Some);
        var result = input.EnsureSome(nested => nested.InnerValue).AtLeast(40);

        result.ShouldBe(input);

        var output = result.ShouldBeSuccess();
        output.ShouldBeOfType<NestedOption>();
        output.InnerValue.ShouldBeSome().ShouldBe(42);
    }

    [Test]
    public void EnsureSome_WithNoneValue_ReturnsFailure()
    {
        var input = Result.Success(NestedOption.None);
        var result = input.EnsureSome(nested => nested.InnerValue).AtLeast(40);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("Option was None");
    }

    [Test]
    public void EnsureSome_WithSomeValue_FailsValidation()
    {
        var input = Result.Success(NestedOption.Some);
        var result = input.EnsureSome(nested => nested.InnerValue).AtLeast(50);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("50");
    }

    [Test]
    public void EnsureSome_WithSomeValue_FailsValidation_WithCustomMessage()
    {
        var input = Result.Success(NestedOption.Some);
        var result = input.EnsureSome(nested => nested.InnerValue).AtLeast(50, "Custom error message");
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("Custom error message");
    }

    [Test]
    public void EnsureSome_WithFailureResult_ReturnsFailure()
    {
        Result<NestedOption> input = new TestFailure("Original error");
        var result = input.EnsureSome(nested => nested.InnerValue).AtLeast(40);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original error");
    }

    [Test]
    public void EnsureWhenSome_WithSomeValue_ValidatesSuccessfully()
    {
        var input = Result.Success(NestedOption.Some);
        var result = input.EnsureWhenSome(nested => nested.InnerValue).AtLeast(40);
        result.ShouldBe(input);
        var output = result.ShouldBeSuccess();
        output.ShouldBeOfType<NestedOption>();
        output.InnerValue.ShouldBeSome().ShouldBe(42);
    }

    [Test]
    public void EnsureWhenSome_WithNoneValue_ReturnsSuccess()
    {
        var input = Result.Success(NestedOption.None);
        var result = input.EnsureWhenSome(nested => nested.InnerValue).AtLeast(40);
        result.ShouldBe(input);
        var output = result.ShouldBeSuccess();
        output.ShouldBeOfType<NestedOption>();
        output.InnerValue.ShouldBeNone();
    }

    [Test]
    public void EnsureWhenSome_WithSomeValue_FailsValidation()
    {
        var input = Result.Success(NestedOption.Some);
        var result = input.EnsureWhenSome(nested => nested.InnerValue).AtLeast(50);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("50");
    }

    [Test]
    public void EnsureWhenSome_WithFailureResult_ReturnsFailure()
    {
        Result<NestedOption> input = new TestFailure("Original error");
        var result = input.EnsureWhenSome(nested => nested.InnerValue).AtLeast(40);
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original error");
    }

    [Test]
    public void EnsureWhenSome_WithSomeValue_FailsValidation_WithCustomMessage()
    {
        var input = Result.Success(NestedOption.Some);
        var result = input.EnsureWhenSome(nested => nested.InnerValue).AtLeast(50, "Custom error message");
        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldContain("Custom error message");
    }
}


public class ResultTEnsureExtensionTests_WithPropertyName
{
    [Test]
    public void EnsureSome_WithSomeValue_ValidatesSuccessfully()
    {
        var input = Result.Success(Option.Some(42));
        var result = input.EnsureSome("TestProperty").AtLeast(40);

        result.ShouldBe(input);

        var output = result.ShouldBeSuccess();
        output.ShouldBeOfType<Option<int>>();
        output.ShouldBeSome().ShouldBe(42);
    }

    [Test]
    public void EnsureSome_WithNoneValue_ReturnsFailure()
    {
        var input = Result.Success(Option<int>.None());
        var result = input.EnsureSome("TestProperty").AtLeast(40);

        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("TestProperty");
        failure.Message.ShouldContain("Option was None");
    }

    [Test]
    public void EnsureSome_WithSomeValue_FailsValidation()
    {
        var input = Result.Success(Option.Some(30));
        var result = input.EnsureSome("TestProperty").AtLeast(40);

        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("TestProperty");
        failure.Message.ShouldContain("40");
    }

    [Test]
    public void EnsureSome_WithFailureResult_ReturnsFailure()
    {
        Result<Option<int>> input = new TestFailure("Original error");
        var result = input.EnsureSome("TestProperty").AtLeast(40);

        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original error");
    }

    [Test]
    public void EnsureWhenSome_WithSomeValue_ValidatesSuccessfully()
    {
        var input = Result.Success(Option.Some(42));
        var result = input.EnsureWhenSome("TestProperty").AtLeast(40);

        result.ShouldBe(input);

        var output = result.ShouldBeSuccess();
        output.ShouldBeOfType<Option<int>>();
        output.ShouldBeSome().ShouldBe(42);
    }

    [Test]
    public void EnsureWhenSome_WithNoneValue_ReturnsSuccess()
    {
        var input = Result.Success(Option<int>.None());
        var result = input.EnsureWhenSome("TestProperty").AtLeast(40);

        result.ShouldBe(input);

        var output = result.ShouldBeSuccess();
        output.ShouldBeOfType<Option<int>>();
        output.ShouldBeNone();
    }

    [Test]
    public void EnsureWhenSome_WithSomeValue_FailsValidation()
    {
        var input = Result.Success(Option.Some(30));
        var result = input.EnsureWhenSome("TestProperty").AtLeast(40);

        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.ParameterName.ShouldBe("TestProperty");
        failure.Message.ShouldContain("40");
    }

    [Test]
    public void EnsureWhenSome_WithFailureResult_ReturnsFailure()
    {
        Result<Option<int>> input = new TestFailure("Original error");
        var result = input.EnsureWhenSome("TestProperty").AtLeast(40);

        result.ShouldBeFailure();
        var failure = result.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Original error");
    }
}
