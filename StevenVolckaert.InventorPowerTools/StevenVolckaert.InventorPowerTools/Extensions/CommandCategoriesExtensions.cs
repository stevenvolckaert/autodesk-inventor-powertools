using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for Inventor.CommandCategories objects.
    /// </summary>
    internal static class CommandCategoriesExtensions
    {
        public static CommandCategory TryGetValue(this CommandCategories commandCategories, string key)
        {
            try
            {
                return commandCategories[key];
            }
            catch
            {
                return null;
            }
        }
    }
}
