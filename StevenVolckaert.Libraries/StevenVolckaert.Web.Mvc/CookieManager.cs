using System;
using System.Web;

namespace StevenVolckaert.Web.Mvc
{
    /// <summary>
    /// Provides utility methods for HTTP cookie management in ASP.NET projects. 
    /// </summary>
    public static class CookieManager
    {
        /// <summary>
        /// Gets a cookie by its name.
        /// </summary>
        /// <param name="httpRequest">The HTTP request to get the cookie from.</param>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <exception cref="ArgumentNullException"><paramref name="httpRequest"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="cookieName"/> is <c>null</c>, empty, or white space.</exception>
        public static HttpCookie GetCookie(HttpRequestBase httpRequest, string cookieName)
        {
            if (httpRequest == null)
                throw new ArgumentNullException("httpRequest");

            if (String.IsNullOrWhiteSpace(cookieName))
                throw new ArgumentException("The parameter is null, empty, or white space.", "cookieName");

            return httpRequest.Cookies[cookieName];
        }

        /// <summary>
        /// Creates or updates a cookie in the cookie collection.
        /// </summary>
        /// <param name="httpResponse">The HTTP response to set the cookie for.</param>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <exception cref="ArgumentNullException"><paramref name="httpResponse"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="cookieName"/> is <c>null</c>, empty, or white space.</exception>
        public static void SetCookie(HttpResponseBase httpResponse, string cookieName)
        {
            SetCookie(httpResponse, cookieName, null);
        }

        /// <summary>
        /// Creates or updates a cookie in the cookie collection.
        /// </summary>
        /// <param name="httpResponse">The HTTP response to set the cookie for.</param>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <param name="cookieValue">The value of the cookie.</param>
        /// <exception cref="ArgumentNullException"><paramref name="httpResponse"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="cookieName"/> is <c>null</c>, empty, or white space.</exception>
        public static void SetCookie(HttpResponseBase httpResponse, string cookieName, string cookieValue)
        {
            /* TODO Parameterize the cookie expiration date (now hard-coded to 1 year).
             * Steven Volckaert. January 16, 2013.
             */

            if (httpResponse == null)
                throw new ArgumentNullException("httpResponse");

            if (String.IsNullOrWhiteSpace(cookieName))
                throw new ArgumentException("The parameter is null or empty.", "cookieName");

            var cookie = string.IsNullOrEmpty(cookieValue)
                ? new HttpCookie(cookieName)
                : new HttpCookie(cookieName, cookieValue);

            cookie.Expires = DateTime.Now.AddYears(1);
            httpResponse.SetCookie(cookie);
        }
    }
}
