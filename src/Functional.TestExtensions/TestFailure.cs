using Toarnbeike.Results;

namespace Toarnbeike.Functional.TestExtensions;

/// <summary>
/// Failure used for testing purposes.
/// </summary>
/// <remarks>Although this is not really a domainFailure, it is used to test failure scenarios.</remarks>
/// <param name="Identifier">The identifier for the failure.</param>
public sealed record TestFailure(string Identifier) : DomainFailure(Identifier)
{
    /// <summary>
    /// Static instance of a <see cref="TestFailure"/> with the default instance.
    /// </summary>
    public static TestFailure Instance => new("TestFailure");
}
