using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Results;

/// <summary>
/// Represents the outcome of an operation that either succeeded without a value or failed with an <see cref="Results.Failure"/>.
/// </summary>
/// <remarks>
/// Use <see cref="Success"/> to create a successful result without a value,
/// or <see cref="Failure(Results.Failure)"/> to create a failed result.
/// </remarks>
public partial class Result
{
    private readonly Failure? _failure;

    /// <summary>
    /// True when the result represents a successful outcome, false otherwise.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// True when the result represents a failure, false otherwise.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Attempts to retrieve the reason why to operation failed.
    /// </summary>
    /// <param name="failure">When this method returns, contains the reason if the result is a failure; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the result is a failure; otherwise, <c>false</c>.</returns>
    public bool TryGetFailure([MaybeNullWhen(false)] out Failure failure)
    {
        failure = _failure;
        return IsFailure;
    }

    /// <summary>
    /// Creates a new <see cref="Result"/> representing a successful operation.
    /// </summary>
    /// <returns>A success <see cref="Result"/> instance.</returns>
    public static Result Success() => new(true, null);

    /// <summary>
    /// Creates a new <see cref="Result"/> representing a failure with the specified <paramref name="failure"/>.
    /// </summary>
    /// <param name="failure">The reason why the operation failed.</param>
    /// <returns>A failure <see cref="Result"/> instance containing the specified <paramref name="failure"/>.</returns>
    public static Result Failure(Failure failure) => new(false, failure);

    /// <summary>
    /// Creates a new successful <see cref="Result{TValue}"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value to encapsulate.</param>
    /// <returns>A success <see cref="Result{TValue}"/> instance containing the specified value.</returns>
    public static Result<TValue> Success<TValue>(TValue value) => Result<TValue>.Success(value);

    /// <summary>
    /// Implicitly converts an <see cref="Results.Failure"/> to a failed <see cref="Result"/>.
    /// </summary>
    /// <param name="failure">The failure to convert.</param>
    public static implicit operator Result(Failure failure) => Failure(failure);

    private Result(bool isSuccess, Failure? failure) => (IsSuccess, _failure) = (isSuccess, failure);
    
    /// <summary>
    /// Override the ToString method to return the objects ToString if the Option has a value, and an empty string if not.
    /// </summary>
    /// <returns>The result of <c>ToString()</c> on the contained value, or an empty string if no value is present.</returns>
    public override string ToString() => IsSuccess ? "Success" : $"Failure({_failure!.GetType().Name})";

    /// <summary>
    /// Value for the debugger display, which shows the value if present or a placeholder if not.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [ExcludeFromCodeCoverage(Justification = "Debugger display is used by the debugger only.")]
    // ReSharper disable once UnusedMember.Local
    private string DebuggerDisplay => IsSuccess ? "Success()" : $"Failure({_failure!.GetType().Name})";
}