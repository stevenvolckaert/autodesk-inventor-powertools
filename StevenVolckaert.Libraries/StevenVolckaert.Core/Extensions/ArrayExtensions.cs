using System;
using System.Collections.ObjectModel;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Array objects.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Returns a read-only wrapper for the specified array.
        /// </summary>
        /// <typeparam name="T">The type of elements of the array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based array to wrap in a read-only ReadOnlyCollection&lt;T&gt; wrapper.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
        public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            return new ReadOnlyCollection<T>(array);
        }
    }
}
