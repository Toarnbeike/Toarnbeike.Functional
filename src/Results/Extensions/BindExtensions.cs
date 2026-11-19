namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Bind: Provides extension methods for binding a <see cref="Result"/> and <see cref="Result{TIn}" />
/// to a new <see cref="Result{TOut}"/> using a function.
/// </summary>
public static class BindExtensions
{
    /// <param name="result">The original result to project from.</param>
    extension(Result result)
    {
        /// <summary>
        /// Projects a successful <see cref="Result"/> into a new <see cref="Result"/> using the specified function.
        /// </summary>
        /// <param name="bindFunc">A function to apply if the original result is successful. Should return a new <see cref="Result"/>.</param>
        /// <returns>
        /// If the original result is a failure, that failure is returned.
        /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
        /// </returns>
        public Result Bind(Func<Result> bindFunc) 
        {
            ArgumentNullException.ThrowIfNull(bindFunc);

            return result switch
            {
                Success => bindFunc(),
                Failure failure => failure
            };
        }

        /// <summary>
        /// Projects a successful <see cref="Result"/> into a new <see cref="Result{TOut}"/> using the specified function.
        /// </summary>
        /// <typeparam name="TOut">The result type of the projection.</typeparam>
        /// <param name="bindFunc">
        /// A function to apply if the original result is successful.
        /// Should return a new <see cref="Result{TOut}"/>.
        /// </param>
        /// <returns>
        /// If the original result is a failure, that failure is returned.
        /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
        /// </returns>
        public Result<TOut> Bind<TOut>(Func<Result<TOut>> bindFunc) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bindFunc);

