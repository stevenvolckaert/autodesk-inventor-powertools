namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    /// <summary>
    /// Provides extensions methods for <see cref="PropertySet"/> instances.
    /// </summary>
    internal static class PropertySetExtensions
    {
        public static Property TryGetValue(this PropertySet propertySet, string key)
        {
            if (propertySet == null)
                throw new ArgumentNullException(nameof(propertySet));

            try
            {
                return propertySet[key];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Sets a value in the property set. If the property doesn't exist, it is created.
        /// </summary>
        /// <param name="propertySet">
        /// The <see cref="PropertySet"/> instance that this extension method affects.</param>
        /// <param name="key">The property's key.</param>
        /// <param name="value">The property's value.</param>
        /// <returns>The property.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="propertySet"/> is <c>null</c>.</exception>
        public static Property SetValue(this PropertySet propertySet, string key, object value)
        {
            if (propertySet == null)
                throw new ArgumentNullException(nameof(propertySet));

            var property = propertySet.TryGetValue(key);

            if (property == null)
                property = propertySet.Add(value, key);
            else
                property.Value = value;

            return property;
        }
    }
}
