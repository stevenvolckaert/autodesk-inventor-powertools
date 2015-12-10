using System;
using System.Runtime.InteropServices;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for Inventor.BOMViews objects.
    /// </summary>
    public static class BOMViewsExtensions
    {
        /// <summary>
        /// Gets the BOM view associated with the specified name.
        /// </summary>
        /// <param name="views">The Inventor.BOMViews instance that this extension method affects.</param>
        /// <param name="viewName">The name of the view to get.</param>
        /// <param name="value">When this method returns, contains the BOM view associated with the specified name,
        /// if the name is found; otherwise, the default value of the <paramref name="value"/> parameter.</param>
        /// <returns><c>true</c> if <paramref name="views"/> contains a view with the specified <paramref name="viewName"/>;
        /// otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="views"/> is <c>null</c>.</exception>
        public static bool TryGetValue(this BOMViews views, String viewName, out BOMView value)
        {
            if (views == null)
                throw new ArgumentNullException("views");

            try
            {
                value = views[viewName];
                return true;
            }
            catch (ArgumentException)
            {
                value = default(BOMView);
                return false;
            }
            catch (COMException)
            {
                value = default(BOMView);
                return false;
            }
        }
    }
}
