namespace Toarnbeike.Options.Extensions;

/// <summary>
/// Provides extension methods for performing an action with the value of an <see cref="Option{TValue}"/>.
/// </summary>
public static class TapExtensions
{
    /// <param name="option">The option that the action should be performed on.</param>
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public Option<TValue> Tap(Action<TValue> action)
        {
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
            if (option.TryGetValue(out var value))
            {
                await action(value);
            }
            return option;
        }
    }

    /// <param name="optionTask">The task that will return an option on which the action should be applied.</param>
    extension<TValue>(Task<Option<TValue>> optionTask)
    {
        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> Tap(Action<TValue> action) =>
            Tap(await optionTask, action);

        /// <summary>
        /// Execute an action with the value of the option if a value is present.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public async Task<Option<TValue>> TapAsync(Func<TValue, Task> action) =>
            await TapAsync(await optionTask, action);
    }
}
