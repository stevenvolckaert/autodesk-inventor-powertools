using System;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extensions methods for Inventor.PropertySet objects.
    /// </summary>
    internal static class PropertySetExtensions
    {
        public static Property TryGetValue(this PropertySet propertySet, string key)
        {
            if (propertySet == null)
                throw new ArgumentNullException("propertySet");

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
        /// <param name="propertySet">The Inventor.PropertySet instance that this extension method affects.</param>
        /// <param name="key">The property's key.</param>
        /// <param name="value">The property's value.</param>
        /// <returns>The property.</returns>
        public static Property SetValue(this PropertySet propertySet, string key, object value)
        {
            if (propertySet == null)
                throw new ArgumentNullException("propertySet");

            var property = propertySet.TryGetValue(key);

            if (property == null)
                property = propertySet.Add(value, key);
            else
                property.Value = value;

            return property;
        }
    }
}
