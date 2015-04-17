using System;
using System.Collections.Generic;
using System.Linq;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Collections.Generic.IDictionary&lt;TKey, TValue&gt; objects.
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the end of the System.Collections.Generic.ICollection&lt;T&gt;.
        /// </summary>
        /// <typeparam name="TSource">The type of elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The target data collection.</param>
        /// <param name="sourceCollection">The collection whose elements should be added to the System.Collections.Generic.ICollection&lt;T&gt;.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static void AddRange<TSource>(this ICollection<TSource> source, IEnumerable<TSource> sourceCollection)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (sourceCollection != null)
                foreach (TSource value in sourceCollection)
                    source.Add(value);
        }

        /// <summary>
        /// Removes the first element of the sequence that satisfies a specified condition,
        /// or does nothing if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The System.Collection.Generic.ICollection&lt;T&gt; to remove the element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <c>null</c>.</exception>
        public static void RemoveFirst<TSource>(this ICollection<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            if (source.IsReadOnly)
                return;

            source.Remove(source.FirstOrDefault(predicate));
        }

        /// <summary>
        /// Replaces the content of a collection with the content of another collection.
        /// </summary>
        /// <typeparam name="TSource">The type of elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The target data collection.</param>
        /// <param name="sourceCollection">The collection whose elements should be added to the System.Collections.Generic.ICollection&lt;T&gt;.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static void ReplaceContentWith<TSource>(this ICollection<TSource> source, IEnumerable<TSource> sourceCollection)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            source.Clear();
            source.AddRange(sourceCollection);
        }
    }
}
