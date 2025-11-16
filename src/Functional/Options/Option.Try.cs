namespace Toarnbeike.Options;

/// <summary>
/// Provides utility methods for working with potentially failing operations that return an <see cref="Option{TValue}"/>.
/// </summary>
public static partial class Option
{
    /// <summary>
    /// Tries to execute the specified function.
    /// </summary>
    /// <remarks>
    /// This method will swallow any exception. Intended use cases include e.g., parsing and reading configuration.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value returned by the function.</typeparam>
    /// <param name="func">The function to execute that might throw.</param>
    /// <param name="logException">Optional action to log the exception before it is swallowed.</param>
    /// <returns>An <see cref="Option{TValue}"/> with the result or <see cref="Option{TValue}.None"/> if an exception occurred.</returns>
    /// <example>
    /// Option&lt;int&gt; result = Option.Try(() => int.Parse("abc"));
    /// </example>
    public static Option<TValue> Try<TValue>(Func<TValue> func, Action<Exception>? logException = null) =>
        Try(func, _ => true, logException);

    /// <summary>
    /// Tries to execute the specified function.
    /// </summary>
    /// <remarks>
    /// This method will swallow any exception for which the <paramref name="exceptionFilter"/> returns true.
    /// Intended use cases include e.g., parsing and reading configuration.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value returned by the function.</typeparam>
    /// <param name="func">The function to execute that might throw.</param>
    /// <param name="exceptionFilter">Optional filter to determine if the exception should be swallowed or rethrown.</param>
    /// <param name="logException">Optional action to log the exception before it is swallowed.</param>
    /// <returns>An <see cref="Option{TValue}"/> with the result or <see cref="Option{TValue}.None"/> if an exception occurred and was filtered.</returns>
    /// <example>
    /// Option&lt;int&gt; result = Option.Try(() => int.Parse("abc"), ex => ex is FormatException);
    /// </example>
    public static Option<TValue> Try<TValue>(Func<TValue> func, Func<Exception, bool>? exceptionFilter, Action<Exception>? logException = null)
    {
        try
        {
            var result = func();
            return result is not null ? Some(result) : None;
        }
        catch (Exception ex) when (exceptionFilter?.Invoke(ex) ?? true)
        {
            logException?.Invoke(ex);
            return None;
        }
    }

    /// <summary>
    /// Tries to execute the specified task.
    /// </summary>
    /// <remarks>
    /// This method will swallow any exception. Intended use cases include e.g., parsing and reading configuration.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value returned by the task.</typeparam>
    /// <param name="task">The task to execute that might throw.</param>
    /// <param name="logException">Optional action to log the exception before it is swallowed.</param>
    /// <returns>An <see cref="Option{TValue}"/> with the result or <see cref="Option{TValue}.None"/> if an exception occurred.</returns>
    /// <example>
    /// Option&lt;int&gt; result = Option.Try(() => int.Parse("abc"));
    /// </example>
    public static async Task<Option<TValue>> TryAsync<TValue>(Func<Task<TValue>> task, Action<Exception>? logException = null) =>
        await TryAsync(task, _ => true, logException);

    /// <summary>
    /// Tries to execute the specified task.
    /// </summary>
    /// <remarks>
    /// This method will swallow any exception for which the <paramref name="exceptionFilter"/> returns true.
    /// Intended use cases include e.g., parsing and reading configuration.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value returned by the task.</typeparam>
    /// <param name="task">The task to execute that might throw.</param>
    /// <param name="exceptionFilter">Optional filter to determine if the exception should be swallowed or rethrown.</param>
    /// <param name="logException">Optional action to log the exception before it is swallowed.</param>
    /// <returns>An <see cref="Option{TValue}"/> with the result or <see cref="Option{TValue}.None"/> if an exception occurred and was filtered.</returns>
    /// <example>
    /// Option&lt;int&gt; result = Option.TryAsync(async () => await int.ParseAsync("abc"), ex => ex is FormatException);
    /// </example>
    public static async Task<Option<TValue>> TryAsync<TValue>(Func<Task<TValue>> task, Func<Exception, bool> exceptionFilter, Action<Exception>? logException = null)
    {
        try
        {
            var result = await task().ConfigureAwait(false);
            return result is not null ? Some(result) : None;
        }
        catch (Exception ex) when (exceptionFilter.Invoke(ex))
        {
            logException?.Invoke(ex);
            return None;
        }
    }
}