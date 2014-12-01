using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using StevenVolckaert.Globalization;

namespace StevenVolckaert.Web.Mvc
{
    /// <summary>
    /// Provides extension methods for <see cref="System.Web.Mvc.HtmlHelper"/> objects.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns a combo box that's used to set the language,
        /// using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field to return.</param>
        /// <param name="cultureRouteKey"></param>
        /// <param name="routeName"></param>
        /// <exception cref="ArgumentNullException"><paramref name="htmlHelper"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/>, <paramref name="cultureRouteKey"/>, or
        /// <paramref name="routeName"/> is <c>null</c>, empty, or white space.</exception>
        public static MvcHtmlString LanguageComboBox(this HtmlHelper htmlHelper, string name, string cultureRouteKey, string routeName)
        {
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException(Properties.Resources.StringIsNullEmptyOrWhiteSpace, "name");

            if (String.IsNullOrWhiteSpace(cultureRouteKey))
                throw new ArgumentException(Properties.Resources.StringIsNullEmptyOrWhiteSpace, "cultureRouteKey");

            if (String.IsNullOrWhiteSpace(routeName))
                throw new ArgumentException(Properties.Resources.StringIsNullEmptyOrWhiteSpace, "routeName");

            var itemList = new List<SelectListItem>();
            var cultureManager = Container.Current.GetExportedValue<CultureManager>();

            foreach (var cultureInfo in cultureManager.SupportedCultures.Values)
            {
                itemList.Add(new SelectListItem
                {
                    Selected = cultureManager.IsCultureSelected(cultureInfo.Name),
                    Text = cultureInfo.NativeName,
                    Value = GetGlobalizedUrl(htmlHelper, cultureInfo.Name, cultureRouteKey, routeName)
                });
            }

            return htmlHelper.DropDownList(name, itemList, new { onchange = "window.location.replace(this.value)" });
        }

        //public static MvcHtmlString LanguageHyperLink(this HtmlHelper htmlHelper, string cultureName, string selectedText, string unselectedText, string routeName, IDictionary<string, object> htmlAttributes, string languageRouteName = "language", bool strictSelected = false)
        //{
        //    /* TODO Implement this extension method. See <http://geekswithblogs.net/shaunxu/archive/2010/05/06/localization-in-asp.net-mvc-ndash-3-days-investigation-1-day.aspx>.
        //     * Steven Volckaert. January 8, 2012.
        //     */
        //    throw new NotImplementedException();
        //    /*
        //    var routeValueDictionary = GetRouteValueDictionary(htmlHelper);
        //    // Add the culture name to the route values.
        //    routeValueDictionary["language"] = cultureName;
        //    return htmlHelper.RouteLink(selectedText, routeName, routeValueDictionary, htmlAttributes);
        //    */
        //}

        /// <summary>
        /// Returns a date in the language of the application's current culture.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="year">The year (1 through 9999).</param>
        /// <param name="month">The month (1 through 12).</param>
        /// <param name="day">The day (1 through the number of days in month).</param>
        /// <exception cref="ArgumentNullException"><paramref name="htmlHelper"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999, or  is less than 1 or greater
        /// than 12, or <paramref name="day"/> is less than 1 or greater than the number of days in <paramref name="month"/>.</exception>
        /// <example>Friday, February 27, 2009</example>
        public static MvcHtmlString LocalizedDate(this HtmlHelper htmlHelper, int year, int month, int day)
        {
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");

            return new MvcHtmlString(new DateTime(year, month, day).ToString("D", Container.Current.GetExportedValue<CultureManager>().CurrentCulture));
        }

        private static string GetGlobalizedUrl(HtmlHelper htmlHelper, string cultureName, string cultureRouteKey, string routeName)
        {
            var routeValueDictionary = GetRouteValueDictionary(htmlHelper);

            // Add the culture name to the route values.
            routeValueDictionary[cultureRouteKey] = cultureName;

            // Return the globalized URL.
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);
            return urlHelper.RouteUrl(routeName, routeValueDictionary);
        }

        private static RouteValueDictionary GetRouteValueDictionary(HtmlHelper htmlHelper)
        {
            // Get the route values from the HTML helper's view context.
            var routeValueDictionary = new RouteValueDictionary(htmlHelper.ViewContext.RouteData.Values);

            // Copy the query strings into the route values.
            var queryString = htmlHelper.ViewContext.HttpContext.Request.QueryString;

            foreach (string key in queryString)
                if (queryString[key] != null && !String.IsNullOrWhiteSpace(key))
                    if (routeValueDictionary.ContainsKey(key))
                        routeValueDictionary[key] = queryString[key];
                    else
                        routeValueDictionary.Add(key, queryString[key]);

            return routeValueDictionary;
        }
    }
}
