namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="Box2d"/> instances.
    /// </summary>
    public static class Box2dExtensions
    {
        public static double Height(this Box2d box2d)
        {
            if (box2d == null)
                throw new ArgumentNullException(nameof(box2d));

            return box2d.MaxPoint.Y - box2d.MinPoint.Y;
        }

        public static double Width(this Box2d box2d)
        {
            if (box2d == null)
                throw new ArgumentNullException(nameof(box2d));

            return box2d.MaxPoint.X - box2d.MinPoint.X;
        }
    }
}
