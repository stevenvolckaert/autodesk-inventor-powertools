using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;

namespace StevenVolckaert.Windows
{
    /// <summary>
    /// Provides extension methods for System.Windows.FrameworkElement objects.
    /// </summary>
    public static class FrameworkElementExtensions
    {
        /// <summary>
        /// Returns a value that indicates whether the framework element is constructed and added to the object tree.
        /// </summary>
        /// <param name="frameworkElement">The FrameworkElement this extension method affects.</param>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Method may only be used on FrameworkElement objects.")]
        public static bool IsLoaded(this FrameworkElement frameworkElement)
        {
            return frameworkElement.Children().Count() > 0;
        }
    }
}
