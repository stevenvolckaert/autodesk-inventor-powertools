using System.Diagnostics.CodeAnalysis;
using System.Web.Routing;

namespace StevenVolckaert.Web.Mvc.Routing
{
    /// <summary>
    /// Provides properties and methods for defining a route that issues redirects
    /// (HTTP response status code 301) to another route.
    /// </summary>
    public class RedirectingRoute : Route
    {
        public string TargetRouteName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectingRoute"/> class,
        /// by using the specified URL pattern and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="targetRouteName">The name of the route to redirect to.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "This is a URL template with special characters, not just a regular valid URL.")]
        public RedirectingRoute(string url, string targetRouteName, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            TargetRouteName = targetRouteName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectingRoute"/> class,
        /// by using the specified URL pattern, default parameter values, and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="targetRouteName">The name of the route to redirect to.</param>
        /// <param name="defaults">The values to use for any parameters that are missing in the URL.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "This is a URL template with special characters, not just a regular valid URL.")]
        public RedirectingRoute(string url, string targetRouteName, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            TargetRouteName = targetRouteName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectingRoute"/> class,
        /// by using the specified URL pattern, default parameter values, constraints, and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="targetRouteName">The name of the route to redirect to.</param>
        /// <param name="defaults">The values to use for any parameters that are missing in the URL.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "This is a URL template with special characters, not just a regular valid URL.")]
        public RedirectingRoute(string url, string targetRouteName, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
            TargetRouteName = targetRouteName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectingRoute"/> class,
        /// by using the specified URL pattern, default parameter values, constraints, custom values, and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="targetRouteName">The name of the route to redirect to.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">
        /// Custom values that are passed to the route handler, but which are not used to determine whether
        /// the route matches a specific URL pattern. These values are passed to the route handler, where
        /// they can be used for processing the request.
        /// </param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "This is a URL template with special characters, not just a regular valid URL.")]
        public RedirectingRoute(string url, string targetRouteName, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            TargetRouteName = targetRouteName;
        }
    }
}
