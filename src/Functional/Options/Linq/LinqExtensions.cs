using Toarnbeike.Options.Extensions;

namespace Toarnbeike.Options.Linq;

public static class LinqExtensions
{
    extension<TValue>(Option<TValue> option)
    {
        /// <summary>
        /// Transforms the value of the specified <see cref="Option{TValue}"/> using a mapping function,
        /// if the option contains a value, and returns a new <see cref="Option{TResult}"/> containing the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the mapping function.</typeparam>
        /// <param name="map">
        /// A function that takes the value of the original <see cref="Option{TValue}"/> and transforms it into a new value of type <typeparamref name="TResult"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="Option{TResult}"/> containing the transformed value if the original option has a value,
        /// or an empty <see cref="Option{TResult}"/> if the original option is empty.
        /// </returns>
        public Option<TResult> Select<TResult>(Func<TValue, TResult> map)
        {
            return option.Map(map);
        }

        /// <summary>
        /// Projects the value of an <see cref="Option{TValue}"/> into another <see cref="Option{TResult}"/> using
        /// a binding function, and then maps the intermediate result to the final result using a projector function.
        /// </summary>
        /// <typeparam name="TIntermediate">The type of the intermediate value returned by the binding function.</typeparam>
        /// <typeparam name="TResult">The type of the value contained in the resulting <see cref="Option{TResult}"/>.</typeparam>
        /// <param name="binder">
        /// A function that takes the value of the original <see cref="Option{TValue}"/> and returns an <see cref="Option{TIntermediate}"/>.
        /// </param>
        /// <param name="projector">
        /// A function that takes the value of the original <see cref="Option{TValue}"/> and the intermediate value of type <typeparamref name="TIntermediate"/>,
        /// and returns the final result of type <typeparamref name="TResult"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="Option{TResult}"/> containing the final result if both the original option and the intermediate option contain values;
        /// otherwise, an empty <see cref="Option{TResult}"/>.
        /// </returns>
        public Option<TResult> SelectMany<TIntermediate, TResult>(
            Func<TValue, Option<TIntermediate>> binder,
            Func<TValue, TIntermediate, TResult> projector)
        {
            if (!option.TryGetValue(out var value))
                return Option.None;

            var intermediate = binder(value);
            if (!intermediate.TryGetValue(out var midValue))
                return Option.None;

            return (projector(value, midValue));
        }
    }
}