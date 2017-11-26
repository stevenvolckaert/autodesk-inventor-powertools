namespace StevenVolckaert.InventorPowerTools.Drawing
{
    using System;
    using Inventor;

    public class GenerateBaseViewWithLeftThenThreeTopProjectedViewsDrawingsBehavior
        : GenerateSheetMetalDrawingsBehaviorBase
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GenerateBaseViewWithLeftThenThreeTopProjectedViewsDrawingsBehavior"/> class.
        /// </summary>
        public GenerateBaseViewWithLeftThenThreeTopProjectedViewsDrawingsBehavior()
        {
            DisplayName = "Base View With Left and 3 Top Projected Views";
            Type = GenerateSheetMetalDrawingsBehaviorType.BaseViewWithLeftThenThreeTopProjectedViews;
        }

        public override void AddViews(
            Part part,
            int quantity,
            DrawingDocument drawingDocument,
            DrawingViewStyle viewStyle,
            double scale,
            double perspectiveScale
        )
        {
            Part = part ?? throw new ArgumentNullException(nameof(part));
            DrawingDocument = drawingDocument ?? throw new ArgumentNullException(nameof(drawingDocument));

            if (viewStyle == null)
                throw new ArgumentNullException(nameof(viewStyle));

            var centerPoint = DrawingDocument.ActiveSheet.CenterPoint();
            var baseViewPosition = AddIn.CreatePoint2D(x: centerPoint.X * 1.5, y: centerPoint.Y * 0.5);

            var baseView = AddFlatPatternBaseView(viewStyle, scale, baseViewPosition);

            baseView.AddLeftThenTopProjectedViews(
                numberOfTopViews: 3,
                addDimensions: false,
                drawingDistance: 5
            );

            var partsList = AddPartsList(quantity);
            var perspectiveView = AddPerspectiveView(viewStyle, perspectiveScale);

            if (partsList != null)
                perspectiveView.Position =
                    AddIn.CreatePoint2D(
                        x: perspectiveView.Position.X,
                        y: perspectiveView.Position.Y - partsList.RangeBox.Height()
                    );
        }
    }
}
