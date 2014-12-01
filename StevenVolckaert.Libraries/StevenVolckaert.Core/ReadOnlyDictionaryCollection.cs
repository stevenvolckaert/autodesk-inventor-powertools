using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StevenVolckaert
{
    public sealed class ReadOnlyDictionaryCollection<TKey, TValue> : ReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        public ReadOnlyDictionaryCollection(IDictionary<TKey, TValue> items)
            : base(items.ToList())
        {
        }

        public TValue this[TKey key]
        {
            get
            {
                var valueQuery = GetQuery(key);

                if (valueQuery.Count() == 0)
                    throw new KeyNotFoundException("The given key was not present in the dictionary.");

                return valueQuery.First().Value;
            }
        }

        public bool ContainsKey(TKey key)
        {
            return GetQuery(key).Count() > 0;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var returnValue = ContainsKey(key);
            value = returnValue ? this[key] : default(TValue);
            return returnValue;
        }

        private IEnumerable<KeyValuePair<TKey, TValue>> GetQuery(TKey key)
        {
            return base.Items.Where(x => x.Key.Equals(key));
        }
    }
}
