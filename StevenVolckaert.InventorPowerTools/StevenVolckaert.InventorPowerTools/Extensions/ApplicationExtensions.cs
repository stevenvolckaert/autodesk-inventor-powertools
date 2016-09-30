namespace StevenVolckaert.InventorPowerTools
{
    using System.Diagnostics;
    using System.IO;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="Application"/> instances.
    /// </summary>
    internal static class ApplicationExtensions
    {
        /// <summary>
        /// Saves the ribbon structure in a text file.
        /// </summary>
        /// <param name="application">
        /// The <see cref="Application"/> instance that this extension method affects.</param>
        /// <param name="filePath">The path to the text file.</param>
        [Conditional("DEBUG")]
        public static void SaveRibbonStructure(this Application application, string filePath)
        {
            using (var stream = new StreamWriter(filePath))
            {
                foreach (Ribbon ribbon in application.UserInterfaceManager.Ribbons)
                {
                    stream.WriteLine("Ribbon \"" + ribbon.InternalName + "\" (" + ribbon.RibbonTabs.Count + ")");

                    foreach (RibbonTab tab in ribbon.RibbonTabs)
                    {
                        stream.WriteLine($"    {tab.InternalName} - \"{tab.DisplayName}\" ({tab.RibbonPanels.Count})");

                        foreach (RibbonPanel panel in tab.RibbonPanels)
                            stream.WriteLine($"        {panel.InternalName} - \"{panel.DisplayName}\"");
                    }
                }
            }
        }
    }
}
