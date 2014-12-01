using System;
using System.Collections.Generic;
using System.Linq;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for Inventor.PartsList objects.
    /// </summary>
    internal static class PartsListExtensions
    {
        /// <summary>
        /// Adds a new column that contains a custom property to a specified parts list.
        /// </summary>
        /// <param name="partsList">The Inventor.PartsList instance that this extension method affects.</param>
        /// <param name="propertyName">The name of the custom property.</param>
        /// <returns>The newly created column.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="partsList"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is <c>null</c> or empty.</exception>
        public static PartsListColumn AddCustomPropertyColumn(this PartsList partsList, string propertyName)
        {
            if (partsList == null)
                throw new ArgumentNullException("partsList");

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Argument is null or empty.", "propertyIdentifier");

            var column = partsList.PartsListColumns.Add(
                PropertyType: PropertyTypeEnum.kCustomProperty,
                PropertySetId: string.Empty,
                PropertyIdentifier: propertyName,
                TargetIndex: 0,
                InsertBefore: true
            );

            column.ValueHorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter;

            return column;
        }

        /// <summary>
        /// Removes all columns from the parts list, except the ones that appear in a specified collection.
        /// </summary>
        /// <param name="partsList">The Inventor.PartsList instance that this extension method affects.</param>
        /// <param name="columnIdsToExclude">A dictionary of IDs of kFileProperty columns that shouldn't be removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partsList"/> is <c>null</c>.</exception>
        public static void RemoveColumns(this PartsList partsList, IDictionary<string, int> columnIdsToExclude)
        {
            RemoveColumns(partsList, columnIdsToExclude, null);
        }

        /// <summary>
        /// Removes all columns from the parts list, except the ones that appear in a specified collection.
        /// 
        /// </summary>
        /// <param name="partsList">The Inventor.PartsList instance that this extension method affects.</param>
        /// <param name="columnIdsToExclude">A dictionary of IDs of kFileProperty columns that shouldn't be removed.</param>
        /// <param name="propertyTypesToExclude"></param>
        /// <exception cref="ArgumentNullException"><paramref name="partsList"/> is <c>null</c>.</exception>
        public static void RemoveColumns(this PartsList partsList, IDictionary<string, int> columnIdsToExclude, params PropertyTypeEnum[] propertyTypesToExclude)
        {
            if (partsList == null)
                throw new ArgumentNullException("partsList");

            foreach (PartsListColumn column in partsList.PartsListColumns)
            {
                if (column.PropertyType == PropertyTypeEnum.kFileProperty)
                {
                    var filePropertyId = column.GetFilePropertyId();

                    if (columnIdsToExclude != null && columnIdsToExclude.Any(x => x.Key == filePropertyId.Key && x.Value == filePropertyId.Value))
                        continue;
                }
                else if (propertyTypesToExclude != null && propertyTypesToExclude.Contains(column.PropertyType))
                    continue;

                column.Remove();
            }
        }

        /// <summary>
        /// Sets the horizontal justification of the values of all columns.
        /// </summary>
        /// <param name="partsList">The Inventor.PartsList instance that this extension method affects.</param>
        /// <param name="valueHorizontalJustification">The horizontal justification value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partsList"/> is <c>null</c>.</exception>
        public static void SetColumnValuesHorizontalJustification(this PartsList partsList, HorizontalTextAlignmentEnum valueHorizontalJustification)
        {
            if (partsList == null)
                throw new ArgumentNullException("partsList");

            foreach (PartsListColumn column in partsList.PartsListColumns)
                column.ValueHorizontalJustification = valueHorizontalJustification;
        }
    }
}
