namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="PartsList"/> objects.
    /// </summary>
    internal static class PartsListExtensions
    {
        /// <summary>
        /// Adds a new column that contains a custom property to a specified parts list.
        /// </summary>
        /// <param name="partsList">The <see cref="PartsList"/> instance that this extension method affects.</param>
        /// <param name="propertyName">The name of the custom property.</param>
        /// <returns>The newly created column.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="partsList"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is <c>null</c> or empty.</exception>
        public static PartsListColumn AddCustomPropertyColumn(this PartsList partsList, string propertyName)
        {
            if (partsList == null)
                throw new ArgumentNullException(nameof(partsList));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Argument is null or empty.", nameof(propertyName));

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
        /// Clears all columns from the parts list, except the ones that appear in a specified collection.
        /// </summary>
        /// <param name="partsList">The <see cref="PartsList"/> instance that this extension method affects.</param>
        /// <param name="columnIdsToKeep">A dictionary of IDs of <see cref="PropertyTypeEnum.kFileProperty"/> columns
        /// that shouldn't be removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partsList"/> is <c>null</c>.</exception>
        public static void ClearColumnsExcept(this PartsList partsList, IDictionary<string, int> columnIdsToKeep)
        {
            ClearColumnsExcept(partsList, columnIdsToKeep, propertyTypesToKeep: null);
        }

        /// <summary>
        /// Clears all columns from the parts list, except the ones that appear in a specified collection.
        /// </summary>
        /// <param name="partsList">The <see cref="PartsList"/> instance that this extension method affects.</param>
        /// <param name="columnIdsToKeep">A dictionary of IDs of <see cref="PropertyTypeEnum.kFileProperty"/> columns
        /// that shouldn't be removed.</param>
        /// <param name="propertyTypesToKeep"></param>
        /// <exception cref="ArgumentNullException"><paramref name="partsList"/> is <c>null</c>.</exception>
        public static void ClearColumnsExcept(
            this PartsList partsList,
            IDictionary<string, int> columnIdsToKeep,
            params PropertyTypeEnum[] propertyTypesToKeep
        )
        {
            if (partsList == null)
                throw new ArgumentNullException(nameof(partsList));

            foreach (PartsListColumn column in partsList.PartsListColumns)
            {
                if (column.PropertyType == PropertyTypeEnum.kFileProperty)
                {
                    var filePropertyId = column.GetFilePropertyId();

                    if (columnIdsToKeep != null &&
                        columnIdsToKeep.Any(x => x.Key == filePropertyId.Key && x.Value == filePropertyId.Value))
                        continue;
                }
                else if (propertyTypesToKeep != null && propertyTypesToKeep.Contains(column.PropertyType))
                    continue;

                column.Remove();
            }
        }

        /// <summary>
        /// Sets the horizontal justification of the values of all columns.
        /// </summary>
        /// <param name="partsList">The <see cref="PartsList"/> instance that this extension method affects.</param>
        /// <param name="valueHorizontalJustification">The horizontal justification value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partsList"/> is <c>null</c>.</exception>
        public static void SetColumnValuesHorizontalJustification(
            this PartsList partsList,
            HorizontalTextAlignmentEnum valueHorizontalJustification
        )
        {
            if (partsList == null)
                throw new ArgumentNullException(nameof(partsList));

            foreach (PartsListColumn column in partsList.PartsListColumns)
                column.ValueHorizontalJustification = valueHorizontalJustification;
        }

        public static PartsListRow GetRowByPartNumber(this PartsList partsList, string partNumber)
        {
            if (partsList == null)
                throw new ArgumentNullException(nameof(partsList));

            // Select the 'part number' column.
            //var partNumberColumn =
            //    partsList.GetColumnByFilePropertyId(key: "{32853F0F-3444-11D1-9E93-0060B03C1CA6}", value: 5);

            var rows = partsList.PartsListRows.Cast<PartsListRow>();
            return rows.FirstOrDefault(row => row.Cast<PartsListCell>().Any(x => x.Value == partNumber));
        }

        //public static PartsListColumn GetColumnByFilePropertyId(this PartsList partsList, string key, int value)
        //{
        //    if (partsList == null)
        //        throw new ArgumentNullException(nameof(partsList));

        //    if (string.IsNullOrEmpty(key))
        //        throw new ArgumentException(nameof(key));

        //    return partsList.PartsListColumns.Cast<PartsListColumn>()
        //        .Where(x => x.PropertyType == PropertyTypeEnum.kFileProperty)
        //        .FirstOrDefault(
        //            x =>
        //            {
        //                var id = x.GetFilePropertyId();
        //                return id.Key == key && id.Value == value;
        //            }
        //        );
        //}
    }
}
