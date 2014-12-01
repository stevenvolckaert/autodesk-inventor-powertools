using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for objects that implement the System.Collections.Generic.IEnumerable interface.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Produces the difference of a System.Collection.Generic.IEnumerable&lt;T&gt; and a specified element
        /// by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A System.Collections.Generic.IEnumerable&lt;T&gt; whose elements do not
        /// contain <paramref name="element"/> will be returned.</param>
        /// <param name="element">An element that, if contained in <paramref name="source"/>,
        /// will be removed from the returned sequence.</param>
        /// <returns>A sequence that contains the set difference of <paramref name="source"/> and <paramref name="element"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            return source.Except(new List<TSource> { element });
        }

        /// <summary>
        /// Determines whether a System.Collection.Generic.IEnumerable&lt;T&gt; is a subset of a specified collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/> and <paramref name="other"/>.</typeparam>
        /// <param name="source">The System.Collection.Generic.IEnumerable&lt;T&gt; to check.</param>
        /// <param name="other">The collection to compare to the current System.Collection.Generic.IEnumerable&lt;T&gt; object.</param>
        /// <returns><c>true</c> if the <paramref name="source"/> object is a subset of <paramref name="other"/>; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="other"/> is <c>null</c>.</exception>
        public static bool IsSubsetOf<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other)
        {
            return !source.Except(other).Any();
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by comparing strings alphabetically in a human-readable fashion.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function that extracts a System.String from each element.</param>
        /// <returns>A System.Linq.IEnumerable&lt;TElement&gt; whose elements are sorted alphabetically in a human-readable fashion.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="stringSelector"/> is <c>null</c>.</exception>
        public static IEnumerable<TSource> OrderByHumanReadableString<TSource>(this IEnumerable<TSource> source, Func<TSource, string> stringSelector)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            using (var comparer = new HumanReadableStringComparer())
            {
                return source.OrderBy(stringSelector, comparer).ToList();
            }
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order by comparing strings alphabetically in a human-readable fashion.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function that extracts a System.String from each element.</param>
        /// <returns>A System.Linq.IEnumerable&lt;TElement&gt; whose elements are sorted alphabetically in a human-readable fashion.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="stringSelector"/> is <c>null</c>.</exception>
        public static IEnumerable<TSource> OrderByHumanReadableStringDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, string> stringSelector)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            using (var comparer = new HumanReadableStringComparer())
            {
                return source.OrderByDescending(stringSelector, comparer).ToList();
            }
        }

        /// <summary>
        /// Creates a System.Collections.ObjectModel.ObservableCollection&lt;T&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt;.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Collections.ObjectModel.ObservableCollection&lt;T&gt; from.</param>
        /// <returns>A System.Collections.ObjectModel.ObservableCollection&lt;T&gt; that contains elements from the input sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static ObservableCollection<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return new ObservableCollection<TSource>(source);
        }

        /// <summary>
        /// Creates a System.Collections.ObjectModel.ReadOnlyObservableCollection&lt;T&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt;.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Collections.ObjectModel.ObservableCollection&lt;T&gt; from.</param>
        /// <returns>A System.Collections.ObjectModel.ReadOnlyObservableCollection&lt;T&gt; that contains elements from the input sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static ReadOnlyObservableCollection<TSource> ToReadOnlyObservableCollection<TSource>(this IEnumerable<TSource> source)
        {
            return new ReadOnlyObservableCollection<TSource>(source.ToObservableCollection());
        }

        /// <summary>
        /// Converts a System.Collections.Generic.IEnumerable&lt;T&gt; to a string by concatenating
        /// the Object.ToString() value of every element, using the specified separator between each member.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to convert to a string.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <returns>A string that consists of the string representations of the elements contained in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static string ToString(this IEnumerable<object> source, string separator)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return string.Join(separator, source);
        }

        /// <summary>
        /// Produces the union of a System.Collection.Generic.IEnumerable&lt;T&gt; and a specified element
        /// by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">A System.Collections.Generic.IEnumerable&lt;T&gt; whose distinct elements
        /// form the first set for the union.</param>
        /// <param name="element">An element who forms the second set of the union.</param>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements of <paramref name="source"/>
        /// and <paramref name="element"/>, excluding duplicates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            return source.Union(new List<TSource> { element });
        }
    }
}
