using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Results;

/// <summary>
/// Represents the outcome of an operation that either succeeded with a value of type <typeparamref name="TValue"/>
/// or failed with an <see cref="Results.Failure"/>.
/// </summary>
/// <remarks>
/// If the operation is successful, the value can be retrieved using <see cref="TryGetValue(out TValue)"/>.
/// If it failed, the failure can be inspected using <see cref="TryGetFailure"/>.
/// </remarks>
/// <typeparam name="TValue">The type of the success value.</typeparam>
public class Result<TValue>
{
    private readonly TValue? _value;
    private readonly Failure? _failure;

    /// <summary>
    /// Gets a value indicating whether the result represents a successful outcome.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the result represents a failure.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Attempts to retrieve the value of a successful result.
    /// </summary>
    /// <param name="value">When this method returns, contains the value if the result is successful; otherwise, the default value.</param>
    /// <returns><c>true</c> if the result is successful; otherwise, <c>false</c>.</returns>
    public bool TryGetValue([NotNullWhen(true)] out TValue? value)
    {
        value = _value;
        return IsSuccess;
    }

    /// <summary>
    /// Attempts to retrieve both the value, if success, and the failure reason, if failure, of the result.
    /// </summary>
    /// <param name="value">When this method returns <c>true</c>, contains the value of the result; otherwise, the default value.</param>
    /// <param name="failure">When this method returns <c>false</c>, contains the reason the result is a failure; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the result is successful; otherwise, <c>false</c>.</returns>
    internal bool TryGetContents([MaybeNullWhen(false)] out TValue value, [MaybeNullWhen(true)] out Failure failure)
    {
        value = _value;
        failure = _failure;
        return IsSuccess;
    }

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
    /// Creates a new <see cref="Result{TValue}"/> representing a successful operation with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to encapsulate.</param>
    /// <returns>A success <see cref="Result{TValue}"/> instance containing the specified value.</returns>
    public static Result<TValue> Success(TValue value) => new(true, value, null);

    /// <summary>
    /// Creates a new <see cref="Result{TValue}"/> representing a failure with the specified <paramref name="failure"/>.
    /// </summary>
    /// <param name="failure">The reason why the operation failed.</param>
    /// <returns>A failure <see cref="Result{TValue}"/> instance containing the specified failure reason.</returns>
    public static Result<TValue> Failure(Failure failure) => new(false, default, failure);

    /// <summary>
    /// Implicitly converts a <typeparamref name="TValue"/> to a successful <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Result<TValue>(TValue value) => Success(value);

    /// <summary>
    /// Implicitly converts an <see cref="Results.Failure"/> to a failed <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="failure">The reason the result failed.</param>
    public static implicit operator Result<TValue>(Failure failure) => Failure(failure);

    /// <summary>
    /// Implicitly converts a <see cref="Result{TValue}"/> to a non-generic <see cref="Result"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{TValue}"/> to convert.</param>
    /// <returns>A <see cref="Result"/> representing success or failure without a value.</returns>
    public static implicit operator Result(Result<TValue> result) =>
        result.TryGetFailure(out var failure) ? Result.Failure(failure) : Result.Success();

    private Result(bool isSuccess, TValue? value, Failure? failure) =>
        (IsSuccess, _value, _failure) = (isSuccess, value, failure);

    /// <summary>
    /// Override the ToString method to return the objects ToString if the Option has a value, and an empty string if not.
    /// </summary>
    /// <returns>The result of <c>ToString()</c> on the contained value, or an empty string if no value is present.</returns>
    public override string ToString() =>
        TryGetValue(out var value) ? value.ToString() ?? typeof(TValue).Name : string.Empty;

    /// <summary>
    /// Value for the debugger display, which shows the value if present or a placeholder if not.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [ExcludeFromCodeCoverage(Justification = "Debugger display is used by the debugger only.")]
    // ReSharper disable once UnusedMember.Local
    private string DebuggerDisplay => IsSuccess 
            ? $"Success({_value?.ToString() ?? typeof(TValue).Name})" 
            : $"Failure({_failure!.GetType().Name})";
}
