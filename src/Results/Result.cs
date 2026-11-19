using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Results;

[DebuggerDisplay("{DebuggerToString(),nq}")]
public readonly union Result(Success, Failure) : IResult
{
    /// <inheritdoc />
    public bool IsSuccess => !IsFailure;
    /// <inheritdoc />
    public bool IsFailure => this is Failure;

    /// <inheritdoc/>
    public bool TryGetFailure([NotNullWhen(true)] out Failure? failure)
    {
        if (this is Failure f)
        {
            failure = f;
            return true;
        }
        failure = null;
        return false;
    }

    /// <summary>
    /// Debugger string representation of the object.
    /// </summary>
    internal string DebuggerToString() => this switch
    {
        Success _ => "Success",
        Failure failure => failure.DebuggerToString()
    };


    public static Result Success() => new Success();
    public static Result<TValue> Success<TValue>(TValue value) where TValue : notnull => value;

    public static Result Failure(Failure failure) => failure;
    public static Result<TValue> Failure<TValue>(Failure failure) where TValue : notnull => failure;

}

/// <summary>
/// Represents the outcome of an operation that either succeeded with a value of type <typeparamref name="TValue"/>
/// succeeded without value (i.e., a void success), or failed with an <see cref="Results.Failure"/>.
/// </summary>
/// <remarks>
/// If the operation is successful, the value can be retrieved using <see cref="TryGetValue(out TValue)"/>.
/// If it failed, the failure can be inspected using <see cref="TryGetFailure(out Results.Failure)"/>.
/// </remarks>
/// <typeparam name="TValue">The type of the success value.</typeparam>
[DebuggerDisplay("{DebuggerToString(),nq}")]
public readonly union Result<TValue>(TValue, Failure) : IResult where TValue : notnull
{
    /// <inheritdoc />
    public bool IsSuccess => !IsFailure;

    /// <inheritdoc />
    public bool IsFailure => this is Failure;

    /// <inheritdoc/>
    public bool TryGetFailure([NotNullWhen(true)] out Failure? failure)
    {
        if (this is Failure f)
        {
            failure = f;
            return true;
        }
        failure = null;
        return false;
    }

    /// <summary>
    /// Debugger string representation of the object.
    /// </summary>
    internal string DebuggerToString() => this switch
    {
        TValue value => $"Success: {value}",
        Failure failure => failure.DebuggerToString(),
        null => "Success with value [null]"
    };

    public static implicit operator Result(Result<TValue> valueResult) => valueResult switch
    {
        TValue => Result.Success(),
        Failure failure => Result.Failure(failure),
        null => Result.Success()
    };
}

public static class ResultExtensions
{
    extension<TValue>(Result<TValue> result) where TValue : notnull
    {
        /// <summary>
        /// Attempts to retrieve the value of a successful result.
        /// </summary>
        /// <param name="value">When this method returns, contains the value if the result is successful; otherwise, the default value.</param>
        /// <returns><c>true</c> if the result is successful; otherwise, <c>false</c>.</returns>
        public bool TryGetValue([NotNullWhen(true)] out TValue? value)
        {
            if (result is TValue v)
            {
                value = v;
                return true;
            }
            value = default;
            return false;
        }
    }
}