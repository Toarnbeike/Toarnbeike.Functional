namespace Toarnbeike.Eithers.Collections;

public static class CollectionExtensions
{
    extension<TValue>(IEnumerable<TValue> collection)
    {
        /// <summary>
        /// Applies a function to each element in the collection, producing an <see cref="Either{TLeft, TRight}"/> for each element.
        /// If any operation results in a left value, the process short-circuits and returns that left value.
        /// Otherwise, returns a right value containing a collection of all successfully transformed results.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value, typically representing an error.</typeparam>
        /// <typeparam name="TRight">The type of the right value, typically representing a successful result.</typeparam>
        /// <param name="bind">The function to apply to each element in the collection, producing an <see cref="Either{TLeft, TRight}"/>.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/> where the left represents an error occurred during processing and the right contains a collection of all successful results.</returns>
        public Either<TLeft, IEnumerable<TRight>> Traverse<TLeft, TRight>(Func<TValue, Either<TLeft, TRight>> bind)
        {
            ArgumentNullException.ThrowIfNull(collection);

            List<TRight> rights = [];
            foreach (var value in collection)
            {
                var either = bind(value);
                if (either.IsLeft(out var left, out var right)) return Either<TLeft, IEnumerable<TRight>>.Left(left);
                rights.Add(right);
            }
            return Either<TLeft, IEnumerable<TRight>>.Right(rights);
        }
    }
    
    extension<TLeft, TRight>(IEnumerable<Either<TLeft, TRight>> collection)
    {
        // ReSharper disable InvalidXmlDocComment -- Justification: XML documentation of nested generics are disliked.
        /// <summary>
        /// Converts a collection of <see cref="Either{TLeft, TRight}"/> values into a single
        /// <see cref="Either{TLeft, IEnumerable{TRight}}"/>. If any element in the collection
        /// represents a left value, the result is a left containing the first left value encountered.
        /// Otherwise, the result is a right containing a collection of all right values.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">The type of the right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <returns>
        /// A <see cref="Either{TLeft, IEnumerable{TRight}}"/> containing either the first left value
        /// encountered in the input collection or a collection of all right values if none of the
        /// inputs are left values.
        /// </returns>
        // ReSharper restore InvalidXmlDocComment
        public Either<TLeft, IEnumerable<TRight>> Sequence() => collection.Traverse(static x => x);

        /// <summary>
        /// Executes the specified action on the right values of each <see cref="Either{TLeft, TRight}"/>
        /// in the collection. If an element in the collection is a right, the action is invoked with
        /// the right value as its argument.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">The type of the right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="tap">The action to be invoked on the right values of the collection elements.</param>
        public void TapAll(Action<TRight> tap)
        {
            ArgumentNullException.ThrowIfNull(collection);
            
            foreach (var either in collection)
            {
                if (either.IsRight(out var right)) tap(right);
            }
        }

        /// <summary>
        /// Determines whether any element in the collection represents a left value.
        /// </summary>
        /// <returns> <c>true</c> if any element in the collection is a left value; otherwise, <c>false</c>. </returns>
        public bool AnyLeft() => collection.Any(either => either.IsLeft(out _));

        /// <summary>
        /// Determines whether any element in the collection represents a right value.
        /// </summary>
        /// <returns> <c>true</c> if any element in the collection is a right value; otherwise, <c>false</c>. </returns>
        public bool AnyRight() => collection.Any(either => either.IsRight(out _));

        /// <summary>
        /// Counts the number of elements in the collection that represent left values.
        /// </summary>
        public int CountLeft() => collection.Count(either => either.IsLeft(out _));
        
        /// <summary>
        /// Counts the number of elements in the collection that represent right values.
        /// </summary>
        public int CountRight() => collection.Count(either => either.IsRight(out _));

        /// <summary>
        /// Extracts all left values from a collection of <see cref="Either{TLeft, TRight}"/> values.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">The type of the right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <returns> An <see cref="IEnumerable{T}"/> that contains all left values in the collection. </returns>
        public IEnumerable<TLeft> Lefts()
        {
            foreach (var either in collection)
            {
                if (either.IsLeft(out var left)) yield return left;
            }
        }

        /// <summary>
        /// Retrieves all right values from a collection of <see cref="Either{TLeft, TRight}"/> values.
        /// Only elements that represent a right value are included in the output sequence.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">The type of the right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <returns> An <see cref="IEnumerable{TRight}"/> containing all right values in the collection. </returns>
        public IEnumerable<TRight> Rights()
        {
            foreach (var either in collection)
            {
                if (either.IsRight(out var right)) yield return right;
            }
        }

        /// <summary>
        /// Splits a collection of <see cref="Either{TLeft, TRight}"/> instances into two separate collections,
        /// one containing all left values and the other containing all right values.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">The type of the right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <returns>
        /// A tuple where the first element is an <see cref="IEnumerable{TLeft}"/> containing all left values and
        /// the second element is an <see cref="IEnumerable{TRight}"/> containing all right values.
        /// </returns>
        public (IEnumerable<TLeft> lefts, IEnumerable<TRight> rights) Partition()
        {
            var lefts = new List<TLeft>();
            var rights = new List<TRight>();
            
            foreach (var either in collection)
            {
                if (either.IsLeft(out var left, out var right)) lefts.Add(left);
                else rights.Add(right);
            }
            
            return (lefts, rights);
        }
    }
}