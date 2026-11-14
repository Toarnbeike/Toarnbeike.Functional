namespace Toarnbeike.Options.Collections;

/// <summary>
/// Extension methods for working with collections of <see cref="Option{TValue}"/>.
/// </summary>
public static class CollectionExtensions
{
    /// <param name="source">The <see cref="IEnumerable{T}" /> to return the values of.</param>
    extension<TValue>(IEnumerable<Option<TValue>> source) where TValue : notnull
    {
        /// <summary>
        /// Returns all elements of the sequence that have a value.
        /// </summary>
        public IEnumerable<TValue> Values() =>
            source.WhereInternal(_ => true);

        /// <summary>
        /// Returns all elements of the sequence that have a value.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public IEnumerable<TValue> WhereValues(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return source.WhereInternal(predicate);
        }

        /// <summary>
        /// Projects the values of the <see cref="Option{TValue}"/> elements in the source sequence  into a new form using
        /// the specified selector function.
        /// </summary>
        /// <typeparam name="TValue">The type of the value contained in the <see cref="Option{TValue}"/> elements.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by the selector function.</typeparam>
        /// <param name="selector">A transform function to apply to each value contained in the <see cref="Option{TValue}"/> elements.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the results of applying the selector function  to the values of the
        /// <see cref="Option{TValue}"/> elements in the source sequence.</returns>
        public IEnumerable<TResult> SelectValues<TResult>(Func<TValue, TResult> selector)
        {
            ArgumentNullException.ThrowIfNull(selector);

            return source.Values().Select(selector);
        }

        /// <summary>
        /// Returns the number of entries in the sequence that contain a value.
        /// </summary>
        public int CountValues() =>
            source.Count(option => option.IsSome);

        /// <summary>
        /// Returns the number of entries in the sequence that contain a value.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public int CountValues(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.Count(option => SomePredicate(option, predicate));
        }

        /// <summary>
        /// Returns true if there are any non-empty entries in the sequence.
        /// </summary>
        public bool AnyValues() =>
            source.Any(option => option.IsSome);

        /// <summary>
        /// Returns true if there are any non-empty entries in the sequence.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public bool AnyValues(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.Any(option => SomePredicate(option, predicate));
        }

        /// <summary>
        /// Determines whether all elements in the source sequence have a value.
        /// </summary>
        /// <remarks>This method evaluates whether every element in the source sequence satisfies the
        /// condition  that it has a value. If the sequence is empty, the method returns <see langword="true"/>.</remarks>
        /// <returns><see langword="true"/> if all elements in the source sequence have a value;  otherwise, <see langword="false"/>.</returns>
        public bool AllValues() =>
            source.All(option => option.IsSome);

        /// <summary>
        /// Determines whether all elements in the source sequence have a value that satisfy a condition.
        /// </summary>
        /// <remarks>This method evaluates whether every element in the source sequence satisfies the
        /// condition that it has a value and matches the predicate. If the sequence is empty, the method returns <see langword="true"/>.</remarks>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns><see langword="true"/> if all elements in the source sequence have a value;  otherwise, <see langword="false"/>.</returns>
        public bool AllValues(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.All(option => SomePredicate(option, predicate));
        }

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        public Option<TValue> FirstOrNone() =>
            source.FirstOrDefault(option => option.IsSome, Option.None);

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> FirstOrNone(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.FirstOrDefault(option => SomePredicate(option, predicate), Option.None);
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        public Option<TValue> LastOrNone() =>
            source.LastOrDefault(option => option.IsSome, Option.None);

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> LastOrNone(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.LastOrDefault(option => SomePredicate(option, predicate), Option.None);
        }

        /// <summary>
        /// Returns the single occurence of a value in the sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the sequence contains more than one element.</exception>
        public Option<TValue> SingleOrNone() =>
            source.SingleOrDefault(option => option.IsSome, Option.None);

        /// <summary>
        /// Returns the single occurence of a value in the sequence, or <c>Option.None</c> if the sequence contains no elements matching the predicate.
        /// </summary>
        public Option<TValue> SingleOrNone(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            return source.SingleOrDefault(option => SomePredicate(option, predicate), Option.None);
        }

        private IEnumerable<TValue> WhereInternal(Func<TValue, bool> predicate)
        {
            foreach (var option in source)
            {
                if (option is TValue some && predicate(some))
                {
                    yield return some;
                }
            }
        }
    }

    private static bool SomePredicate<TValue>(Option<TValue> option, Func<TValue, bool> predicate)
    where TValue : notnull => option is TValue some && predicate(some);
}