using System;
using System.Web.Routing;

namespace StevenVolckaert.Web.Mvc.Routing
{
    /// <summary>
    /// Provides extension methods for <see cref="System.Web.Routing.RouteCollection"/> objects.
    /// </summary>
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Maps the specified URL route.
        /// <para>This route issues redirects (HTTP response status code 301) to another route.</para>
        /// </summary>
        /// <param name="routes">A collection of routes for the application</param>
        /// <param name="targetRouteName">The name of the route to which must be redirected.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <returns>A reference to the mapped route.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="routes"/> or <paramref name="url"/> is <c>null</c>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "This is a URL template with special characters, not just a regular valid URL.")]
        public static Route MapRedirectingRoute(this RouteCollection routes, string targetRouteName, string url)
        {
            return MapRedirectingRoute(routes, targetRouteName, url, null, null);
        }

        /// <summary>
        /// Maps the specified URL route and sets default route values.
        /// <para>This route issues redirects (HTTP response status code 301) to another route.</para>
        /// </summary>
        /// <param name="routes">A collection of routes for the application</param>
        /// <param name="targetRouteName">The name of the route to which must be redirected.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <returns>A reference to the mapped route.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="routes"/> or <paramref name="url"/> is <c>null</c>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "This is a URL template with special characters, not just a regular valid URL.")]
        public static Route MapRedirectingRoute(this RouteCollection routes, string targetRouteName, string url, object defaults)
        {
            return MapRedirectingRoute(routes, targetRouteName, url, defaults, null);
        }

        /// <summary>
        /// Maps the specified URL route and sets default route values and constraints.
        /// <para>This route issues redirects (HTTP response status code 301) to another route.</para>
        /// </summary>
        /// <param name="routes">A collection of routes for the application.</param>
        /// <param name="targetRouteName">The name of the route to which must be redirected.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify values for the url parameter.</param>
        /// <returns>A reference to the mapped route.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="routes"/> or <paramref name="url"/> is <c>null</c>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "This is a URL template with special characters, not just a regular valid URL.")]
        public static Route MapRedirectingRoute(this RouteCollection routes, string targetRouteName, string url, object defaults, object constraints)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");

            if (String.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("url");

            var route = new RedirectingRoute(
                url,
                targetRouteName,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(constraints),
                new RedirectingRouteHandler()
            );

            routes.Add(route);
            return route;
        }
    }
}
