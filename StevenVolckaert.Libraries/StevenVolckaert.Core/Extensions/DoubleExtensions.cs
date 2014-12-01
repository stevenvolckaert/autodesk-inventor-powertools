using System;
using System.Globalization;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Double objects.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Returns a string that represents the System.Double value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.Double value, in kibibytes.</param>
        public static string KibibytesToString(this Double value)
        {
            return value.KilobytesToString(isBinary: true);
        }

        /// <summary>
        /// Returns a string that represents the System.Double value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.Double value, in kilobytes.</param>
        public static string KilobytesToString(this Double value)
        {
            return value.KilobytesToString(isBinary: false);
        }

        private static string KilobytesToString(this Double value, bool isBinary)
        {
            /* NOTE Hard drive sizes are represented using the SI decimal prefixes, as hard drives are commonly
             * marketed as such by hard drive manufacturers. The decimal prefixes use orders of magnitude of 10 ^ n.
             * In contrast, IEC binary prefixes (as used - but mistakenly labeled with SI decimal prefixes - by most
             * Windows operating systems, for instance), use orders of magnitude of 2 ^ n.
             * Decimal prefixes are labeled 'kilobyte' (KB), 'megabyte' (MB), 'gigabyte' (GB), etc., while binary
             * prefixes are labeled 'kibibyte' (KiB), 'mebibyte' (MiB), 'gibibyte' (GiB) etc.
             * See <http://en.wikipedia.org/wiki/Gigabyte> for more information.
             * Steven Volckaert. July 13, 2012.
             */

            var divider = isBinary ? 1024 : 1000;
            var suffix = isBinary ? "iB" : "B";

            if (value < divider)
                return string.Format(CultureInfo.InvariantCulture, "{0:0} {1}{2}", value, isBinary ? "K" : "k", suffix);

            return (value / divider).MegabytesToString(isBinary);
        }

        /// <summary>
        /// Returns a string that represents the System.Double value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.Double value, in mebibytes.</param>
        public static string MebibytesToString(this Double value)
        {
            return value.MegabytesToString(isBinary: true);
        }

        /// <summary>
        /// Returns a string that represents the System.Double value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.Double value, in megabytes.</param>
        public static string MegabytesToString(this Double value)
        {
            return value.MegabytesToString(isBinary: false);
        }

        private static string MegabytesToString(this Double value, bool isBinary)
        {
            var divider = isBinary ? 1024 : 1000;
            var suffix = isBinary ? "iB" : "B";

            if (value >= Math.Pow(divider, 2))
                return string.Format(CultureInfo.InvariantCulture, "{0:0.#} T{1}", value / Math.Pow(divider, 2), suffix);

            if (value >= divider)
                return string.Format(CultureInfo.InvariantCulture, "{0:0.#} G{1}", value / divider, suffix);

            return string.Format(CultureInfo.InvariantCulture, "{0:0.#} M{1}", value, suffix);
        }
    }
}
