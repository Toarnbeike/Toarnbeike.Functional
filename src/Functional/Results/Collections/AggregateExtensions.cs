using Toarnbeike.Results.Extensions.Unsafe;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Collections;

public static class AggregateExtensions
{
    /// <summary>
    /// Converts a sequence of <see cref="Result"/> into a single <see cref="Result"/> using an aggregate strategy.
    /// </summary>
    /// <remarks>
    /// All failures in <paramref name="results"/> are collected into an <see cref="AggregateFailure"/>.
    /// If all results are successful, a single successful result is returned.
    /// </remarks>
    /// <returns>
    /// A successful <see cref="Result"/> if all results succeeded;
    /// otherwise, a failure result with an <see cref="AggregateFailure"/>.
    /// </returns>
    public static Result Aggregate(this IEnumerable<Result> results)
    {
        var failures = results.Where(r => r.IsFailure)
            .Select(r => r.GetFailureOrThrow()).ToList();

        return failures.Count > 0
            ? new AggregateFailure(failures)
            : Result.Success();
    }
    
    /// <summary>
    /// Converts a sequence of <see cref="Result{T}"/> into a single <see cref="Result{IEnumerable}"/> using an aggregate strategy.
    /// </summary>
    /// <remarks>
    /// All failures in <paramref name="results"/> are collected into an <see cref="AggregateFailure"/>.
    /// If all results are successful, a single successful result containing all values is returned.
    /// </remarks>
    /// <returns> A success <see cref="Result{IEnumerable}"/> if all results succeeded, otherwise, a failure with <see cref="AggregateFailure"/>. </returns>
    public static Result<IEnumerable<TValue>> Aggregate<TValue>(this IEnumerable<Result<TValue>> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var successfulResults = new List<TValue>();
        var failures = new List<Failure>();
        foreach (var result in results)
        {
            if (result.TryGetContents(out var value, out var failure))
            {
                successfulResults.Add(value);
            }
            else
            {
                failures.Add(failure);
            }
        }

        return failures.Count == 0
            ? Result.Success(successfulResults.AsEnumerable())
            : new AggregateFailure(failures);
    }
    
    /// <summary>
    /// Converts a sequence of <see cref="Result"/> into a single <see cref="Result"/> using an aggregate strategy.
    /// </summary>
    /// <remarks>
    /// All failures in <paramref name="resultTasks"/> are collected into an <see cref="AggregateFailure"/>.
    /// If all results are successful, a single successful result is returned.
    /// </remarks>
    /// <returns>
    /// A successful <see cref="Result"/> if all results succeeded;
    /// otherwise, a failure result with an <see cref="AggregateFailure"/>.
    /// </returns>
    public static async Task<Result> Aggregate(this IEnumerable<Task<Result>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks);
        return results.Aggregate();
    }

    /// <summary>
    /// Converts a sequence of <see cref="Result{T}"/> into a single <see cref="Result{IEnumerable}"/> using an aggregate strategy.
    /// </summary>
    /// <remarks>
    /// All failures in <paramref name="resultTasks"/> are collected into an <see cref="AggregateFailure"/>.
    /// If all results are successful, a single successful result containing all values is returned.
    /// </remarks>
    /// <returns>
    /// A successful <see cref="Result{IEnumerable}"/> if all results succeeded;
    /// otherwise, a failure with <see cref="AggregateFailure"/>.
    /// </returns>
    public static async Task<Result<IEnumerable<TValue>>> Aggregate<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks);
        return results.Aggregate();
    }
}