using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace StevenVolckaert.Web.Client
{
    /// <summary>
    /// Provides extension methods for System.Net.Http.HttpResponseMessage objects.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Converts a System.Net.Http.HttpResponseMessage instance to a
        /// StevenVolckaert.Web.Client.WebServiceResponse&lt;T&gt; instance.
        /// </summary>
        /// <typeparam name="T">The type of the response's data, if there is any.</typeparam>
        /// <param name="response">The System.Net.Http.HttpResponseMessage to create a StevenVolckaert.Web.Client.WebServiceResponse&lt;T&gt; from.</param>
        /// <returns>A new StevenVolckaert.Web.Client.WebServiceResponse&lt;T&gt; instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="response"/> is <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Rule does not apply to the task-based asynchronous programming pattern.")]
        public static async Task<WebServiceResponse<T>> AsWebServiceResponse<T>(this HttpResponseMessage response)
            where T : class
        {
            if (response == null)
                throw new ArgumentNullException("response");

            return response.IsSuccessStatusCode
                ? new WebServiceResponse<T> { Data = await response.Content.ReadAsAsync<T>(), Succeeded = true }
                : new WebServiceResponse<T> { Succeeded = false };
        }
    }
}
