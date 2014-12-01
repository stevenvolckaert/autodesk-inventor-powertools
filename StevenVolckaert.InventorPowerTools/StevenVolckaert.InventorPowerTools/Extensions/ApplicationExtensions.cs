using System.IO;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for Inventor.Application objects.
    /// </summary>
    internal static class ApplicationExtensions
    {
        /// <summary>
        /// Saves the ribbon structure in a text file.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="filePath">The path to the text file.</param>
        public static void SaveRibbonStructure(this Application application, string filePath)
        {
            using (var stream = new StreamWriter(filePath))
            {
                foreach (Inventor.Ribbon ribbon in application.UserInterfaceManager.Ribbons)
                {
                    stream.WriteLine(string.Format("Ribbon \"{0}\" ({1})", ribbon.InternalName, ribbon.RibbonTabs.Count));

                    foreach (Inventor.RibbonTab tab in ribbon.RibbonTabs)
                    {
                        stream.WriteLine(string.Format("    {0} - \"{1}\" ({2})", tab.InternalName, tab.DisplayName, tab.RibbonPanels.Count));

                        foreach (Inventor.RibbonPanel panel in tab.RibbonPanels)
                            stream.WriteLine(string.Format("        {0} - \"{1}\"", panel.InternalName, panel.DisplayName));
                    }
                }
                stream.Close();
            }
        }
    }
}
