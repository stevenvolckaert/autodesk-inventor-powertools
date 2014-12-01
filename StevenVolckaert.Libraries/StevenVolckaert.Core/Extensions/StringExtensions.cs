using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.String objects.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the string, or a default value if the string is <c>null</c> or empty.
        /// </summary>
        /// <param name="value">The System.String value this extension method affects.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The string, or the default value if the string is <c>null</c> or empty.</returns>
        public static string DefaultIfEmpty(this String value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        /// <summary>
        /// Returns the string, or a default value if the string is <c>null</c>, empty, or white space.
        /// </summary>
        /// <param name="value">The System.String value this extension method affects.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The string, or the default value if the string is <c>null</c>, empty, or white space.</returns>
        public static string DefaultIfNullOrWhiteSpace(this String value, string defaultValue)
        {
            return String.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        /// <summary>
        /// Returns a value that indicates whether the string represents a 32-bit signed integer.
        /// </summary>
        /// <param name="value">The System.String value this extension method affects.</param>
        /// <returns><c>true</c> if <paramref name="value"/> represents a 32-bit signed integer; otherwise, <c>false</c>.</returns>
        public static bool IsInt32(this String value)
        {
            // TODO Replace Regex.IsMatch with Convert.ToInt32(string) call wrapped in a try/catch statement.
            return Regex.IsMatch(value, ValidationRules.Int32RegexValidationPattern);
        }

        /// <summary>
        /// Returns a value that indicates whether the string represents a decimal.
        /// </summary>
        /// <param name="value">The System.String value this extension method affects.</param>
        /// <returns><c>true</c> if <paramref name="value"/> represents a decimal; otherwise, <c>false</c>.</returns>
        public static bool IsDecimal(this String value)
        {
            try
            {
                Convert.ToDecimal(value, System.Globalization.CultureInfo.CurrentCulture);
                return true;
            }
            catch (ArgumentNullException)
            {
                // The string is null.
                return false;
            }
            catch (FormatException)
            {
                // The string is not formatted as a decimal.
                return false;
            }
            catch (OverflowException)
            {
                // The conversion from string to decimal overflowed. The string is too long to represent a decimal.
                return false;
            }
        }

        /// <summary>
        /// Converts the string to a collection of objects by splitting the string
        /// by a specified array of delimitors.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the returned collection.</typeparam>
        /// <param name="value">The System.String value this extension method affects.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings in the string</param>
        /// <returns>The collection.</returns>
        public static IList<T> ToList<T>(this String value, params char[] separator)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return value.Split(separator, StringSplitOptions.RemoveEmptyEntries).Cast<T>().ToList();
        }

        /// <summary>
        /// Converts the string representation of a number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">The System.String value containing the number to convert.</param>
        /// <returns>The 32-bit signed integer equivalent to the number contained in <paramref name="value"/>,
        /// or <c>null</c> if the conversion failed.</returns>
        public static int? ToNullableInt32(this String value)
        {
            int returnValue;

            return Int32.TryParse(value, out returnValue)
                ? (int?)returnValue
                : null;
        }

        /// <summary>
        /// Converts the string representation of a number to its System.Decimal equivalent.
        /// </summary>
        /// <param name="value">The System.String value containing the number to convert.</param>
        /// <returns>The System.Decimal equivalent to the number contained in <paramref name="value"/>,
        /// or <c>null</c> if the conversion failed.</returns>
        public static decimal? ToNullableDecimal(this String value)
        {
            decimal returnValue;

            return Decimal.TryParse(value, out returnValue)
                ? (decimal?)returnValue
                : null;
        }

        /// <summary>
        /// Attempts to remove all leading and trailing white-space characters from a specified string.
        /// </summary>
        /// <param name="value">The System.String to trim.</param>
        /// <returns>The string that remains after all white-space characters are removed from
        /// the start and end of the given string instance, or <c>null</c> if the string was <c>null</c>.</returns>
        public static string TryTrim(this String value)
        {
            return value == null ? null : value.Trim();
        }
    }
}
