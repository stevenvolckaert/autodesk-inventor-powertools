using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using StevenVolckaert.Globalization;

namespace StevenVolckaert.Web.Mvc
{
    /// <summary>
    /// Represents an attribute that is used to provide globalization support to MVC controllers.
    /// This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// The implementation of this class is based on information available at
    /// http://geekswithblogs.net/shaunxu/archive/2010/05/06/localization-in-asp.net-mvc-ndash-3-days-investigation-1-day.aspx
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class GlobalizationFilterAttribute : ActionFilterAttributeBase
    {
        /// <summary>
        /// Gets the name of the cookie that is used to save a visitor's selected culture.
        /// </summary>
        public string CurrentCultureCookieName { get; private set; }

        /// <summary>
        /// Gets the key that is used to access the culture name of the filter context's route data.
        /// </summary>
        public string CultureNameKey { get; private set; }

        [Import]
        public CultureManager CultureManager { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StevenVolckaert.Web.Mvc.GlobalizationFilterAttribute"/> class.
        /// </summary>
        /// <param name="cultureNameKey">The key used to access the culture name of the filter context's route data.</param>
        /// <param name="currentCultureCookieName">The name of the cookie that is used to save a visitor's selected culture.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="cultureNameKey"/> is <c>null</c> or white space.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="currentCultureCookieName"/> is <c>null</c> or white space.</exception>
        public GlobalizationFilterAttribute(string cultureNameKey, string currentCultureCookieName)
        {
            if (String.IsNullOrWhiteSpace(cultureNameKey))
                throw new ArgumentOutOfRangeException("cultureNameKey");

            if (String.IsNullOrWhiteSpace(currentCultureCookieName))
                throw new ArgumentOutOfRangeException("currentCultureCookieName");

            CurrentCultureCookieName = currentCultureCookieName;
            CultureNameKey = cultureNameKey;

            Container.Current.SatisfyImportsOnce(this);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            // Check if the controller is marked with IgnoreGlobalizationFilterAttribute, and return if it is.
            if (filterContext.Controller.GetType().GetCustomAttributes(typeof(IgnoreGlobalizationFilterAttribute), false).Length > 0)
                return;

            // Check if the executing action is marked with IgnoreGlobalizationFilterAttribute, and return if it is.
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(IgnoreGlobalizationFilterAttribute), false).Length > 0)
                return;

            var routeValueDictionary = filterContext.RouteData.Values;

            // Copy all query-string values of the HTTP request to the route value dictionary.
            foreach (var key in filterContext.RequestContext.HttpContext.Request.QueryString.AllKeys)
                routeValueDictionary[key] = filterContext.RequestContext.HttpContext.Request.QueryString[key];

            if (routeValueDictionary[CultureNameKey] != null && !String.IsNullOrWhiteSpace(routeValueDictionary[CultureNameKey].ToString()))
            {
                // The route data contains the culture name.

                var cultureName = routeValueDictionary[CultureNameKey].ToString();
                CultureManager.SetCulture(cultureName);

                // If required, redirect, so the URL in the web browser's address bar is updated with the correct culture name.
                if (cultureName != CultureManager.CurrentCultureName)
                    filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
            }
            else
            {
                // The route data doesn't contain the culture name. Let's see if there is a cookie with the name.
                var cookie = GetCookie(filterContext, CurrentCultureCookieName);

                if (cookie == null)
                {
                    // There is no cookie. Default to the user's default languages, as set in their browser.

                    /* TODO Sort the preferred languages on their probability q.
                     * Steven Volckaert. January 11, 2013.
                     */

                    //var cultureNamesList = new List<string>(filterContext.HttpContext.Request.UserLanguages);
                    //cultureNamesList.Insert(...);

                    var cultureNames = filterContext.HttpContext.Request.UserLanguages;

                    for (var i = 0; i < cultureNames.Length; i++)
                    {
                        var index = cultureNames[i].IndexOf(";", StringComparison.Ordinal);

                        if (index > 0)
                            cultureNames[i] = cultureNames[i].Substring(0, index);
                    }

                    CultureManager.SetCulture(cultureNames);
                }
                else
                {
                    // The cookie is found. Let's use it to set the current culture.

                    var cultureName = cookie.Value;
                    CultureManager.SetSpecificCulture(cultureName);

                    // Redirect, so the URL in the web browser's address bar is updated with the correct culture name.
                    filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
                }
            }

            // Update the culture name in the route data.
            routeValueDictionary[CultureNameKey] = CultureManager.CurrentCultureName;

            // Store the name of the current culture into a cookie.
            SetCookie(filterContext, CurrentCultureCookieName, CultureManager.CurrentCultureName);

            base.OnActionExecuting(filterContext);
        }
    }
}
