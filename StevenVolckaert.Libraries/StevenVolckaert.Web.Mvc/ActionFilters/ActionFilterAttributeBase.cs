using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;

namespace StevenVolckaert.Web.Mvc
{
    /// <summary>
    /// Base class for MVC action filters.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Identifier represents an abstract class.")]
    public abstract class ActionFilterAttributeBase : ActionFilterAttribute
    {
        /// <summary>
        /// Gets a cookie by its name.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <exception cref="ArgumentNullException"><paramref name="controllerContext"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="cookieName"/> is <c>null</c>, empty, or white space.</exception>
        protected static HttpCookie GetCookie(ControllerContext controllerContext, string cookieName)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            if (String.IsNullOrWhiteSpace(cookieName))
                throw new ArgumentException("The parameter is null, empty, or white space.", "cookieName");

            return CookieManager.GetCookie(controllerContext.HttpContext.Request, cookieName);
        }

        /// <summary>
        /// Creates or updates a cookie in the cookie collection.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <exception cref="ArgumentNullException"><paramref name="controllerContext"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="cookieName"/> is <c>null</c>, empty, or white space.</exception>
        protected static void SetCookie(ControllerContext controllerContext, string cookieName)
        {
            SetCookie(controllerContext, cookieName, null);
        }

        /// <summary>
        /// Creates or updates a cookie in the cookie collection.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <param name="cookieValue">The value of the cookie.</param>
        /// <exception cref="ArgumentNullException"><paramref name="controllerContext"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="cookieName"/> is <c>null</c>, empty, or white space.</exception>
        protected static void SetCookie(ControllerContext controllerContext, string cookieName, string cookieValue)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            if (String.IsNullOrWhiteSpace(cookieName))
                throw new ArgumentException("The parameter is null, empty, or white space.", "cookieName");

            CookieManager.SetCookie(controllerContext.HttpContext.Response, cookieName, cookieValue);
        }
    }
}
