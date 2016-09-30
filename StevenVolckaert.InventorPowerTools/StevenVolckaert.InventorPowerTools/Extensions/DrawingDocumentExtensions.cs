namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="DrawingView"/> instances. 
    /// </summary>
    internal static class DrawingDocumentExtensions
    {
        /// <summary>
        /// Gets the active linear <see cref="DimensionStyle"/>. 
        /// </summary>
        /// <param name="drawingDocument">The <see cref="DrawingDocument"/> that this extension method affects.</param>
        /// <returns>The active linear <see cref="DimensionStyle"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingDocument"/> is <c>null</c>.</exception>
        public static DimensionStyle ActiveLinearDimensionStyle(this DrawingDocument drawingDocument)
        {
            if (drawingDocument == null)
                throw new ArgumentNullException(nameof(drawingDocument));

            return drawingDocument.StylesManager.ActiveStandardStyle.ActiveObjectDefaults.LinearDimensionStyle;
        }
    }
}
