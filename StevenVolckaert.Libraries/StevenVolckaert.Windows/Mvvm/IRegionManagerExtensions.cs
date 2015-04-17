using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Regions;

namespace StevenVolckaert.Windows.Mvvm
{
    /// <summary>
    /// Provides extension methods for objects that implement <see cref="Microsoft.Practices.Prism.Regions.IRegionManager"/>.
    /// </summary>
    public static class IRegionManagerExtensions
    {
        /// <summary>
        /// Navigates the specified region manager to the main view of a specified Prism module.
        /// </summary>
        /// <param name="regionManager">The region manager that this extension method affects.</param>
        /// <param name="regionName">The name of the region to call Navigate on.</param>
        /// <param name="moduleName">The name of the module to display.</param>
        public static void RequestNavigateToModuleMainView(this IRegionManager regionManager, string regionName, string moduleName)
        {
            regionManager.RequestNavigateToModuleMainView(regionName, moduleName, null);
        }

        /// <summary>
        /// Navigates the specified region manager to the main view of a specified Prism module.
        /// </summary>
        /// <param name="regionManager">The region manager that this extension method affects.</param>
        /// <param name="regionName">The name of the region to call Navigate on.</param>
        /// <param name="moduleName">The name of the module to display.</param>
        /// <param name="navigationCallback">The method to call when the operation finishes.</param>
        public static void RequestNavigateToModuleMainView(this IRegionManager regionManager, string regionName, string moduleName, Action<NavigationResult> navigationCallback)
        {
            if (String.IsNullOrWhiteSpace(regionName))
                throw new ArgumentException(StevenVolckaert.Properties.Resources.ValueNullEmptyOrWhiteSpace, "regionName");

            if (String.IsNullOrWhiteSpace(moduleName))
                throw new ArgumentException(StevenVolckaert.Properties.Resources.ValueNullEmptyOrWhiteSpace, "moduleName");

            var uri = new Uri("/" + moduleName.Replace("Module", "View"), UriKind.Relative);

            if (navigationCallback == null)
                regionManager.RequestNavigate(regionName, uri);
            else
                regionManager.RequestNavigate(regionName, uri, navigationCallback);
        }


        /// <summary>
        /// Navigates the specified region manager to the main view of a specified Prism module.
        /// </summary>
        /// <param name="regionManager">The region manager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to call Navigate on.</param>
        /// <param name="moduleName">The name of the module to display.</param>
        /// <param name="parameters">The navigation parameters.</param>
        public static void RequestNavigateToModuleSpecificView(this IRegionManager regionManager, string regionName, string moduleName, Dictionary<string, string> parameters)
        {
            if (String.IsNullOrWhiteSpace(moduleName))
                throw new ArgumentException(StevenVolckaert.Properties.Resources.ValueNullEmptyOrWhiteSpace, "moduleName");

            var navigationParameters = new NavigationParameters();

            if (parameters != null && parameters.Count > 0)
                foreach (KeyValuePair<string, string> parameter in parameters)
                    navigationParameters.Add(parameter.Key, parameter.Value);

            var uri = new Uri("/" + moduleName.Replace("Module", "View") + navigationParameters, UriKind.Relative);
            regionManager.RequestNavigate(regionName, uri);
        }
    }
}
