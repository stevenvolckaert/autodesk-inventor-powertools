using System.Windows;
using System.Windows.Automation.Peers;

namespace StevenVolckaert.Windows
{
    /// <summary>
    /// Provides extension methods for System.Windows.UIElement objects.
    /// </summary>
    public static class UIElementExtensions
    {
        /// <summary>
        /// Returns a value that indicates whether the element accepts keyboard focus.
        /// </summary>
        /// <param name="element">The System.Windows.UIElement this extension method affects.</param>
        /// <returns><c>true</c> if the element accepts keyboard focus; otherwise, false.</returns>
        public static bool IsKeyboardFocusable(this UIElement element)
        {
            if (element == null)
                return false;

            var elementPeer = FrameworkElementAutomationPeer.CreatePeerForElement(element) as FrameworkElementAutomationPeer;

            return elementPeer == null
                ? false
                : elementPeer.IsKeyboardFocusable();
        }
    }
}
