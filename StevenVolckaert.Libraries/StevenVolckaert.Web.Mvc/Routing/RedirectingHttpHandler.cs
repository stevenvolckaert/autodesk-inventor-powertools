using System;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.Routing;

namespace StevenVolckaert.Web.Mvc.Routing
{
    /// <summary>
    /// A HTTP handler that issues permanent redirects.
    /// </summary>
    public class RedirectingHttpHandler : IHttpHandler
    {
        private readonly RequestContext _requestContext;

        public bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectingHttpHandler"/> class.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request to be processed.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="requestContext"/> is <c>null</c>.</exception>
        public RedirectingHttpHandler(RequestContext requestContext)
        {
            if (requestContext == null)
                throw new ArgumentNullException("requestContext");

            _requestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var targetRouteName = ((RedirectingRoute)_requestContext.RouteData.Route).TargetRouteName;

            // Copy all query string parameters to _requestContext.RouteData.Values.
            if (context.Request.QueryString != null)
                foreach (string key in context.Request.QueryString.AllKeys)
                    _requestContext.RouteData.Values[key] = context.Request.QueryString[key];

            try
            {
                context.Response.RedirectToRoutePermanent(targetRouteName, _requestContext.RouteData.Values);
            }
            catch
            {
                throw new HttpException(Convert.ToInt32(HttpStatusCode.NotFound, CultureInfo.InvariantCulture), "Not Found");
            }
        }
    }
}
