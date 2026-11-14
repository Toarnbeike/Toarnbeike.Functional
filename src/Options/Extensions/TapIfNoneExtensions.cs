namespace Toarnbeike.Options.Extensions;

public static class TapIfNoneExtensions
{
    /// <param name="option">The option to operate on.</param>
    extension<TValue>(Option<TValue> option) where TValue : notnull
    {
        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public Option<TValue> TapIfNone(Action action)
        {
            ArgumentNullException.ThrowIfNull(action);
            if (!option.IsSome)
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
            ArgumentNullException.ThrowIfNull(action);
            if (!option.IsSome)
            {
                await action().ConfigureAwait(false);
            }

            return option;
        }
    }

    /// <param name="optionTask">The task returning an option to operate on.</param>
    extension<TValue>(Task<Option<TValue>> optionTask) where TValue : notnull
    {
        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> TapIfNone(Action action)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.TapIfNone(action);
        }

        /// <summary>
        /// Execute an action if the option has no value.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> TapIfNoneAsync(Func<Task> action)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.TapIfNoneAsync(action).ConfigureAwait(false);
        }
    }
}