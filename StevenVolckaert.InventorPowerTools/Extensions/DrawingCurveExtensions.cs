namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="DrawingCurve"/> instances.
    /// </summary>
    public static class DrawingCurveExtensions
    {
        /// <summary>
        /// Returns a value that indicates whether the drawing curve is a bend line.
        /// </summary>
        /// <param name="drawingCurve">
        /// The <see cref="DrawingCurve"/> instance that this extension method affects.</param>
        /// <returns><c>true</c> if the drawing curve is a bend line, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingCurve"/> is <c>null</c>.</exception>
        public static bool IsBendLine(this DrawingCurve drawingCurve)
        {
            if (drawingCurve == null)
                throw new ArgumentNullException(nameof(drawingCurve));

            return drawingCurve.IsLine() && (
                drawingCurve.EdgeType == DrawingEdgeTypeEnum.kBendDownEdge ||
                drawingCurve.EdgeType == DrawingEdgeTypeEnum.kBendUpEdge
            );
        }

        /// <summary>
        /// Returns a value that indicates whether the drawing curve represents a line.
        /// </summary>
        /// <param name="drawingCurve">
        /// The <see cref="DrawingCurve"/> instance that this extension method affects.</param>
        /// <returns><c>true</c> if the drawing curve is a line or a line segment, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingCurve"/> is <c>null</c>.</exception>
        public static bool IsLine(this DrawingCurve drawingCurve)
        {
            if (drawingCurve == null)
                throw new ArgumentNullException(nameof(drawingCurve));

            return drawingCurve.CurveType == CurveTypeEnum.kLineCurve ||
                drawingCurve.CurveType == CurveTypeEnum.kLineSegmentCurve;
        }

        /// <summary>
        /// Returns a value that indicates wheter the drawing curve is horizontal.
        /// </summary>
        /// <param name="drawingCurve">
        /// The <see cref="DrawingCurve"/> instance that this extension method affects.</param>
        /// <returns><c>true</c> if horizontal, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingCurve"/> is <c>null</c>.</exception>
        public static bool IsHorizontal(this DrawingCurve drawingCurve)
        {
            if (drawingCurve == null)
                throw new ArgumentNullException(nameof(drawingCurve));

            return drawingCurve.StartPoint == null || drawingCurve.EndPoint == null
                ? false
                : Math.Abs(
                    drawingCurve.StartPoint.Y - drawingCurve.EndPoint.Y) <= Math.Abs(drawingCurve.StartPoint.Y * .0001
                );
        }

        /// <summary>
        /// Returns a value that indicates wheter the drawing curve is vertical.
        /// </summary>
        /// <param name="drawingCurve">
        /// The <see cref="DrawingCurve"/> instance that this extension method affects.</param>
        /// <returns><c>true</c> if vertical, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="drawingCurve"/> is <c>null</c>.</exception>
        public static bool IsVertical(this DrawingCurve drawingCurve)
        {
            if (drawingCurve == null)
                throw new ArgumentNullException(nameof(drawingCurve));

            return drawingCurve.StartPoint == null || drawingCurve.EndPoint == null
                ? false
                : Math.Abs(
                    drawingCurve.StartPoint.X - drawingCurve.EndPoint.X) <= Math.Abs(drawingCurve.StartPoint.X * .0001
                );
        }
    }
}
