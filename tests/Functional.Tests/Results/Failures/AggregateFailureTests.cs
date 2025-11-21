namespace Toarnbeike.Results.Failures;

/// <summary>
/// Tests for the <see cref="AggregateFailure"/> record.
/// </summary>
public class AggregateFailureTests
{
    [Fact]
    public void AggregateFailure_Should_BeCreatedFromFailureIEnumerable()
    {
        var innerFailure1 = new Failure("Property1", "Message1");
        var innerFailure2 = new Failure("Property2", "Message2");

        var failure = new AggregateFailure([innerFailure1, innerFailure2]);

        failure.Code.ShouldBe("aggregate");
        failure.Message.ShouldBe("Multiple failures occurred");
        failure.Failures.Count.ShouldBe(2);
    }

    [Fact]
    public void AggregateFailure_ShouldThrow_WhenCreatedWithNullFailures()
    {
        Should.Throw<ArgumentNullException>(() => new AggregateFailure(null!));
    }

    [Fact]
    public void AggregateFailure_ShouldThrow_WhenCreatedWithEmptyFailures()
    {
        Should.Throw<ArgumentException>(() => new AggregateFailure([]));
    }

    [Fact]
    public void AggregateFailure_Should_FlattenInnerAggregateFailures()
    {
        var innerFailure1 = new Failure("Property1", "Message1");
        var innerFailure2 = new Failure("Property2", "Message2");
        var failure = new AggregateFailure([innerFailure1, innerFailure2]);

        innerFailure1.ToString().ShouldBe("Message1");
        failure.Failures.Count.ShouldBe(2);
        failure.Failures.ShouldContain(innerFailure1);
        failure.Failures.ShouldContain(innerFailure2);
    }

    [Fact]
    public void Add_Should_AddNewFailureToAggregate()
    {
        var innerFailure1 = new Failure("Property1", "Message1");
        var innerFailure2 = new Failure("Property2", "Message2");
        var originalFailure = new AggregateFailure([innerFailure1, innerFailure2]);

        var newFailure = new Failure("new", "New failure");
        var updatedFailure = originalFailure.Add(newFailure);

        updatedFailure.Failures.Count.ShouldBe(3);
        updatedFailure.Failures.ShouldContain(innerFailure1);
        updatedFailure.Failures.ShouldContain(innerFailure2);
        updatedFailure.Failures.ShouldContain(newFailure);
    }

    [Fact]
    public void Combine_Should_CombineTwoAggregateFailures()
    {
        var innerFailure1 = new Failure("Property1", "Message1");
        var innerFailure2 = new Failure("Property2", "Message2");
        var originalFailure = new AggregateFailure([innerFailure1, innerFailure2]);

        var additionalFailure = new AggregateFailure([new Failure("new", "New failure")]);
        var mergedFailure = originalFailure.Combine(additionalFailure);
        mergedFailure.Failures.Count.ShouldBe(3);

        mergedFailure.Failures.ShouldContain(innerFailure1);
        mergedFailure.Failures.ShouldContain(innerFailure2);
        mergedFailure.Failures.ShouldContain(additionalFailure.Failures.Single());
    }

    [Fact]
    public void AggregateFailure_Should_BeAbleToChangeBaseProperties_UsingWithSyntax()
    {
        var innerFailure1 = new Failure("Property1", "Message1");
        var innerFailure2 = new Failure("Property2", "Message2");

        var originalFailure = new AggregateFailure([innerFailure1, innerFailure2]);

        var newFailure = originalFailure with { Code = "Something else" };
        newFailure.Code.ShouldBe("Something else");
    }
}