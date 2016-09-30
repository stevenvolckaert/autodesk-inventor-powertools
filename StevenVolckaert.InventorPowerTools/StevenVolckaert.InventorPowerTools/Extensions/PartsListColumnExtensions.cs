namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.Collections.Generic;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="PartsListColumn"/> instances.
    /// </summary>
    internal static class PartsListColumnExtensions
    {
        /// <summary>
        /// Returns the ID of a <see cref="PropertyTypeEnum.kFileProperty"/> parts list column.
        /// </summary>
        /// <param name="partsListColumn">
        /// The <see cref="PartsListColumn"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partsListColumn"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="partsListColumn"/> is not of type <see cref="PropertyTypeEnum.kFileProperty"/>.</exception>
        public static KeyValuePair<string, int> GetFilePropertyId(this PartsListColumn partsListColumn)
        {
            if (partsListColumn == null)
                throw new ArgumentNullException(nameof(PartsListColumn));

            if (partsListColumn.PropertyType != PropertyTypeEnum.kFileProperty)
                throw new InvalidOperationException(
                    $"{nameof(partsListColumn)} is not of type {PropertyTypeEnum.kFileProperty}."
                );

            string propertySetId;
            int propId;
            partsListColumn.GetFilePropertyId(out propertySetId, out propId);
            return new KeyValuePair<string, int>(propertySetId, propId);
        }
    }
}
