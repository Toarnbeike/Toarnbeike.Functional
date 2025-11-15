using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Eithers;

/// <summary>
/// Represents a type that can hold one of two possible values, either a value of type TLeft
/// or a value of type TRight. This can be used to model computations or outcomes that may
/// produce two distinct types of results.
/// <remarks>
/// <typeparamref name="TLeft"/> is typically used to represent an error condition, whereas
/// <typeparamref name="TRight"/> is typically used to represent a successful result.
/// </remarks>
/// </summary>
/// <typeparam name="TLeft">The type of the first possible value.</typeparam>
/// <typeparam name="TRight">The type of the second possible value.</typeparam>
public class Either<TLeft, TRight>
{
    private readonly TLeft? _left;
    private readonly TRight? _right;
    private readonly bool _isRight;

    /// <summary>
    /// Determines whether the current instance represents a left value.
    /// </summary>
    /// <param name="left">The output parameter that receives the left value if the instance represents a left value; otherwise, it is null.</param>
    /// <returns><c>true</c> if the instance holds a left value; otherwise, <c>false</c>.</returns>
    public bool IsLeft([MaybeNullWhen(false)] out TLeft left)
    {
        left = _left;
        return !_isRight;
    }

    /// <summary>
    /// Determines whether the current instance represents a right value.
    /// </summary>
    /// <param name="right">The output parameter that receives the right value if the instance represents a right value; otherwise, it is null.</param>
    /// <returns><c>true</c> if the instance holds a right value; otherwise, <c>false</c>.</returns>
    public bool IsRight([MaybeNullWhen(false)] out TRight right)
    {
        right = _right;
        return _isRight;
    }

    internal Either(TLeft? left, TRight? right, bool isRight)
    {
        (_left, _right, _isRight) = isRight switch
        {
            true when right is null => throw new ArgumentNullException(nameof(right), "Either must have a value."),
            false when left is null => throw new ArgumentNullException(nameof(left), "Either must have a value."),
            _ => (left, right, isRight)
        };
    }
    
    /// <summary>
    /// Helper method that returns the value of the left or right side of the Either.
    /// Used for internal extension methods on Either.
    /// </summary>
    /// <param name="left">The value of the left operator, not null if either has the left value.</param>
    /// <param name="right">The value of the right operator, not null if either has the right value.</param>
    /// <returns><c>true</c> if the left value is filled, <c>false</c> otherwise.</returns>
    internal bool IsLeft([MaybeNullWhen(false)] out TLeft left, [MaybeNullWhen(true)] out TRight right)
    {
        left = _left;
        right = _right;
        return !_isRight;
    }
}