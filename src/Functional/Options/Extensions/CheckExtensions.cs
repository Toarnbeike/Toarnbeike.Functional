namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Provides extension methods for applying a predicate to the value of an <see cref="Option{TValue}"/>.
/// </summary>
public static class CheckExtensions
{
    /// <param name="option">The option this method is applied to.</param>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public Option<TValue> Check(Func<TValue, bool> predicate) =>
            !option.TryGetValue(out var value) ? Option.None : predicate(value) ? option : Option.None;

        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public async Task<Option<TValue>> CheckAsync(Func<TValue, Task<bool>> predicate) =>
            !option.TryGetValue(out var value) ? Option.None : await predicate(value) ? option : Option.None;
    }

    /// <param name="optionTask">The Task{Option} this method is applied to.</param>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public async Task<Option<TValue>> Check(Func<TValue, bool> predicate) =>
            Check(await optionTask, predicate);

        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public async Task<Option<TValue>> CheckAsync(Func<TValue, Task<bool>> predicate) =>
            await CheckAsync(await optionTask, predicate);
    }
}
