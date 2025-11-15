using Toarnbeike.Eithers.Extensions;

namespace Toarnbeike.Eithers.Linq;

public static class LinqExtensions
{
    extension<TLeft, TRight>(Either<TLeft, TRight> either)
    {
        /// <summary>
        /// Projects the right value of the specified <see cref="Either{TLeft, TRight}"/>
        /// into a new form using the provided mapping function.
        /// </summary>
        /// <typeparam name="TResult">The type of the value the mapping function will return.</typeparam>
        /// <param name="map">A function to apply to the right value of the <see cref="Either{TLeft, TRight}"/>.</param>
        /// <returns>
        /// A new <see cref="Either{TLeft, TResult}"/> containing the result of applying
        /// the mapping function to the right value if the original <see cref="Either{TLeft, TRight}"/>
        /// was in a right state. If the original <see cref="Either{TLeft, TRight}"/> was in a left state,
        /// the result will contain the same left value as the original.
        /// </returns>
        public Either<TLeft, TResult> Select<TResult>(Func<TRight, TResult> map)
        {
            return either.Map(map);
        }

        /// <summary>
        /// Projects the right value of the specified <see cref="Either{TLeft, TRight}"/> into a new form
        /// by first binding it into a new <see cref="Either{TLeft, TMiddle}"/> using the provided binding function,
        /// and then combining the original right value and the bound value into a final result using the projection function.
        /// </summary>
        /// <typeparam name="TMiddle">The type of the intermediate value returned by the binding function.</typeparam>
        /// <typeparam name="TResult">The type of the final result produced by the projection function.</typeparam>
        /// <param name="bind">
        /// A function that takes the right value of the <see cref="Either{TLeft, TRight}"/> and transforms it
        /// into a new <see cref="Either{TLeft, TMiddle}"/>.
        /// </param>
        /// <param name="project">
        /// A function that takes the original right value and the intermediate value produced by the binding function,
        /// and transforms them into the final result.
        /// </param>
        /// <returns>
        /// A new <see cref="Either{TLeft, TResult}"/> containing the final result if all transformations succeed,
        /// or containing the left value if any intermediate step results in a left state.
        /// </returns>
        public Either<TLeft, TResult> SelectMany<TMiddle, TResult>(
            Func<TRight, Either<TLeft, TMiddle>> bind,
            Func<TRight, TMiddle, TResult> project)
        {
            if (either.IsLeft(out var left, out var right))
                return (Either<TLeft, TResult>)left;

            var middle = bind(right);

            return middle.IsLeft(out left, out var right2) 
                ? (Either<TLeft, TResult>)left
                : project(right, right2);
        }
    }
}