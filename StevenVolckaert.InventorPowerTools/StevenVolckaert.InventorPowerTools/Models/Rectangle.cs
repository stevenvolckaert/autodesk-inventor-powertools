using System;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    public class Rectangle
    {
        public Point2d BottomLeftCorner { get; private set; }
        public Point2d TopRightCorner { get; private set; }

        public double Width { get; private set; }
        public double Height { get; private set; }

        public Point2d CenterPoint { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerTools.Models.Rectangle"/> class.
        /// </summary>
        public Rectangle(Point2d bottomLeftCorner, Point2d topRightCorner)
        {
            if (bottomLeftCorner == null)
                throw new ArgumentNullException("bottomLeftCorner");

            if (topRightCorner == null)
                throw new ArgumentNullException("topRightCorner");

            BottomLeftCorner = bottomLeftCorner;
            TopRightCorner = topRightCorner;

            Width = topRightCorner.X - bottomLeftCorner.X;
            Height = topRightCorner.Y - bottomLeftCorner.Y;

            CenterPoint =
                AddIn.CreatePoint2D(
                    bottomLeftCorner.X + Width / 2,
                    bottomLeftCorner.Y + Height / 2
                );
        }
    }
}
