namespace StevenVolckaert.InventorPowerTools
{
    using System;

    /// <summary>
    ///     Wraps an <see cref="Enum"/> value, providing additional functionality.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Enum"/> value.</typeparam>
    public abstract class Enumeration<T>
        where T : struct, IConvertible
    {
        /// <summary>
        ///     Gets the underlying <see cref="Enum"/> value.
        /// </summary>
        public T EnumValue { get; }

        /// <summary>
        ///     Gets all underlying <see cref="Enum"/> values of <see cref="T"/>.
        /// </summary>
        public static T[] EnumValues { get; } = (T[])Enum.GetValues(typeof(T));

        /// <summary>
        ///     Gets the display name.
        /// </summary>
        public string DisplayName { get; }

        protected Enumeration(T enumValue, string displayName)
        {
            if (typeof(T).IsEnum == false)
                throw new ArgumentException("T must be of type System.Enum.");

            EnumValue = enumValue;
            DisplayName = displayName;
        }
    }
}
