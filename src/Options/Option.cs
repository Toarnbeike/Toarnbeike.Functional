using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Options;

/// <summary>
/// An optional value can either be present (Some) or not (None).
/// Defaults to None, if created using the default struct constructor.
/// </summary>
/// <remarks>The <see cref="Option{TValue}"/> type is a lightweight alternative to nullable types or <see
/// cref="Nullable{T}"/> for representing optional values. 
/// It provides methods and operators for safely handling the presence or absence of a value. 
/// <typeparam name="TValue">The type of the value contained in the option.</typeparam>
public readonly union Option<TValue>(TValue, None) where TValue : notnull
{
    [Obsolete("Prefer using IsSome over HasValue for clarity. Will be removed in version 1.0.0")]
    public bool HasValue => Value is TValue;

    /// <summary>
    /// Gets a value indicating whether the option contains a value.
    /// </summary>
    public bool IsSome => Value is TValue;

    /// <summary>
    /// Gets a value indicating whether the option does not contain a value.
    /// </summary>
    public bool IsNone => Value is None;

    /// <summary>
    /// Attempts to retrieve the value stored in the current instance.
    /// </summary>
    /// <remarks>This method is useful for safely accessing the value of the instance without throwing an exception. 
    /// If the instance does not contain a value, the <paramref name="value"/> parameter will be set to <see langword="null"/>.</remarks>
    /// <param name="value">When this method returns <see langword="true"/>, contains the value stored in the instance. If the method
    /// returns <see langword="false"/>, the value is <see langword="null"/>.</param>
    /// <returns>True if the instance contains a value; otherwise, false.</returns>
    public bool TryGetValue([NotNullWhen(true)] out TValue? value)
    {
        if (Value is TValue some)
        {
            value = some;
            return true;
        }
        value = default;
        return false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Option{TValue}"/> class with the provided value.
    /// </summary>
    /// <remarks>Passing null to <see cref="Some"/> will throw. Use <see cref="Option{TValue}.None"/> instead.</remarks>
    /// <param name="value">The value of the option.</param>
    public static Option<TValue> Some(TValue? value) => value is null 
        ? throw new ArgumentNullException(nameof(value))
        : value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Option{TValue}"/> class with no value.
    /// </summary>
    /// <returns>An <see cref="Option{TValue}"/> instance with no value.</returns>
    public static Option<TValue> None => new None();

    public static implicit operator Option<TValue>(Option<None> _) => None;
}

public static class Option
{
    public static Option<None> None => Option<None>.None;

    public static Option<TValue> Some<TValue>(TValue value) where TValue : notnull 
        => Option<TValue>.Some(value);
}