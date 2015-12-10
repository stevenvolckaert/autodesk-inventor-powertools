using System;
using System.Collections.Generic;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for Inventor.PartsListColumn objects.
    /// </summary>
    internal static class PartsListColumnExtensions
    {
        /// <summary>
        /// Returns the ID of a kFileProperty parts list column.
        /// </summary>
        /// <param name="partsListColumn">The Inventor.PartsListColumn instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partsListColumn"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="partsListColumn"/> is not of type PropertyTypeEnum.kFileProperty.</exception>
        public static KeyValuePair<string, int> GetFilePropertyId(this PartsListColumn partsListColumn)
        {
            if (partsListColumn == null)
                throw new ArgumentNullException("partsListColumn");

            if (partsListColumn.PropertyType != PropertyTypeEnum.kFileProperty)
                throw new InvalidOperationException("partsListColumn is not of type PropertyTypeEnum.kFileProperty.");

            string propertySetId;
            int propId;
            partsListColumn.GetFilePropertyId(out propertySetId, out propId);
            return new KeyValuePair<string, int>(propertySetId, propId);
        }
    }
}
