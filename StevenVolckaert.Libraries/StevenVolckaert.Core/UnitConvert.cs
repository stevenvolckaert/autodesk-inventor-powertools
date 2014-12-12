using System;

namespace StevenVolckaert
{
    /// <summary>
    /// Converts a unit of measurement to another unit of measurement.
    /// </summary>
    public static class UnitConvert
    {
        private const double FeetPerMeter = 3.280839895;

        /// <summary>
        /// Converts a value in degrees Celsius (°C) to its equivalent in degrees Fahrenheit (°F).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in degrees Fahrenheit (°F).</returns>
        public static int FromCelsiusToFahrenheit(int value)
        {
            return FromCelsiusToFahrenheit((double)value);
        }

        /// <summary>
        /// Converts a value in degrees Celsius (°C) to its equivalent in degrees Fahrenheit (°F).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in degrees Fahrenheit (°F).</returns>
        public static int FromCelsiusToFahrenheit(double value)
        {
            var calculatedValue = (decimal)9 / (decimal)5 * (decimal)value + 32;
            return Convert.ToInt32(Math.Round(calculatedValue));
        }

        /// <summary>
        /// Converts a value in degrees Fahrenheit (°F) to its equivalent in degrees Celsius (°C).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in degrees Celsius (°C)</returns>
        public static int FromFahrenheitToCelsius(int value)
        {
            return FromFahrenheitToCelsius((double)value);
        }

        /// <summary>
        /// Converts a value in degrees Fahrenheit (°F) to its equivalent in degrees Celsius (°C).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in degrees Celsius (°C)</returns>
        public static int FromFahrenheitToCelsius(double value)
        {
            var calculatedValue = (decimal)5 / (decimal)9 * ((decimal)value - 32);
            return Convert.ToInt32(Math.Round(calculatedValue));
        }

        /// <summary>
        /// Converts a value in feet to its equivalent in meter.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in meter.</returns>
        public static double FromFeetToMeter(int value)
        {
            return FromFeetToMeter((double)value);
        }

        /// <summary>
        /// Converts a value in feet to its equivalent in meter.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in meter.</returns>
        public static double FromFeetToMeter(double value)
        {
            return value / FeetPerMeter;
        }

        /// <summary>
        /// Converts a value in meter to its equivalent in feet.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in feet.</returns>
        public static double FromMeterToFeet(int value)
        {
            return FromMeterToFeet((double)value);
        }

        /// <summary>
        /// Converts a value in meter to its equivalent in feet.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in feet.</returns>
        public static double FromMeterToFeet(double value)
        {
            return value * FeetPerMeter;
        }
    }
}
