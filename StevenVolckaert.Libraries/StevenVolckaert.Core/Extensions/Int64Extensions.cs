using System;
using System.Globalization;
using System.Linq;

namespace StevenVolckaert
{
    /// <summary>
    /// Specifies a prefix that is used in combination with a unit of information (e.g. byte).
    /// </summary>
    public enum UnitOfInformationPrefix
    {
        None = 0,
        Decimal = 1000,
        Binary = 1024
    }

    /// <summary>
    /// Provides extension methods for System.Int64 and System.UInt64 objects.
    /// </summary>
    [CLSCompliant(false)]
    public static class Int64Extensions
    {
        private static readonly String[] _binaryUnitSymbols = new String[] { "B", "KiB", "MiB", "GiB", "TiB", "PiB" };
        private static readonly String[] _decimalUnitSymbols = new String[] { "B", "kB", "MB", "GB", "TB", "PB" };

        /// <summary>
        /// Returns a string that represents a number of bytes in a human-readable format.
        /// </summary>
        /// <param name="value">The System.UInt64 value, in bytes.</param>
        public static string BytesToString(this UInt64 value, UnitOfInformationPrefix prefix)
        {
            var divider = (UInt64)prefix;
            String[] unitSymbols;

            switch (prefix)
            {
                case UnitOfInformationPrefix.Binary:
                    unitSymbols = _binaryUnitSymbols;
                    break;
                case UnitOfInformationPrefix.Decimal:
                    unitSymbols = _decimalUnitSymbols;
                    break;
                case UnitOfInformationPrefix.None:
                default:
                    return value + " B";
            }

            Double divideValue = 1;
            UInt64 compareValue = divider;

            for (var i = 0; i < unitSymbols.Length; i++)
            {
                if (value < compareValue)
                    return String.Format(CultureInfo.InvariantCulture, "{0:0.#} {1}", value / divideValue, unitSymbols[i]);

                compareValue = compareValue * divider;
                divideValue = divideValue * divider;
            }

            return String.Format(CultureInfo.InvariantCulture, "{0:0.#} {1}", value / divideValue, unitSymbols.Last());
        }

        /// <summary>
        /// Returns a string that represents a number of kibiytes in a human-readable format.
        /// </summary>
        /// <param name="value">The System.UInt64 value, in kibibytes.</param>
        public static string KibibytesToString(this UInt64 value)
        {
            return BytesToString(value * 1024, UnitOfInformationPrefix.Binary);
        }

        /// <summary>
        /// Returns a string that represents a number of kilobytes in a human-readable format.
        /// </summary>
        /// <param name="value">The System.UInt64 value, in kilobytes.</param>
        public static string KilobytesToString(this UInt64 value)
        {
            return BytesToString(value * 1000, UnitOfInformationPrefix.Decimal);
        }

        /// <summary>
        /// Returns a string that represents a number of mebibytes in a human-readable format.
        /// </summary>
        /// <param name="value">The System.UInt64 value, in mebibytes.</param>
        public static string MebibytesToString(this UInt64 value)
        {
            return BytesToString(value * 1024 * 1024, UnitOfInformationPrefix.Binary);
        }

        /// <summary>
        /// Returns a string that represents a number of megabytes in a human-readable format.
        /// </summary>
        /// <param name="value">The System.UInt64 value, in megabytes.</param>
        public static string MegabytesToString(this UInt64 value)
        {
            return BytesToString(value * 1000 * 1000, UnitOfInformationPrefix.Decimal);
        }
    }
}
