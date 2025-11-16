namespace Toarnbeike.Options.Collection;

/// <summary>
/// Extension methods for working with collections of <see cref="Option{TValue}"/>.
/// </summary>
public static class CollectionExtensions
{
    /// <param name="source">The <see cref="IEnumerable{T}" /> to return the values of.</param>
    /// <typeparam name="TValue">The type of the value contained in the <see cref="Option{TValue}"/> elements.</typeparam>
    extension<TValue>(IEnumerable<Option<TValue>> source)
    {
        /// <summary>
        /// Returns all elements of the sequence that have a value.
        /// </summary>
        public IEnumerable<TValue> Sequence() =>
            source.Sequence(_ => true);

        /// <summary>
        /// Returns all elements of the sequence that have a value.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public IEnumerable<TValue> Sequence(Func<TValue, bool> predicate)
        {
            foreach (var option in source)
            {
                if (option.TryGetValue(out var value) && predicate(value))
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Projects the values of the <see cref="Option{TValue}"/> elements in the source sequence  into a new form using
        /// the specified selector function.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the selector function.</typeparam>
        /// <param name="selector">A transform function to apply to each value contained in the <see cref="Option{TValue}"/> elements.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the results of applying the selector function  to the values of the
        /// <see cref="Option{TValue}"/> elements in the source sequence.</returns>
        public IEnumerable<TResult> Sequence<TResult>(Func<TValue, TResult> selector) =>
            source.Sequence().Select(selector);

        /// <summary>
        /// Returns the number of entries in the sequence that contain a value.
        /// </summary>
        public int CountValues() =>
            source.Count(option => option.HasValue);

        /// <summary>
        /// Returns the number of entries in the sequence that contain a value.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public int CountValues(Func<TValue, bool> predicate) =>
            source.Count(option => option.TryGetValue(out var value) && predicate(value));

        /// <summary>
        /// Returns true if there are any non-empty entries in the sequence.
        /// </summary>
        public bool AnyValues() =>
            source.Any(option => option.HasValue);

        /// <summary>
        /// Returns true if there are any non-empty entries in the sequence.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public bool AnyValues(Func<TValue, bool> predicate) =>
            source.Any(option => option.TryGetValue(out var value) && predicate(value));

        /// <summary>
        /// Determines whether all elements in the source sequence have a value.
        /// </summary>
        /// <remarks>This method evaluates whether every element in the source sequence satisfies the
        /// condition  that it has a value. If the sequence is empty, the method returns <see langword="true"/>.</remarks>
        /// <returns><see langword="true"/> if all elements in the source sequence have a value;  otherwise, <see langword="false"/>.</returns>
        public bool AllValues() =>
            source.All(option => option.HasValue);

        /// <summary>
        /// Determines whether all elements in the source sequence have a value that satisfies a provided condition.
        /// </summary>
        /// <remarks>This method evaluates whether every element in the source sequence satisfies the
        /// condition that it has a value and matches the predicate. If the sequence is empty, the method returns <see langword="true"/>.</remarks>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns><see langword="true"/> if all elements in the source sequence have a value;  otherwise, <see langword="false"/>.</returns>
        public bool AllValues(Func<TValue, bool> predicate) =>
            source.All(option => option.TryGetValue(out var value) && predicate(value));

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        public Option<TValue> FirstOrNone() =>
            source.Where(option => option.HasValue).FirstOrDefault(Option.None);

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> FirstOrNone(Func<TValue, bool> predicate) =>
            source.Where(option => option.TryGetValue(out var value) && predicate(value)).FirstOrDefault(Option.None);

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        public Option<TValue> LastOrNone() =>
            source.Where(option => option.HasValue).LastOrDefault(Option.None);

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> LastOrNone(Func<TValue, bool> predicate) =>
            source.Where(option => option.TryGetValue(out var value) && predicate(value)).LastOrDefault(Option.None);
    }
    
    extension<TValue>(IEnumerable<TValue> source)
    {
        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <remarks> Uses <see cref="IEnumerable{T}.GetEnumerator"/> to avoid conflict between the first entity of 
        /// a IEnumerable of a struct return the default value, versus FirstOrDefault() returning default because.</remarks>
        public Option<TValue> FirstOrNone()
        {
            using var enumerator = source.GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current : Option<TValue>.None();
        }

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <remarks> Uses a foreach loop to avoid conflict between the first entity of first entity of 
        /// a IEnumerable of a struct return the default value, versus FirstOrDefault() returning default because. </remarks>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> FirstOrNone(Func<TValue, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return Option.None;
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        public Option<TValue> LastOrNone()
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return Option<TValue>.None();
            }

            var last = enumerator.Current;
            while (enumerator.MoveNext())
            {
                last = enumerator.Current;
            }

            return Option<TValue>.Some(last);
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> LastOrNone(Func<TValue, bool> predicate)
        {
            var found = false;
            TValue lastMatch = default!;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    lastMatch = item;
                    found = true;
                }
            }

            return found ? lastMatch : Option.None;
        }
    }
}