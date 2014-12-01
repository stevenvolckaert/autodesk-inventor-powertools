using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Collections.Generic.Dictionary&lt;TKey, TValue&gt; objects.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Returns a read-only wrapper for the specified dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to wrap in a read-only ReadOnlyDictionary&lt;TKey, TValue&gt; wrapper.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c>.</exception>
        public static ReadOnlyDictionaryCollection<TKey, TValue> AsReadOnly<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            return new ReadOnlyDictionaryCollection<TKey, TValue>(dictionary);
        }
    }
}
