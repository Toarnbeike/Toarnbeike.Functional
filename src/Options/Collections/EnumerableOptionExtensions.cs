namespace Toarnbeike.Options.Collections;

/// <summary>
/// Extension methods for working with <see cref="IEnumerable{T}"/> collections.
/// </summary>
public static class EnumerableOptionExtensions
{
    extension<TValue>(IEnumerable<TValue?> source) where TValue : struct
    {
        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <remarks> Uses <see cref="IEnumerable{T}.GetEnumerator"/> to avoid conflict between the first entity of 
        /// a IEnumerable of a struct return the default value, versus FirstOrDefault() returning default.</remarks>
        public Option<TValue> FirstOrNone()
        {
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is not null)
                {
                    return enumerator.Current.Value;
                }
            }

            return Option.None;
        }

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <remarks> Uses a foreach loop to avoid conflict between the first entity of first entity of 
        /// a IEnumerable of a struct return the default value, versus FirstOrDefault() returning default because. </remarks>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> FirstOrNone(Func<TValue?, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is not null && predicate(enumerator.Current))
                {
                    return enumerator.Current.Value;
                }
            }

            return Option.None;
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        public Option<TValue> LastOrNone()
        {
            ArgumentNullException.ThrowIfNull(source);

            for (int i = source.Count() - 1; i >= 0; i--)
            {
                var value = source.ElementAt(i);
                if (value is not null)
                {
                    return value.Value;
                }
            }

            return Option.None;
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> LastOrNone(Func<TValue?, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            for (int i = source.Count() - 1; i >= 0; i--)
            {
                var value = source.ElementAt(i);
                if (value is not null && predicate(value.Value))
                {
                    return value.Value;
                }
            }

            return Option.None;
        }

        /// <summary>
        /// Returns the single element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        public Option<TValue> SingleOrNone()
        {
            ArgumentNullException.ThrowIfNull(source);
            var found = Option<TValue>.None;
            foreach (var value in source.Where(value => value is not null))
            {
                if (found.IsSome)
                {
                    throw new InvalidOperationException("Sequence contains more than one matching element.");
                }
                found = value!.Value;
            }
            return found;
        }

        /// <summary>
        /// Returns the single element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> SingleOrNone(Func<TValue?, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
            var found = Option<TValue>.None;
            foreach (var value in source.Where(value => value is not null && predicate(value.Value)))
            {
                if (found.IsSome)
                {
                    throw new InvalidOperationException("Sequence contains more than one matching element.");
                }
                found = value!.Value;
            }

            return found;
        }
    }

    /// <param name="source">The <see cref="IEnumerable{T}" /> to return the first value of.</param>
    extension<TValue>(IEnumerable<TValue?> source) where TValue : notnull
    {
        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <remarks> Uses <see cref="IEnumerable{T}.GetEnumerator"/> to avoid conflict between the first entity of 
        /// a IEnumerable of a struct return the default value, versus FirstOrDefault() returning default.</remarks>
        public Option<TValue> FirstOrNone()
        {
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is not null)
                {
                    return enumerator.Current;
                }
            }

            return Option.None;
        }

        /// <summary>
        /// Returns the first element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <remarks> Uses a foreach loop to avoid conflict between the first entity of first entity of 
        /// a IEnumerable of a struct return the default value, versus FirstOrDefault() returning default because. </remarks>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> FirstOrNone(Func<TValue?, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is not null && predicate(enumerator.Current))
                {
                    return enumerator.Current;
                }
            }

            return Option.None;
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        public Option<TValue> LastOrNone()
        {
            ArgumentNullException.ThrowIfNull(source);

            for (int i = source.Count() - 1; i >= 0; i--)
            {
                var value = source.ElementAt(i);
                if (value is not null)
                {
                    return value;
                }
            }

            return Option.None;
        }

        /// <summary>
        /// Returns the last element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> LastOrNone(Func<TValue, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            for (int i = source.Count() - 1; i >= 0; i--)
            {
                var value = source.ElementAt(i);
                if (value is not null && predicate(value))
                {
                    return value;
                }
            }

            return Option.None;
        }

        /// <summary>
        /// Returns the single element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        public Option<TValue> SingleOrNone()
        {
            ArgumentNullException.ThrowIfNull(source);
            var found = Option<TValue>.None;
            foreach (var value in source)
            {
                if (value is not null)
                {
                    if (found.IsSome)
                    {
                        throw new InvalidOperationException("Sequence contains more than one matching element.");
                    }
                    found = value;
                }
            }
            return found;
        }

        /// <summary>
        /// Returns the single element of a sequence, or <c>Option.None</c> if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public Option<TValue> SingleOrNone(Func<TValue?, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
            var found = Option<TValue>.None;
            foreach (var value in source)
            {
                if (value is not null && predicate(value))
                {
                    if (found.IsSome)
                    {
                        throw new InvalidOperationException("Sequence contains more than one matching element.");
                    }
                    found = value;
                }
            }

            return found;
        }
    }
}