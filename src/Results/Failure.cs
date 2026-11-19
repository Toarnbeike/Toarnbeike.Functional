using System.Diagnostics;

namespace Toarnbeike.Results;

/// <summary>
/// Represents a failing outcome of a Result.
/// Unions the different types of failures that can occur in the system, such as validation failures, domain failures, exceptions, and external system failures.
/// </summary>
[DebuggerDisplay("{DebuggerToString(),nq}")]
public closed record Failure(string Message)
{
    /// <summary>
    /// Returns the <see cref="Message"/> as the string representation of this failure reason.
    /// </summary>
    public override string ToString() => Message;

    /// <summary>
    /// Debugger string representation of the object.
    /// </summary>
    internal string DebuggerToString() => $"Failure: {Message}";
}

/// <summary>
/// A validation failure for a property.
/// </summary>
/// <param name="Property">The name of the property that failed validation.</param>
/// <param name="ValidationMessage">The message describing the validation failure.</param>
public record ValidationFailure(string Property, string ValidationMessage) 
    : Failure($"Validation failed for property '{Property}': {ValidationMessage}");

/// <summary>
/// A failure caused by a domain rule.
/// </summary>
/// <param name="Message">The message describing the domain failure.</param>
public record DomainFailure(string Message) : Failure(Message);

/// <summary>
/// A failure caused by an exception.
/// </summary>
/// <param name="Exception">The exception that caused the failure.</param>
/// <param name="Message">An optional message describing the failure. If none is provided, the exception message is used.</param>
public record ExceptionFailure(Exception Exception, string? Message = null) 
    : Failure($"Exception occurred: {Exception.Message}{(Message is not null ? $" - {Message}" : string.Empty)}");

/// <summary>
/// A failure reported by and external system.
/// </summary>
/// <param name="System">The name of the external system.</param>
/// <param name="Message">The message describing the failure.</param>
public record SystemFailure(string System, string Message) 
    : Failure($"System '{System}' reported an error: {Message}");

/// <summary>
/// A failure that consists of multiple inner failures.
/// </summary>
/// <param name="Failures">The failures that make up this aggregate failure.</param>
public record AggregateFailure(IEnumerable<Failure> Failures) : Failure($"Multiple failures occurred: {string.Join("; ", Failures.Select(f => f.Message))}");