namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    public class Rectangle
    {
        public Point2d BottomLeftCorner { get; }
        public Point2d TopRightCorner { get; }

        public double Width { get; }
        public double Height { get; }

        public Point2d CenterPoint { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle(Point2d bottomLeftCorner, Point2d topRightCorner)
        {
            if (bottomLeftCorner == null)
                throw new ArgumentNullException(nameof(bottomLeftCorner));

            if (topRightCorner == null)
                throw new ArgumentNullException(nameof(topRightCorner);

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
