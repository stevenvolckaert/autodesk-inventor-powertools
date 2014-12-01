using System;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Uri objects.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Returns the base URL of the current System.Uri instance,
        /// consisting of the URI's scheme and host.
        /// </summary>
        /// <param name="uri"></param>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <c>null</c>.</exception>
        public static string ToBaseUrl(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            return uri.Scheme + @"://" + uri.Host;
        }

        /// <summary>
        /// Returns a new System.Uri which is one level above the current System.Uri,
        /// Uses the '/' character as delimiter.
        /// </summary>
        /// <param name="uri">The System.Uri this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <c>null</c>.</exception>
        public static Uri UpOneLevel(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            return uri == null
                ? null
                : new Uri(uri.ToString().Remove(uri.ToString().LastIndexOf("/", StringComparison.OrdinalIgnoreCase)));
        }
    }
}
