namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.Globalization;
    using Inventor;

    /// <summary>
    /// Provides extension methods for Inventor.UserInterfaceManager objects.
    /// </summary>
    internal static class UserInterfaceManagerExtensions
    {
        public static RibbonPanel GetPanel(this UserInterfaceManager userInterfaceManager, string name, string tabName, string ribbonName)
        {
            if (userInterfaceManager == null)
                throw new ArgumentNullException("userInterfaceManager");

            return userInterfaceManager.Ribbons[ribbonName].RibbonTabs[tabName].RibbonPanels[name];
        }

        /// <summary>
        /// Adds a new panel to a tab of a specified ribbon.
        /// </summary>
        /// <param name="userInterfaceManager">The Inventor.UserInterfaceManager object that this extension method affects.</param>
        /// <param name="name">The panel's name.</param>
        /// <param name="displayName">The panel's display name.</param>
        /// <param name="tabName">The tab's name.</param>
        /// <param name="ribbonName">The ribbon's name.</param>
        /// <returns>the new panel.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="userInterfaceManager"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">A panel with the specified name already exists in the specified ribbon's tab.</exception>
        public static RibbonPanel AddPanel(this UserInterfaceManager userInterfaceManager, string name, string displayName, string tabName, string ribbonName)
        {
            return userInterfaceManager.AddPanel(name, displayName, tabName, ribbonName, string.Empty, false);
        }

        /// <summary>
        /// Adds a new panel to a tab of a specified ribbon.
        /// </summary>
        /// <param name="userInterfaceManager">The Inventor.UserInterfaceManager object that this extension method affects.</param>
        /// <param name="name">The panel's name.</param>
        /// <param name="displayName">The panel's display name.</param>
        /// <param name="tabName">The tab's name.</param>
        /// <param name="ribbonName">The ribbon's name.</param>
        /// <param name="targetPanelName">The name of an existing panel to position the panel next to.</param>
        /// <param name="insertBeforeTargetPanel">A value that indicates whether to position the panel before or after the target panel.</param>
        /// <returns>The new panel.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="userInterfaceManager"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">A panel with the specified name already exists in the specified ribbon's tab.</exception>
        public static RibbonPanel AddPanel(this UserInterfaceManager userInterfaceManager, string name, string displayName, string tabName, string ribbonName, string targetPanelName, bool insertBeforeTargetPanel)
        {
            if (userInterfaceManager == null)
                throw new ArgumentNullException("userInterfaceManager");

            try
            {
                var ribbonPanel = userInterfaceManager.GetPanel(name, tabName, ribbonName);

                if (ribbonPanel != null)
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Ribbon panel '{0}' already exists in tab '{1}' of ribbon '{2}'.", name, tabName, ribbonName));
            }
            catch
            {
            }

            return
                userInterfaceManager.Ribbons[ribbonName].RibbonTabs[tabName].RibbonPanels.Add(
                    DisplayName: displayName,
                    InternalName: name,
                    ClientId: AddIn.ClientId,
                    TargetPanelInternalName: targetPanelName,
                    InsertBeforeTargetPanel: insertBeforeTargetPanel
                );
        }
    }
}
