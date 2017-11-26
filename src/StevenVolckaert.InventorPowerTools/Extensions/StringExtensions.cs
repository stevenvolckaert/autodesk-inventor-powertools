namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Provides extension methods for <see cref="string"/> instances.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Returns the file name without the extension.
        /// </summary>
        /// <param name="value">The <see cref="string"/> instance that this extension mehod affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public static string RemoveExtension(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return Regex.IsMatch(value, @"^.*\.[a-zA-Z]{3,4}$")
                ? Path.GetFileNameWithoutExtension(value)
                : value;
        }
    }
}
