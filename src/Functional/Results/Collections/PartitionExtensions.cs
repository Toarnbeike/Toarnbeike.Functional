namespace Toarnbeike.Results.Collections;

public static class PartitionExtensions
{
    public static IEnumerable<Failure> Failures(this IEnumerable<Result> results)
    {
        foreach (var result in results)
        {
            if (result.TryGetFailure(out var failure)) yield return failure;
        }
    }
    
    /// <param name="results">The collection of <see cref="Result{TValue}"/> instances to evaluate.</param>
    extension<TValue>(IEnumerable<Result<TValue>> results)
    {
        /// <summary>
        /// Collect all the success values in a new collection of <see cref="Failure"/> instances.
        /// </summary>
        public IEnumerable<TValue> SuccessValues()
        {
            foreach (var result in results)
            {
                if (result.TryGetValue(out var value)) yield return value;
            }
        }
        
        /// <summary>
        /// Collect all failures in a new collection of <see cref="Failure"/> instances.
        /// </summary>
        public IEnumerable<Failure> Failures()
        {
            foreach (var result in results)
            {
                if (result.TryGetFailure(out var failure)) yield return failure;
            }
        }
        
        /// <summary>
        /// Splits a collection of <see cref="Result{T}"/> into successful values and failures.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// <list type="bullet">
        ///   <item><c>successes</c>: The values from all successful results.</item>
        ///   <item><c>failures</c>: The failure objects from all failed results.</item>
        /// </list>
        /// </returns>
        public (IEnumerable<TValue> Successes, IEnumerable<Failure> Failures) Partition()
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

            return (successfulResults.AsEnumerable(), failures.AsEnumerable());
        }
    }
    
    /// <summary>
    /// Collect all failures in a new collection of <see cref="Failure"/> instances.
    /// </summary>
    public static async Task<IEnumerable<Failure>> Failures(this IEnumerable<Task<Result>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks);
        return Failures(results);
    }

    /// <param name="resultTasks">The collection of <see cref="Result{TValue}"/> Tasks to evaluate.</param>
    extension<TValue>(IEnumerable<Task<Result<TValue>>> resultTasks)
    {
        /// <summary>
        /// Collect all failures in a new collection of <see cref="Failure"/> instances.
        /// </summary>
        public async Task<IEnumerable<Failure>> Failures()
        {
            ArgumentNullException.ThrowIfNull(resultTasks);

            var results = await Task.WhenAll(resultTasks);
            return results.Failures();
        }

        /// <summary>
        /// Collect all the success values in a new collection of <see cref="Failure"/> instances.
        /// </summary>
        public async Task<IEnumerable<TValue>> SuccessValues()
        {
            ArgumentNullException.ThrowIfNull(resultTasks);

            var results = await Task.WhenAll(resultTasks);
            return results.SuccessValues();
        }


        /// <summary>
        /// Splits a collection of <see cref="Result{T}"/> into successful values and failures.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// <list type="bullet">
        ///   <item><c>successes</c>: The values from all successful results.</item>
        ///   <item><c>failures</c>: The failure objects from all failed results.</item>
        /// </list>
        /// </returns>
        public async Task<(IEnumerable<TValue> Successes, IEnumerable<Failure> Failures)> Partition()
        {
            ArgumentNullException.ThrowIfNull(resultTasks);

            var results = await Task.WhenAll(resultTasks);
            return results.Partition();
        }
    }
}