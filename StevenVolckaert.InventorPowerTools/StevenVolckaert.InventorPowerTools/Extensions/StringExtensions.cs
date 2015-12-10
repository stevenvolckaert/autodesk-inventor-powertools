using System;
using System.IO;
using System.Text.RegularExpressions;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for System.String objects.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Returns the file name without the extension.
        /// </summary>
        /// <param name="value">The System.String instance that this extension mehod effects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public static string RemoveExtension(this string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return Regex.IsMatch(value, @"^.*\.[a-zA-Z]{3,4}$")
                ? Path.GetFileNameWithoutExtension(value)
                : value;
        }
    }
}
