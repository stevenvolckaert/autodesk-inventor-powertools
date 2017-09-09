namespace StevenVolckaert.InventorPowerTools.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Inventor;
    using System.Windows;

    public class GenerateSheetMetalFlatPatternDrawingsBehavior : GenerateSheetMetalDrawingsBehaviorBase
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GenerateSheetMetalFlatPatternDrawingsBehavior"/> class.
        /// </summary>
        public GenerateSheetMetalFlatPatternDrawingsBehavior()
        {
            DisplayName = "Sheet Metal Flat Pattern";
            Type = GenerateSheetMetalDrawingsBehaviorType.SheetMetalFlatPattern;
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

            var baseViewPosition = DrawingDocument.ActiveSheet.CenterPoint();

            AddFlatPatternBaseView(viewStyle, scale, baseViewPosition);
            var partsList = AddPartsList(quantity);
            var perspectiveView = AddPerspectiveView(viewStyle, perspectiveScale);

            if (partsList != null)
                perspectiveView.Position =
                    AddIn.CreatePoint2D(
                        x: perspectiveView.Position.X,
                        y: perspectiveView.Position.Y - partsList.RangeBox.Height()
                    );

            // Next operation not necessary for GenerateSheetMetalDrawingsBehaviorType.BaseViewWithLeftThenThreeTopProjectedViews
            AddTopView(viewStyle, perspectiveScale);
        }
    }
}
