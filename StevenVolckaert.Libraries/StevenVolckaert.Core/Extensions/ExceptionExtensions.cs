using System;
using System.Globalization;
using System.Text;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extensions methods for System.Exception objects.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Returns a message that describes the current exception in detail.
        /// </summary>
        /// <param name="exception">The exception that this extension method affects.</param>
        /// <returns>A message that describes <paramref name="exception"/> in detail,
        /// or an emtpy string if <paramref name="exception"/> is <c>null</c>.</returns>
        public static string DetailedMessage(this Exception exception)
        {
            return DetailedMessage(exception, formatProvider: CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a message that describes the current exception in detail.
        /// </summary>
        /// <param name="exception">The exception that this extension method affects.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A message that describes <paramref name="exception"/> in detail,
        /// or an emtpy string if <paramref name="exception"/> is <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="formatProvider"/> is <c>null</c>.</exception>
        public static string DetailedMessage(this Exception exception, IFormatProvider formatProvider)
        {
            if (exception == null)
                return String.Empty;

            if (formatProvider == null)
                throw new ArgumentNullException("formatProvider");

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[" + DateTime.Now.ToString("s", formatProvider) + "] Encountered an exception of type " + exception);
            stringBuilder.AppendLine("System.Environment.OSVersion: " + Environment.OSVersion ?? "null");
            stringBuilder.AppendLine("System.Environment.Is64BitOperatingSystem: " + (Environment.Is64BitOperatingSystem ? "True" : "False"));
            stringBuilder.AppendLine("System.Environment.Is64BitProcess: " + (Environment.Is64BitProcess ? "True" : "False"));
            stringBuilder.AppendLine("System.Environment.Version: " + Environment.Version ?? "null");
            stringBuilder.AppendLine("System.Environment.WorkingSet: " + ((UInt64)Environment.WorkingSet).BytesToString(UnitOfInformationPrefix.Binary));

            return stringBuilder.ToString();
        }
    }
}
