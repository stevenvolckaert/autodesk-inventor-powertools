using System;
using StevenVolckaert.Globalization;

namespace StevenVolckaert.Web.Mvc
{
    /// <summary>
    /// Provides strongly typed access to uniform resource identifiers (URI) that are available to the application.
    /// </summary>
    public static class UriProvider
    {
        private static string BarcoWebsiteUrl
        {
            get { return "http://www.barco.com/" + Container.Current.GetExportedValue<CultureManager>().CurrentCultureName; }
        }

        /// <summary>
        /// Gets the URI that provides access to Barco's corporate web site.
        /// </summary>
        public static Uri BarcoWebsiteUri
        {
            get { return new Uri(BarcoWebsiteUrl); }
        }

        /// <summary>
        /// Gets the URI that provides access to the about section on Barco's corporate web site.
        /// </summary>
        public static Uri AboutBarcoUri
        {
            get { return new Uri(BarcoWebsiteUrl + "/aboutbarco"); }
        }

        /// <summary>
        /// Gets the URI that provides access to the privacy policy section on Barco's corporate web site.
        /// </summary>
        public static Uri BarcoPrivacyPolicyUri
        {
            get { return new Uri(BarcoWebsiteUrl + "/aboutbarco/privacy.aspx"); }
        }

        /// <summary>
        /// Gets the URI that provides access to the support section on Barco's corporate web site.
        /// </summary>
        public static Uri BarcoSupportUri
        {
            get { return new Uri(BarcoWebsiteUrl + "/support"); }
        }
    }
}
