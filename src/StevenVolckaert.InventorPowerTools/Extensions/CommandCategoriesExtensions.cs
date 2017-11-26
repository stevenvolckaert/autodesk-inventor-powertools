namespace StevenVolckaert.InventorPowerTools
{
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="CommandCategories"/> instances.
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
