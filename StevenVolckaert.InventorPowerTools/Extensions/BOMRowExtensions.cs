namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="BOMRow"/> instances.
    /// </summary>
    public static class BOMRowExtensions
    {
        /// <summary>
        /// Returns the BOM row's primary component definition, or <c>null</c>
        /// if the row doesn't contain one.
        /// </summary>
        /// <param name="row">The <see cref="BOMRow"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="BOMRow"/> is <c>null</c>.</exception>
        public static ComponentDefinition PrimaryComponentDefinition(this BOMRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));

            if (row.ComponentDefinitions.Count == 0)
                return null;

            return row.ComponentDefinitions[1];
        }
    }
}
