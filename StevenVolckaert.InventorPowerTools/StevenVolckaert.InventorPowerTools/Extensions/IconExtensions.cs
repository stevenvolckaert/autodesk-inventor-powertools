using System;
using System.Drawing;
using stdole;
using StevenVolckaert.InventorPowerTools.Helpers;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for System.Drawing.Icon objects.
    /// </summary>
    internal static class IconExtensions
    {
        /// <summary>
        /// Converts the icon to a new stdole.IPictureDisp instance.
        /// </summary>
        /// <param name="icon">The System.Drawing.Icon instance that this </param>
        /// <exception cref="ArgumentNullException"><paramref name="icon"/> is <c>null</c>.</exception>
        public static IPictureDisp ToPictureDisp(this Icon icon)
        {
            if (icon == null)
                throw new ArgumentNullException("icon");

            return OleCreateConverter.ImageToPictureDisp(icon.ToBitmap());
        }
    }
}
