using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Defines a method for comparing strings in a human-readable fashion.
    /// </summary>
    /// <remarks>The inspiration of this class comes from http://zootfroot.blogspot.be/2009/09/natural-sort-compare-with-linq-orderby.html </remarks>
    public sealed class HumanReadableStringComparer : IComparer<string>, IDisposable
    {
        private bool _sortAscending;
        private Dictionary<string, string[]> _dictionary = new Dictionary<string, string[]>();

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanReadableStringComparer"/> class.
        /// </summary>
        public HumanReadableStringComparer()
            : this(sortAscending: true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanReadableStringComparer"/> class.
        /// </summary>
        /// <param name="sortAscending">A value which indicates whether to sort ascending or descending.</param>
        public HumanReadableStringComparer(bool sortAscending)
        {
            _sortAscending = sortAscending;
        }

        public void Dispose()
        {
            _dictionary.Clear();
            _dictionary = null;
        }

        int IComparer<string>.Compare(string x, string y)
        {
            if (x == null || y == null || x == y)
                return 0;

            string[] x1, y1;

            if (!_dictionary.TryGetValue(x, out x1))
            {
                x1 = Regex.Split(x.Replace(" ", ""), "([0-9]+)");
                _dictionary.Add(x, x1);
            }

            if (!_dictionary.TryGetValue(y, out y1))
            {
                y1 = Regex.Split(y.Replace(" ", ""), "([0-9]+)");
                _dictionary.Add(y, y1);
            }

            int returnValue;

            for (int i = 0; i < x1.Length && i < y1.Length; i++)
                if (x1[i] != y1[i])
                {
                    returnValue = PartCompare(x1[i], y1[i]);
                    return _sortAscending ? returnValue : -returnValue;
                }

            if (y1.Length > x1.Length)
                returnValue = 1;
            else if (x1.Length > y1.Length)
                returnValue = -1;
            else
                returnValue = 0;

            return _sortAscending ? returnValue : -returnValue;
        }

        private static int PartCompare(string strA, string strB)
        {
            int x, y;

            if (!int.TryParse(strA, out x))
                return String.Compare(strA, strB, StringComparison.CurrentCulture);

            if (!int.TryParse(strB, out y))
                return String.Compare(strA, strB, StringComparison.CurrentCulture);

            return x.CompareTo(y);
        }
    }
}
