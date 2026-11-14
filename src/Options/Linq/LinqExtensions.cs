using Toarnbeike.Options.Extensions;

namespace Toarnbeike.Options.Linq;

/// <summary>
/// LINQ-style extension methods for Option{T}.
/// Enables usage in query expressions.
/// </summary>
public static class OptionLinqExtensions
{
    extension<TSource>(Option<TSource> source) where TSource : notnull
    {
        /// <summary>
        /// Maps the value of the option to a new value if present.
        /// </summary>
        public Option<TResult> Select<TResult>(Func<TSource, TResult> selector) 
            where TResult : notnull =>
            source.Map(selector);

        /// <summary>
        /// Overload for SelectMany to support projection in query expressions.
        /// </summary>
        public Option<TResult> SelectMany<TIntermediate, TResult>(Func<TSource, Option<TIntermediate>> bind, Func<TSource, TIntermediate, TResult> project)
            where TIntermediate : notnull
            where TResult : notnull =>
            source.Bind(s => bind(s).Map(i => project(s, i)));

        /// <summary>
        /// Filters the option based on a predicate.
        /// </summary>
        public Option<TSource> Where(Func<TSource, bool> predicate) =>
            source.Check(predicate);
    }
}