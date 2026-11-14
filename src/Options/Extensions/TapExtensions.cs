namespace Toarnbeike.Options.Extensions;

public static class TapExtensions
{
    /// <param name="option">The option to operate on.</param>
    extension<TValue>(Option<TValue> option) where TValue : notnull
    {
        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public Option<TValue> Tap(Action<TValue> action)
        {
            ArgumentNullException.ThrowIfNull(action);
            if (option.TryGetValue(out var value))
            {
                action(value);
            }

            return option;
        }

        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> TapAsync(Func<TValue, Task> action)
        {
            ArgumentNullException.ThrowIfNull(action);
            if (option.TryGetValue(out var value))
            {
                await action(value).ConfigureAwait(false);
            }

            return option;
        }
    }

    /// <param name="optionTask">The task returning an option to operate on.</param>
    extension<TValue>(Task<Option<TValue>> optionTask) where TValue : notnull
    {
        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> Tap(Action<TValue> action)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Tap(action);
        }

        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> TapAsync(Func<TValue, Task> action)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.TapAsync(action).ConfigureAwait(false);
        }
    }
}
