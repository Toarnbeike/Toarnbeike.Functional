namespace Toarnbeike.Functional.Options.Extensions;

public static class AsResultExtensions
{
    /// <summary>
    /// Converts an <see cref="Option{T}"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <remarks>The failure factory is invoked only when the option does not contain a value.</remarks>
    /// <typeparam name="T">The type of the optional value.</typeparam>
    /// <param name="option">The option to convert.</param>
    /// <param name="failureFactory">Creates the failure to use when the option has no value.</param>
    /// <returns>A successful result containing the option value when present; otherwise a failed result created by <paramref name="failureFactory"/>.</returns>
    public static Result<T> AsResult<T>(this Option<T> option, Func<Failure> failureFactory) where T : notnull => option switch
    {
        T value => Result<T>.Success(value),
        None => Result<T>.Failure(failureFactory())
    };

    /// <summary>
    /// Converts an asynchronous <see cref="Option{T}"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <remarks>The failure factory is invoked only when the option does not contain a value.</remarks>
    /// <typeparam name="T">The type of the optional value.</typeparam>
    /// <param name="optionTask">The task containing the option to convert.</param>
    /// <param name="failureFactory">Creates the failure to use when the option has no value.</param>
    /// <returns>A successful result containing the option value when present; otherwise a failed result created by <paramref name="failureFactory"/>.</returns>
    public static Task<Result<T>> AsResultAsync<T>(this Task<Option<T>> optionTask, Func<Failure> failureFactory) where T : notnull =>
        optionTask.ContinueWith(task => task.Result.AsResult(failureFactory));
}
