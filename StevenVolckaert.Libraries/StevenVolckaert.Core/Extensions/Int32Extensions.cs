using System;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Int32 objects.
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        /// Returns a value that indicates whether the System.Int32 value is even.
        /// </summary>
        /// <param name="value">The System.Int32 value.</param>
        public static bool IsEven(this Int32 value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Returns a value that indicates whether the System.Int32 value is odd.
        /// </summary>
        /// <param name="value">The System.Int32 value.</param>
        public static bool IsOdd(this Int32 value)
        {
            return !value.IsEven();
        }

        /// <summary>
        /// Returns a string that represents the System.Int32 value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.Int32 value, in kibibytes.</param>
        public static string KibibytesToString(this Int32 value)
        {
            return ((UInt64)value).KibibytesToString();
        }

        /// <summary>
        /// Returns a string that represents the System.Int32 value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.Int32 value, in kilobytes.</param>
        public static string KilobytesToString(this Int32 value)
        {
            return ((UInt64)value).KilobytesToString();
        }

        /// <summary>
        /// Returns a string that represents the System.Int32 value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.Int32 value, in mebibytes.</param>
        public static string MebibytesToString(this Int32 value)
        {
            return ((UInt64)value).MebibytesToString();
        }

        /// <summary>
        /// Returns a string that represents the System.Int32 value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.Int32 value, in megabytes.</param>
        public static string MegabytesToString(this Int32 value)
        {
            return ((UInt64)value).MegabytesToString();
        }
    }
}
