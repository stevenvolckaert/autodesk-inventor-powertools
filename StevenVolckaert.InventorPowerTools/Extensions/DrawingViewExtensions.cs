namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="DrawingView"/> instances.
    /// </summary>
    internal static class DrawingViewExtensions
    {
        #region Helper members

        private static DrawingDocument DrawingDocument
        {
            get { return (DrawingDocument)AddIn.Inventor.ActiveDocument; }
        }

        private static ObjectCollection CreateGeometryIntentCollection(IEnumerable<DrawingCurve> drawingCurves)
        {
            var geometryIntentCollection = AddIn.Inventor.TransientObjects.CreateObjectCollection();

            foreach (var geometryIntent in drawingCurves)
                geometryIntentCollection.Add(DrawingDocument.ActiveSheet.CreateGeometryIntent(geometryIntent));

            return geometryIntentCollection;
        }

        #endregion

        /// <summary>
        /// Adds left and top projected views of a specified drawing view to the active document.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="addDimensions">A value that specifies whether dimensions need to be added.</param>
        /// <param name="drawingDistance">The distance between <paramref name="drawingView"/> and
        /// the projected views.</param>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="drawingDistance"/> is negative.</exception>
        public static void AddTopAndLeftProjectedViews(
            this DrawingView drawingView,
            bool addDimensions,
            double drawingDistance
        )
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            if (drawingDistance < 0)
                throw new ArgumentOutOfRangeException(nameof(drawingDistance));

            var drawingViews = DrawingDocument.ActiveSheet.DrawingViews;
            var drawingDimensions = DrawingDocument.ActiveSheet.DrawingDimensions;
            var dimensionStyle = DrawingDocument.ActiveLinearDimensionStyle();

            // Add top view.
            var topView =
                drawingViews.AddProjectedView(
                    ParentView: drawingView,
                    Position: AddIn.CreatePoint2D(drawingView.Position.X, drawingView.Top + 1),
                    ViewStyle: DrawingViewStyleEnum.kFromBaseDrawingViewStyle,
                    Scale: Type.Missing
                );

            topView.Position =
                AddIn.CreatePoint2D(
                    topView.Position.X,
                    topView.Position.Y + topView.Height / 2 + drawingDistance - 1
                );

            if (addDimensions)
                topView.AddHorizontalDimension(dimensionStyle);

            // Add left view.
            var leftView =
                    drawingViews.AddProjectedView(
                    ParentView: drawingView,
                    Position: AddIn.CreatePoint2D(drawingView.Left - 1, drawingView.Position.Y),
                    ViewStyle: DrawingViewStyleEnum.kFromBaseDrawingViewStyle,
                    Scale: Type.Missing
                );

            leftView.Position =
                AddIn.CreatePoint2D(
                    leftView.Position.X - leftView.Width / 2 - drawingDistance + 1,
                    leftView.Position.Y
                );

            if (addDimensions)
                leftView.AddVerticalDimension(dimensionStyle);
        }

        /// <summary>
        /// Adds a part name to the drawing view of the active document.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="partName">The name of the part to add.</param>
        /// <param name="drawingDistance">The distance between <paramref name="drawingView"/> and the name to add.
        /// </param>
        public static void AddPartName(this DrawingView drawingView, string partName, double drawingDistance)
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            var note = DrawingDocument.ActiveSheet.DrawingNotes.GeneralNotes.AddFitted(
                PlacementPoint: AddIn.CreatePoint2D(
                    drawingView.Left - drawingDistance, drawingView.Top + drawingDistance * 5
                ),
                // TODO Parameterize style override. May 5, 2016.
                FormattedText: $"<StyleOverride Bold='True' FontSize='0,4' Underline='True'>{partName}</StyleOverride>"
            );

            note.Position =
                AddIn.CreatePoint2D(
                    note.Position.X - note.FittedTextWidth,
                    note.Position.Y + note.FittedTextHeight
                );
        }

        /// <summary>
        /// Adds a horizontal dimension.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="dimensionStyle">The style to apply to the dimension.</param>
        /// <returns>The created dimension.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        public static LinearGeneralDimension AddHorizontalDimension(
            this DrawingView drawingView,
            DimensionStyle dimensionStyle
        )
        {
            return AddHorizontalDimension(drawingView, dimensionStyle, drawingDistance: 1.0);
        }

        /// <summary>
        /// Adds a horizontal dimension.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="dimensionStyle">The style to apply to the dimension.</param>
        /// <param name="drawingDistance">The distance between <paramref name="drawingView"/> and the dimension.
        /// </param>
        /// <returns>The created dimension.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="drawingDistance"/> is negative.</exception>
        public static LinearGeneralDimension AddHorizontalDimension(
            this DrawingView drawingView,
            DimensionStyle dimensionStyle,
            double drawingDistance
        )
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            if (drawingDistance < 0)
                throw new ArgumentOutOfRangeException(nameof(drawingDistance));

            var verticalLines = drawingView.VerticalLines();

            try
            {

                return verticalLines.Count > 1
                    ? DrawingDocument.ActiveSheet.DrawingDimensions.GeneralDimensions.AddLinear(
                        TextOrigin: AddIn.CreatePoint2D(drawingView.Position.X, drawingView.Top + drawingDistance),
                        IntentOne: DrawingDocument.ActiveSheet.CreateGeometryIntent(verticalLines.First()),
                        IntentTwo: DrawingDocument.ActiveSheet.CreateGeometryIntent(verticalLines.Last()),
                        DimensionType: DimensionTypeEnum.kHorizontalDimensionType,
                        ArrowheadsInside: true,
                        DimensionStyle: dimensionStyle,
                        Layer: Type.Missing)
                    : null;
            }
            catch
            {
                try
                {
                    // fallback to old method.
                    var horizontalLines = drawingView.HorizontalLines();

                    return horizontalLines.Count > 1
                        ? DrawingDocument.ActiveSheet.DrawingDimensions.GeneralDimensions.AddLinear(
                            TextOrigin: AddIn.CreatePoint2D(drawingView.Position.X, drawingView.Top + drawingDistance),
                            IntentOne: DrawingDocument.ActiveSheet.CreateGeometryIntent(horizontalLines.First()),
                            IntentTwo: Type.Missing,
                            DimensionType: DimensionTypeEnum.kHorizontalDimensionType,
                            ArrowheadsInside: true,
                            DimensionStyle: dimensionStyle,
                            Layer: Type.Missing)
                        : null;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Adds a horizontal chain dimension set to annotate the view's vertical bend lines.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="dimensionStyle">The style to apply to the dimension.</param>
        /// <returns>The created dimension set.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        public static ChainDimensionSet AddHorizontalBendLineDimensionSet(
            this DrawingView drawingView,
            DimensionStyle dimensionStyle
        )
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            var curves = new List<DrawingCurve>();
            var verticalLines = drawingView.VerticalLines();

            if (verticalLines.Any())
                curves.Add(verticalLines.First());

            foreach (var bendLine in verticalLines.Where(x => x.IsBendLine()))
                curves.Add(bendLine);

            if (verticalLines.Count() > 1)
                curves.Add(verticalLines.Last());

            curves = curves.Distinct().ToList();

            return curves.Count() > 1
                ? DrawingDocument.ActiveSheet.DrawingDimensions.ChainDimensionSets.Add(
                    GeometryIntents: CreateGeometryIntentCollection(curves),
                    PlacementPoint: AddIn.CreatePoint2D(drawingView.Center.X, drawingView.Top + 1),
                    DimensionType: DimensionTypeEnum.kHorizontalDimensionType,
                    DimensionStyle: dimensionStyle,
                    Layer: Type.Missing)
                : null;
        }

        /// <summary>
        /// Adds a vertical dimension.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="dimensionStyle">The style to apply to the dimension.</param>
        /// <returns>The created dimension.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        public static LinearGeneralDimension AddVerticalDimension(
            this DrawingView drawingView,
            DimensionStyle dimensionStyle
        )
        {
            return AddVerticalDimension(drawingView, dimensionStyle, drawingDistance: 1.0);
        }

        /// <summary>
        /// Adds a vertical dimension.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="dimensionStyle">The style to apply to the dimension.</param>
        /// <param name="drawingDistance">The distance between <paramref name="drawingView"/> and the dimension.
        /// </param>
        /// <returns>The created dimension.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="drawingDistance"/> is negative.</exception>
        public static LinearGeneralDimension AddVerticalDimension(
            this DrawingView drawingView,
            DimensionStyle dimensionStyle,
            double drawingDistance
        )
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            if (drawingDistance < 0)
                throw new ArgumentOutOfRangeException(nameof(drawingDistance));

            var horizontalLines = drawingView.HorizontalLines();

            try
            {
                return horizontalLines.Count > 1
                    ? DrawingDocument.ActiveSheet.DrawingDimensions.GeneralDimensions.AddLinear(
                        TextOrigin: AddIn.CreatePoint2D(drawingView.Left - drawingDistance, drawingView.Position.Y),
                        IntentOne: DrawingDocument.ActiveSheet.CreateGeometryIntent(horizontalLines.First()),
                        IntentTwo: DrawingDocument.ActiveSheet.CreateGeometryIntent(horizontalLines.Last()),
                        DimensionType: DimensionTypeEnum.kVerticalDimensionType,
                        ArrowheadsInside: true,
                        DimensionStyle: dimensionStyle)
                    : null;
            }
            catch
            {
                try
                {
                    // fallback to old method.
                    var verticalLines = drawingView.VerticalLines();

                    return verticalLines.Count > 1
                        ? DrawingDocument.ActiveSheet.DrawingDimensions.GeneralDimensions.AddLinear(
                            TextOrigin: AddIn.CreatePoint2D(
                                x: drawingView.Left - drawingDistance,
                                y: drawingView.Position.Y
                            ),
                            IntentOne: DrawingDocument.ActiveSheet.CreateGeometryIntent(verticalLines.First()),
                            IntentTwo: Type.Missing,
                            DimensionType: DimensionTypeEnum.kVerticalDimensionType,
                            ArrowheadsInside: true,
                            DimensionStyle: dimensionStyle)
                        : null;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Adds a vertical chain dimension set to annotate the view's horizontal bend lines.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="dimensionStyle">The style to apply to the dimension.</param>
        /// <returns>The created dimension set.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        public static ChainDimensionSet AddVerticalBendLineDimensionSet(
            this DrawingView drawingView,
            DimensionStyle dimensionStyle
        )
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            var curves = new List<DrawingCurve>();
            var horizontalLines = drawingView.HorizontalLines(); ;

            if (horizontalLines.Any())
                curves.Add(horizontalLines.First());

            foreach (var bendLine in horizontalLines.Where(x => x.IsBendLine()))
                curves.Add(bendLine);

            if (horizontalLines.Count() > 1)
                curves.Add(horizontalLines.Last());

            curves = curves.Distinct().ToList();

            return curves.Count() > 1
                ? DrawingDocument.ActiveSheet.DrawingDimensions.ChainDimensionSets.Add(
                    GeometryIntents: CreateGeometryIntentCollection(curves),
                    PlacementPoint: AddIn.CreatePoint2D(drawingView.Left - 1, drawingView.Center.Y),
                    DimensionType: DimensionTypeEnum.kVerticalDimensionType,
                    DimensionStyle: dimensionStyle,
                    Layer: Type.Missing)
                : null;
        }

        /// <summary>
        /// Adds a base view of every parts in a specified assembly view.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="drawingDistance">The distance between each generated base view.</param>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="drawingDistance"/> is negative.</exception>
        public static void AddBaseViewOfParts(this DrawingView drawingView, double drawingDistance)
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            if (drawingDistance < 0)
                throw new ArgumentOutOfRangeException(nameof(drawingDistance));

            var views = new List<DrawingView>();
            var distanceFromBottomBorder = 2.0;
            var originalY = DrawingDocument.ActiveSheet.Height - drawingDistance * 2;
            var position = AddIn.CreatePoint2D(drawingDistance * 2 + 1, originalY);

            foreach (var part in drawingView.GetReferencedDocuments<PartDocument>())
            {
                var view = DrawingDocument.ActiveSheet.DrawingViews.AddBaseView(
                    Model: (_Document)part,
                    Position: position,
                    Scale: 0.1,
                    ViewOrientation: ViewOrientationTypeEnum.kFrontViewOrientation,
                    ViewStyle: drawingView.ViewStyle
                );

                if (view.Top - 1.5 * view.Height - distanceFromBottomBorder < 0)
                {
                    // Move view to the top right.
                    view.Position =
                        AddIn.CreatePoint2D(
                            view.Position.X + (views.Any() ? views.Max(x => x.Width) : 0) + drawingDistance,
                            originalY - view.Height / 2
                        );

                    position = AddIn.CreatePoint2D(view.Left + view.Width + drawingDistance, originalY);
                }
                else
                {
                    // Move view down.
                    view.Position =
                        AddIn.CreatePoint2D(
                            view.Position.X + view.Width / 2,
                            view.Position.Y - view.Height / 2
                        );

                    position = AddIn.CreatePoint2D(position.X, view.Top - view.Height - drawingDistance);
                }

                views.Add(view);
            }
        }

        /// <summary>
        /// Fits the view into a specified rectangle by changing it's scale.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <param name="rectangle">The rectangle in which the view has to fit.</param>
        public static void Fit(this DrawingView drawingView, Rectangle rectangle)
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            if (rectangle == null)
                throw new ArgumentNullException(nameof(rectangle));

            var actualWidth = drawingView.Width / drawingView.Scale;
            var actualHeight = drawingView.Height / drawingView.Scale;

            var requiredScales =
                new List<double>
                {
                    rectangle.Width / actualWidth,
                    rectangle.Height / actualHeight
                };

            drawingView.Scale = requiredScales.Min();
            drawingView.Position = rectangle.CenterPoint;
        }

        /// <summary>
        /// Returns the horizontal lines of the drawing view.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <returns>A list of <see cref="DrawingCurve"/> instances.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        public static List<DrawingCurve> HorizontalLines(this DrawingView drawingView)
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            var horizontalLines =
                from x in drawingView.DrawingCurves.Cast<DrawingCurve>()
                where x.IsHorizontal()
                where x.IsLine()
                orderby x.MidPoint.Y descending
                select x;

            return horizontalLines.ToList();
        }

        /// <summary>
        /// Returns the vertical lines of the drawing view.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <returns>A list of <see cref="DrawingCurve"/> instances.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        public static List<DrawingCurve> VerticalLines(this DrawingView drawingView)
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            var verticalLines =
                from x in drawingView.DrawingCurves.Cast<DrawingCurve>()
                where x.IsLine()
                where x.IsVertical()
                orderby x.MidPoint.X ascending
                select x;

            return verticalLines.ToList();
        }

        /// <summary>
        /// Returns a list of documents that are referenced by the drawing view.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        public static List<TDocument> GetReferencedDocuments<TDocument>(this DrawingView drawingView)
        {
            if (drawingView == null)
                throw new ArgumentNullException(nameof(drawingView));

            var document = (_Document)drawingView.ReferencedDocumentDescriptor.ReferencedDocument;
            return document.ReferencedDocuments.OfType<TDocument>().Reverse().ToList();
        }

        /// <summary>
        /// Returns a list of sheet metal part documents that are referenced by the drawing view.
        /// </summary>
        /// <param name="drawingView">
        /// The <see cref="DrawingView"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="drawingView"/> is <c>null</c>.</exception>
        public static List<PartDocument> GetReferencedSheetMetalDocuments(this DrawingView drawingView)
        {
            return drawingView.GetReferencedDocuments<PartDocument>().Where(x => x.IsSheetMetal()).ToList();
        }
    }
}
