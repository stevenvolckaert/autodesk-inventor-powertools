using System;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Int64 and System.UInt64 objects.
    /// </summary>
    [CLSCompliant(false)]
    public static class Int64Extensions
    {
        /// <summary>
        /// Returns a string that represents the System.UInt64 value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.UInt64 value, in kibibytes.</param>
        public static string KibibytesToString(this UInt64 value)
        {
            return ((Double)value).KibibytesToString();
        }

        /// <summary>
        /// Returns a string that represents the System.UInt64 value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.UInt64 value, in kilobytes.</param>
        public static string KilobytesToString(this UInt64 value)
        {
            return ((Double)value).KilobytesToString();
        }

        /// <summary>
        /// Returns a string that represents the System.UInt64 value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.UInt64 value, in mebibytes.</param>
        public static string MebibytesToString(this UInt64 value)
        {
            return ((Double)value).MebibytesToString();
        }

        /// <summary>
        /// Returns a string that represents the System.UInt64 value in a human-readable format.
        /// </summary>
        /// <param name="value">The System.UInt64 value, in megabytes.</param>
        public static string MegabytesToString(this UInt64 value)
        {
            return ((Double)value).MegabytesToString();
        }
    }
}
