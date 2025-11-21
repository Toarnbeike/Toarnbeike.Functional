namespace Toarnbeike.Results.Collections;

public static class TraverseExtensions
{
    extension<TValue>(IEnumerable<TValue> collection)
    {
        /// <summary>
        /// Applies a function to each element in the collection, producing an <see cref="Result{TValue}"/> for each element.
        /// If any operation results in a failure, the process short-circuits and returns that failure.
        /// Otherwise, returns a collection of all the successfully transformed values.
        /// </summary>
        /// <param name="bind">The function to apply to each element in the collection, producing an <see cref="Result{TValue}"/>.</param>
        /// <returns>An <see cref="Result{TValue}"/> with either the first failure or the collection of converted values.</returns>
        public Result<IEnumerable<TValue>> Traverse(Func<TValue, Result<TValue>> bind)
        {
            List<TValue> values = [];
            foreach (var value in collection)
            {
                var result = bind(value);
                if (!result.TryGetContents(out var converted, out var failure)) return failure;
                values.Add(converted);
            }

            return values;
        }
        
        /// <summary>
        /// Applies a function to each element in the collection, producing an <see cref="Result{TValue}"/> for each element.
        /// If any operation results in a failure, the process short-circuits and returns that failure.
        /// Otherwise, returns a collection of all the successfully transformed values.
        /// </summary>
        /// <param name="bindAsync">The function to apply to each element in the collection, producing an <see cref="Result{TValue}"/>.</param>
        /// <returns>An <see cref="Result{TValue}"/> with either the first failure or the collection of converted values.</returns>
        public async Task<Result<IEnumerable<TValue>>> TraverseAsync(Func<TValue, Task<Result<TValue>>> bindAsync)
        {
            List<TValue> values = [];
            foreach (var value in collection)
            {
                var result = await bindAsync(value);
                if (!result.TryGetContents(out var converted, out var failure)) return failure;
                values.Add(converted);
            }

            return values;
        }
    }
    
    extension<TValue>(IEnumerable<Task<TValue>> collectionTasks)
    {
        /// <summary>
        /// Applies a function to each element in the collection, producing an <see cref="Result{TValue}"/> for each element.
        /// If any operation results in a failure, the process short-circuits and returns that failure.
        /// Otherwise, returns a collection of all the successfully transformed values.
        /// </summary>
        /// <param name="bind">The function to apply to each element in the collection, producing an <see cref="Result{TValue}"/>.</param>
        /// <returns>An <see cref="Result{TValue}"/> with either the first failure or the collection of converted values.</returns>
        public async Task<Result<IEnumerable<TValue>>> Traverse(Func<TValue, Result<TValue>> bind)
        {
            ArgumentNullException.ThrowIfNull(collectionTasks);
            
            var collection = await Task.WhenAll(collectionTasks);
            return collection.Traverse(bind);
        }
        
        /// <summary>
        /// Applies a function to each element in the collection, producing an <see cref="Result{TValue}"/> for each element.
        /// If any operation results in a failure, the process short-circuits and returns that failure.
        /// Otherwise, returns a collection of all the successfully transformed values.
        /// </summary>
        /// <param name="bindAsync">The function to apply to each element in the collection, producing an <see cref="Result{TValue}"/>.</param>
        /// <returns>An <see cref="Result{TValue}"/> with either the first failure or the collection of converted values.</returns>
        public async Task<Result<IEnumerable<TValue>>> TraverseAsync(Func<TValue, Task<Result<TValue>>> bindAsync)
        {
            ArgumentNullException.ThrowIfNull(collectionTasks);
            
            var collection = await Task.WhenAll(collectionTasks);
            return await collection.TraverseAsync(bindAsync);
        }
    }
}