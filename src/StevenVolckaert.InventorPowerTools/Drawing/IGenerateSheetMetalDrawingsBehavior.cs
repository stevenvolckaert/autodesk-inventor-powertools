namespace StevenVolckaert.InventorPowerTools.Drawing
{
    using Inventor;

    public interface IGenerateSheetMetalDrawingsBehavior
    {
        /// <summary>
        ///     Gets the display name of the behavior.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        ///     Gets the type of the behavior.
        /// </summary>
        GenerateSheetMetalDrawingsBehaviorType Type { get; }

        /// <summary>
        ///     Adds views of a <see cref="Part"/> to the specified <see cref="DrawingDocument"/>.
        /// </summary>
        /// <param name="part">
        ///     The <see cref="Part"/> to create views of.
        /// </param>
        /// <param name="quantity">
        ///     The quantity of the part that needs to be produced.
        ///     When larger than 0, this value will appear in the parts list.
        /// </param>
        /// <param name="drawingDocument">
        ///     The <see cref="DrawingDocument"/> to create the views in.
        /// </param>
        /// <param name="viewStyle">
        /// 
        /// </param>
        /// <param name="scale">
        ///     The scale at which the generated views must be drawn.
        /// </param>
        /// <param name="perspectiveScale">
        ///     The scale at which the perspective view must be drawn.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="part"/> or <paramref name="drawingDocument"/> is <c>null</c>.
        /// </exception>
        void AddViews(
            Part part,
            int quantity,
            DrawingDocument drawingDocument,
            DrawingViewStyle viewStyle,
            double scale,
            double perspectiveScale
        );
    }
}
