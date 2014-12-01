using System;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for Inventor.Box2d objects.
    /// </summary>
    internal static class Box2dExtensions
    {
        public static double Height(this Box2d box2d)
        {
            if (box2d == null)
                throw new ArgumentNullException("box2d");

            return box2d.MaxPoint.Y - box2d.MinPoint.Y;
        }

        public static double Width(this Box2d box2d)
        {
            if (box2d == null)
                throw new ArgumentNullException("box2d");

            return box2d.MaxPoint.X - box2d.MinPoint.X;
        }
    }
}
