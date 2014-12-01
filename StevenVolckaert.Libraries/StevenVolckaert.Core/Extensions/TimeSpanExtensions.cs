using System;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.TimeSpan objects.
    /// </summary>
    public static class TimeSpanExtensions
    {
        // TODO Embed text in a resource file.
        // TODO Use a System.StringBuilder instance.
        // TODO Replace String.Format with String.Concat.

        /// <summary>
        /// Returns a formatted string that represents the current System.TimeSpan in the language of the application's current culture.
        /// </summary>
        /// <param name="value">The System.TimeSpan that this extension method effects.</param>
        public static string ToFormattedString(this TimeSpan timeSpan)
        {
            string returnValue;

            if (timeSpan.Seconds < 60 && timeSpan.Minutes == 0 && timeSpan.Hours == 0 && timeSpan.Days == 0)
                return "Less than a minute";

            if (timeSpan.Minutes < 60 && timeSpan.Hours == 0 && timeSpan.Days == 0)
                return timeSpan.Minutes == 1 ? "minute" : "minutes";

            if (timeSpan.Hours < 24 && timeSpan.Days == 0)
            {
                returnValue = timeSpan.Hours + " " + (timeSpan.Hours == 1 ? "hour" : "hours");

                if (timeSpan.Minutes > 0)
                    return returnValue + ", " + timeSpan.Minutes + " " + (timeSpan.Minutes == 1 ? "minute" : "minutes");

                return returnValue;
            }

            returnValue = timeSpan.Days + " " + (timeSpan.Days == 1 ? "day" : "days");

            if (timeSpan.Hours > 0)
                return returnValue + ", " + timeSpan.Hours + " " + (timeSpan.Hours == 1 ? "hour" : "hours");

            if (timeSpan.Minutes > 0)
                return returnValue + ", " + timeSpan.Minutes + " " + (timeSpan.Minutes == 1 ? "minute" : "minutes");

            return returnValue;
        }
    }
}
