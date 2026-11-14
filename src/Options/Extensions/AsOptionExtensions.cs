namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Convert a nullable value to an Option.
/// Due to implementation differences between classes and structs, these extensions are duplicated for classes and structs.
/// </summary>
public static class AsOptionExtensions
{
    /// <summary>
    /// Converts a nullable object to an <see cref="Option{TValue}"/>.
    /// </summary>
    public static Option<TValue> AsOption<TValue>(this TValue? value)
        where TValue : class =>
        value is not null ? value : Option.None;

    /// <summary>
    /// Converts a nullable object to an <see cref="Option{TValue}"/>.
    /// </summary>
    public static async Task<Option<TValue>> AsOption<TValue>(this Task<TValue?> task)
        where TValue : class =>
        (await task.ConfigureAwait(false)).AsOption();

    /// <summary>
    /// Converts a nullable object to an <see cref="Option{TValue}"/>.
    /// </summary>
    public static Option<TValue> AsOption<TValue>(this TValue? value)
        where TValue : struct =>
        value is not null ? value.Value : Option.None;

    /// <summary>
    /// Converts a nullable object to an <see cref="Option{TValue}"/>.
    /// </summary>
    public static async Task<Option<TValue>> AsOption<TValue>(this Task<TValue?> task)
        where TValue : struct =>
        (await task.ConfigureAwait(false)).AsOption();
}