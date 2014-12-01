using System;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Enum objects.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts the value of the specified enumeration to an equivalent type.
        /// </summary>
        /// <typeparam name="TEnum">The type of System.Enum to parse as.</typeparam>
        /// <param name="value">The System.Enum value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> does not map to one of the named constants defined in <typeparamref name="TEnum"/>.</exception>
        public static TEnum ParseAs<TEnum>(this Enum value)
            where TEnum : struct
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return value.ParseAs<TEnum>(ignoreCase: false);
        }

        /// <summary>
        /// Converts the value of the specified enumeration to an equivalent type.
        /// A parameter specifies whether the operation is case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum">The type of System.Enum to parse as.</typeparam>
        /// <param name="value">The System.Enum value.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> does not map to one of the named constants defined in <typeparamref name="TEnum"/>.</exception>
        public static TEnum ParseAs<TEnum>(this Enum value, bool ignoreCase)
            where TEnum : struct
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return (TEnum)Enum.Parse(typeof(TEnum), value.ToString(), ignoreCase);
        }
    }
}