            return result switch
            {
                Success => bindFunc(),
                Failure failure => failure
            };
        }

        /// <summary>
        /// Asynchronously projects a successful <see cref="Result"/> into a new <see cref="Result"/> using the specified asynchronous function.
        /// </summary>
        /// <param name="bindTaskFunc">An asynchronous function to apply if the original result is successful. Should return a new <see cref="Result"/>.</param>
        /// <returns>
        /// A task that resolves to:
        /// - The original failure if <paramref name="result"/> is a failure,
        /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
        /// </returns>
        public async Task<Result> BindAsync(Func<Task<Result>> bindTaskFunc)
        {
            ArgumentNullException.ThrowIfNull(bindTaskFunc);

            return result switch
            {
                Success => await bindTaskFunc().ConfigureAwait(false),
                Failure failure => failure
            };
        }

        /// <summary>
        /// Asynchronously projects a successful <see cref="Result"/> into a new <see cref="Result{TOut}"/>
        /// using the specified asynchronous function.
        /// </summary>
        /// <typeparam name="TOut">The result type of the projection.</typeparam>
        /// <param name="bindTaskFunc">
        /// An asynchronous function to apply if the original result is successful.
        /// Should return a new <see cref="Result{TOut}"/>.
        /// </param>
        /// <returns>
        /// A task that resolves to:
        /// - The original failure if <paramref name="result"/> is a failure,
        /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
        /// </returns>
        public async Task<Result<TOut>> BindAsync<TOut>(Func<Task<Result<TOut>>> bindTaskFunc) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bindTaskFunc);

            return result switch
            {
                Success => await bindTaskFunc().ConfigureAwait(false),
                Failure failure => failure
            };
        }
    }

    /// <param name="resultTask">The original result task to project from.</param>
    extension(Task<Result> resultTask)
    {
        /// <summary>
        /// Projects a successful <see cref="Result"/> into a new <see cref="Result"/> using the specified function.
        /// </summary>
        /// <param name="bindFunc">A function to apply if the original result is successful. Should return a new <see cref="Result"/>.</param>
        /// <returns>
        /// If the original result is a failure, that failure is returned.
        /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
        /// </returns>
        public async Task<Result> Bind(Func<Result> bindFunc)
        {
            ArgumentNullException.ThrowIfNull(bindFunc);

            var result = await resultTask.ConfigureAwait(false);
            return result.Bind(bindFunc);
        }

        /// <summary>
        /// Projects a successful <see cref="Result"/> into a new <see cref="Result{TOut}"/> using the specified function.
        /// </summary>
        /// <typeparam name="TOut">The result type of the projection.</typeparam>
        /// <param name="bindFunc">
        /// A function to apply if the original result is successful.
        /// Should return a new <see cref="Result{TOut}"/>.
        /// </param>
        /// <returns>
        /// If the original result is a failure, that failure is returned.
        /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
        /// </returns>
        public async Task<Result<TOut>> Bind<TOut>(Func<Result<TOut>> bindFunc) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bindFunc);

            var result = await resultTask.ConfigureAwait(false);
            return result.Bind(bindFunc);
        }

        /// <summary>
        /// Asynchronously projects a successful <see cref="Result"/> into a new <see cref="Result"/> using the specified asynchronous function.
        /// </summary>
        /// <param name="bindTaskFunc">An asynchronous function to apply if the original result is successful. Should return a new <see cref="Result"/>.</param>
        /// <returns>
        /// A task that resolves to:
        /// - The original failure if <paramref name="result"/> is a failure,
        /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
        /// </returns>
        public async Task<Result> BindAsync(Func<Task<Result>> bindTaskFunc)
        {
            ArgumentNullException.ThrowIfNull(bindTaskFunc);
            var result = await resultTask.ConfigureAwait(false);
            return await result.BindAsync(bindTaskFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously projects a successful <see cref="Result"/> into a new <see cref="Result{TOut}"/>
        /// using the specified asynchronous function.
        /// </summary>
        /// <typeparam name="TOut">The result type of the projection.</typeparam>
        /// <param name="bindTaskFunc">
        /// An asynchronous function to apply if the original result is successful.
        /// Should return a new <see cref="Result{TOut}"/>.
        /// </param>
        /// <returns>
        /// A task that resolves to:
        /// - The original failure if <paramref name="result"/> is a failure,
        /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
        /// </returns>
        public async Task<Result<TOut>> BindAsync<TOut>(Func<Task<Result<TOut>>> bindTaskFunc) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bindTaskFunc);

            var result = await resultTask.ConfigureAwait(false);
            return await result.BindAsync(bindTaskFunc).ConfigureAwait(false);
        }
    }

    /// <param name="result">The original result to project from.</param>
    /// <typeparam name="TIn">The type of the original result value.</typeparam>
    extension<TIn>(Result<TIn> result) where TIn : notnull
    {
        /// <summary>
        /// Projects the value of a successful <see cref="Result{TIn}"/> into a new <see cref="Result{TOut}"/>
        /// using the specified bind function.
        /// </summary>
        /// <typeparam name="TOut">The result type of the projection.</typeparam>
        /// <param name="bindFunc">
        /// A function that takes the original value and returns a new <see cref="Result{TOut}"/>.
        /// </param>
        /// <returns>
        /// If <paramref name="result"/> is a failure, that failure is returned.
        /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
        /// </returns>
        public Result<TOut> Bind<TOut>(Func<TIn, Result<TOut>> bindFunc) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bindFunc);

            return result switch
            {
                TIn value => bindFunc(value),
                Failure failure => failure,
            };
        }

        /// <summary>
        /// Asynchronously projects the value of a successful <see cref="Result{TIn}"/> into a new <see cref="Result{TOut}"/>
        /// using the specified asynchronous bind function.
        /// </summary>
        /// <typeparam name="TOut">The result type of the projection.</typeparam>
        /// <param name="bindTaskFunc">
        /// An asynchronous function that takes the original value and returns a new <see cref="Result{TOut}"/>.
        /// </param>
        /// <returns>
        /// A task that resolves to:
        /// - The original failure if <paramref name="result"/> is a failure,
        /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
        /// </returns>
        public async Task<Result<TOut>> BindAsync<TOut>(Func<TIn, Task<Result<TOut>>> bindTaskFunc) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bindTaskFunc);

            return result switch
            {
                TIn value => await bindTaskFunc(value).ConfigureAwait(false),
                Failure failure => failure,
            };
        }
    }

    /// <param name="resultTask">The original result task to project from.</param>
    /// <typeparam name="TIn">The type of the original result value.</typeparam>
    extension<TIn>(Task<Result<TIn>> resultTask) where TIn : notnull
    {
        /// <summary>
        /// Projects the value of a successful <see cref="Result{TIn}"/> into a new <see cref="Result{TOut}"/>
        /// using the specified bind function.
        /// </summary>
        /// <typeparam name="TOut">The result type of the projection.</typeparam>
        /// <param name="bindFunc">
        /// A function that takes the original value and returns a new <see cref="Result{TOut}"/>.
        /// </param>
        /// <returns>
        /// If <paramref name="result"/> is a failure, that failure is returned.
        /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
        /// </returns>
        public async Task<Result<TOut>> Bind<TOut>(Func<TIn, Result<TOut>> bindFunc) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bindFunc);

            var result = await resultTask.ConfigureAwait(false);
            return result.Bind(bindFunc);
        }

        /// <summary>
        /// Asynchronously projects the value of a successful <see cref="Result{TIn}"/> into a new <see cref="Result{TOut}"/>
        /// using the specified asynchronous bind function.
        /// </summary>
        /// <typeparam name="TOut">The result type of the projection.</typeparam>
        /// <param name="bindTaskFunc">
        /// An asynchronous function that takes the original value and returns a new <see cref="Result{TOut}"/>.
        /// </param>
        /// <returns>
        /// A task that resolves to:
        /// - The original failure if <paramref name="result"/> is a failure,
        /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
        /// </returns>
        public async Task<Result<TOut>> BindAsync<TOut>(Func<TIn, Task<Result<TOut>>> bindTaskFunc) where TOut : notnull
        {
            ArgumentNullException.ThrowIfNull(bindTaskFunc);

            var result = await resultTask.ConfigureAwait(false);
            return await result.BindAsync(bindTaskFunc).ConfigureAwait(false);
        }
    }
}
