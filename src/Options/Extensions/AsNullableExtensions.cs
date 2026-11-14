namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Convert an Option to its nullable representation.
/// Due to implementation differences between classes and structs, these extensions are split in two:
/// <para> - AsNullable() for class type values </para>
/// <para> - AsNullableValue() for struct type values </para>
/// </summary>
public static class AsNullableExtensions
{
    /// <summary>
    /// Convert the <see cref="Option{TValue}"/> to its nullable representation.
    /// </summary>
    /// <remarks>Useful at integration boundaries (e.g. EF Core) that require nullable CLR types. </remarks>
    /// <returns>Returns the <see cref="Nullable{TValue}"/> equivalent to the <see cref="Option{TValue}"/>.</returns>
    public static TValue? AsNullable<TValue>(this Option<TValue> option) where TValue : class => option switch
    {
        TValue some => some,
        None => null
    };

    /// <summary>
    /// Awaits the task and converts the <see cref="Option{TValue}"/> to its nullable representation.
    /// </summary>
    public static async Task<TValue?> AsNullable<TValue>(this Task<Option<TValue>> optionTask) where TValue : class =>
        (await optionTask.ConfigureAwait(false)).AsNullable();

    /// <summary>
    /// Convert the <see cref="Option{TValue}"/> to its nullable representation.
    /// </summary>
    /// <remarks>Useful at integration boundaries (e.g. EF Core) that require nullable CLR types. </remarks>
    /// <returns>Returns the <see cref="Nullable{TValue}"/> equivalent to the <see cref="Option{TValue}"/>.</returns>
    public static TValue? AsNullableValue<TValue>(this Option<TValue> option) where TValue : struct => option switch
    {
        TValue some => some,
        None => null
    };

    /// <summary>
    /// Awaits the task and converts the <see cref="Option{TValue}"/> to its nullable representation.
    /// </summary>
    public static async Task<TValue?> AsNullableValue<TValue>(this Task<Option<TValue>> optionTask) where TValue : struct =>
        (await optionTask.ConfigureAwait(false)).AsNullableValue();
}