namespace Toarnbeike.Results.Extensions;

public static class AsOptionExtensions
{
    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="Option{T}"/>.
    /// This is a lossy conversion, as the <see cref="Option{T}"/> type does not carry error information. 
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <returns>An <see cref="Option{T}"/> containing the value when available; otherwise, <see cref="Option.None"/>.</returns>
    public static Option<T> AsOption<T>(this Result<T> result) =>
        result.TryGetValue(out var value) ? value : Option.None;

    /// <summary>
    /// Converts a <see cref="Task{Result{T}}"/> to a <see cref="Task{Option{T}}"/>.
    /// This is a lossy conversion, as the <see cref="Option{T}"/> type does not carry error information. 
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="resultTask">The task containing the result to convert.</param>
    /// <returns>A task containing an <see cref="Option{T}"/> with the value when available; otherwise, <see cref="Option.None"/>.</returns>
    public static Task<Option<T>> AsOptionAsync<T>(this Task<Result<T>> resultTask) =>
        resultTask.ContinueWith(task => task.Result.AsOption());
}
