namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Provides extension methods for performing an action if an <see cref="Option{TValue}"/> has no value.
/// </summary>
public static class TapIfNoneExtensions
{
    /// <param name="option">This option that the action should be performed on.</param>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public Option<TValue> TapIfNone(Action action)
        {
            if (!option.HasValue)
            {
                action();
            }
            return option;
        }

        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> TapIfNoneAsync(Func<Task> action)
        {
            if (!option.HasValue)
            {
                await action();
            }
            return option;
        }
    }

    /// <param name="optionTask">This task that will return an option on which the action should be applied.</param>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> TapIfNone(Action action) =>
            TapIfNone(await optionTask, action);

        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> TapIfNoneAsync(Func<Task> action) =>
            await TapIfNoneAsync(await optionTask, action);
    }
}
