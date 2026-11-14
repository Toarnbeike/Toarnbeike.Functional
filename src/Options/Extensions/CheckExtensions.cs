namespace Toarnbeike.Options.Extensions;

public static class CheckExtensions
{
    /// <param name="option">The option this method is applied to.</param>
    extension<TValue>(Option<TValue> option) where TValue : notnull
    {
        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// Returns None if the predicate does not match.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public Option<TValue> Check(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return option switch
            {
                TValue some when predicate(some) => option,
                _ => Option.None
            };
        }

        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.        
        /// Returns None if the predicate does not match.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public async Task<Option<TValue>> CheckAsync(Func<TValue, Task<bool>> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return option switch
            {
                TValue some when await predicate(some) => option,
                _ => Option.None
            };
        }
    }

    /// <param name="optionTask">The Task{Option} this method is applied to.</param>
    extension<TValue>(Task<Option<TValue>> optionTask) where TValue : notnull
    {
        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// Returns None if the predicate does not match.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public async Task<Option<TValue>> Check(Func<TValue, bool> predicate)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Check(predicate);
        }

        /// <summary>
        /// Apply a predicate to the option and only retain the value if the current value matches the predicate.
        /// Returns None if the predicate does not match.
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        public async Task<Option<TValue>> CheckAsync(Func<TValue, Task<bool>> predicate)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.CheckAsync(predicate).ConfigureAwait(false);
        }
    }
}