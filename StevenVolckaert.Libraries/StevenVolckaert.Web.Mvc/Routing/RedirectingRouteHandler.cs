using System.Web;
using System.Web.Routing;

namespace StevenVolckaert.Web.Mvc.Routing
{
    /// <summary>
    /// A handler for <see cref="RedirectingRoute"/> objects.
    /// </summary>
    public class RedirectingRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new RedirectingHttpHandler(requestContext);
        }
    }
}
