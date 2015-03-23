using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Collections.Generic.IList&lt;T&gt; objects.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Returns a read-only wrapper for the specified list.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to wrap</param>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is <c>null</c>.</exception>
        public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            return new ReadOnlyCollection<T>(list);
        }
    }
}
