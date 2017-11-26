namespace StevenVolckaert.InventorPowerTools.Drawing
{
    using Inventor;
    using System.Linq;
    using System;

    /// <summary>
    ///     Base class for behaviors that encapsulate the generation of sheet metal drawings.
    /// </summary>
    public abstract class GenerateSheetMetalDrawingsBehaviorBase : IGenerateSheetMetalDrawingsBehavior
    {
        public string DisplayName { get; protected set; }

        public GenerateSheetMetalDrawingsBehaviorType Type { get; protected set; }

        /// <summary>
        ///     Gets or sets the <see cref="DrawingDocument"/> instance the drawing generator is working with.
        /// </summary>
        protected DrawingDocument DrawingDocument { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Part"/> instance the drawing generator is working with.
        /// </summary>
        protected Part Part { get; set; }

        protected GenerateSheetMetalDrawingsBehaviorBase()
        {
        }

        public abstract void AddViews(
            Part part,
            int quantity,
            DrawingDocument drawingDocument,
            DrawingViewStyle viewStyle,
            double scale,
            double perspectiveScale
        );

        protected PartsList AddPartsList(int quantity)
        {
            if (DrawingDocument == null || Part == null)
                return null;

            var partsList =
                DrawingDocument.ActiveSheet.AddPartsList(Part.Document, PartsListLevelEnum.kPartsOnly);

            if (quantity > 0)
                partsList.PartsListRows[1]["QTY"].Value = quantity.ToString();

            return partsList;
        }

        protected DrawingView AddFlatPatternBaseView(
            DrawingViewStyle viewStyle,
            double scale,
            Point2d position
        )
        {
            if (viewStyle == null)
                throw new ArgumentNullException(nameof(viewStyle));

            if (position == null)
                throw new ArgumentNullException(nameof(position));

            if (DrawingDocument == null || Part == null)
                return null;

            var dimensionStyle = DrawingDocument
                .StylesManager.ActiveStandardStyle.ActiveObjectDefaults.LinearDimensionStyle;

            var flatPatternView = DrawingDocument.ActiveSheet.DrawingViews.AddBaseView(
                Model: (_Document)Part.Document,
                Position: position,
                Scale: scale,
                ViewOrientation: ViewOrientationTypeEnum.kDefaultViewOrientation,
                ViewStyle: viewStyle.EnumValue,
                ModelViewName: string.Empty,
                ArbitraryCamera: System.Type.Missing,
                AdditionalOptions: AddIn.CreateNameValueMap("SheetMetalFoldedModel", false)
            );

            flatPatternView.AddHorizontalBendLineDimensionSet(dimensionStyle);
            flatPatternView.AddVerticalBendLineDimensionSet(dimensionStyle);

            if (flatPatternView.VerticalLines().Any(x => x.IsBendLine()))
                flatPatternView.AddHorizontalDimension(dimensionStyle, drawingDistance: 2.0);

            if (flatPatternView.HorizontalLines().Any(x => x.IsBendLine()))
                flatPatternView.AddVerticalDimension(dimensionStyle, drawingDistance: 2.0);

            return flatPatternView;
        }

        protected DrawingView AddPerspectiveView(DrawingViewStyle viewStyle, double scale)
        {
            if (viewStyle == null)
                throw new ArgumentNullException(nameof(viewStyle));

            if (DrawingDocument == null || Part == null)
                return null;

            var sheet = DrawingDocument.ActiveSheet;

            // Add base "ISO Top Right" base view in the drawing's top right corner.
            var perspectiveView = sheet.DrawingViews.AddBaseView(
                Model: (_Document)Part.Document,
                Position: DrawingDocument.ActiveSheet.TopRightPoint(),
                Scale: scale,
                ViewOrientation: ViewOrientationTypeEnum.kIsoTopRightViewOrientation,
                ViewStyle: viewStyle.EnumValue,
                ModelViewName: string.Empty,
                ArbitraryCamera: System.Type.Missing,
                AdditionalOptions: System.Type.Missing
            );

            perspectiveView.FitToTopRightCorner(sheet);
            return perspectiveView;
        }

        protected DrawingView AddTopView(DrawingViewStyle viewStyle, double scale)
        {
            if (viewStyle == null)
                throw new ArgumentNullException(nameof(viewStyle));

            if (DrawingDocument == null || Part == null)
                return null;

            // TODO Add this view below the 'ISO Top Right' view: Implement extension method
            // 'BottomRightCorner()', and use that instead of BottomLeftCorner().

            var sheet = DrawingDocument.ActiveSheet;
            var margin = sheet.Margin();

            var topView = sheet.DrawingViews.AddBaseView(
                Model: (_Document)Part.Document,
                Position: DrawingDocument.ActiveSheet.BottomLeftCorner(),
                Scale: scale,
                ViewOrientation: ViewOrientationTypeEnum.kTopViewOrientation,
                ViewStyle: viewStyle.EnumValue,
                ModelViewName: string.Empty,
                ArbitraryCamera: System.Type.Missing,
                AdditionalOptions: System.Type.Missing
            );

            topView.Position =
                AddIn.CreatePoint2D(
                    x: margin.Left + topView.Width + 1,
                    y: margin.Bottom + topView.Height + 1
                );

            return topView;
        }
    }
}
