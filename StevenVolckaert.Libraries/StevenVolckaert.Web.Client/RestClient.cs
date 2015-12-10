using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StevenVolckaert.Web.Client
{
    /// <summary>
    /// Provides access to resources exposed by a REST service.
    /// <para>This class cannot be inherited.</para>
    /// </summary>
    public sealed class RestClient
    {
        /// <summary>
        /// Gets the base address of the REST service endpoint.
        /// </summary>
        public Uri BaseAddress { get; private set; }

        /// <summary>
        /// Gets or sets the value of the content-type HTTP header.
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StevenVolckaert.Web.Client.RestClient"/> class.
        /// </summary>
        public RestClient()
        {
            MediaType = "application/json";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StevenVolckaert.Web.Client.RestClient"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address of the REST service endpoint.</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseAddress"/> is <c>null</c>.</exception>
        public RestClient(Uri baseAddress)
            : this()
        {
            if (baseAddress == null)
                throw new ArgumentNullException("baseAddress");

            BaseAddress = baseAddress;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Is a factory method that returns an IDisposable object.")]
        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            client.BaseAddress = BaseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));

            return client;
        }

        /// <summary>
        /// Invokes an asynchronous HTTP GET operation on the specified URI.
        /// </summary>
        /// <typeparam name="T">The type of the returned data.</typeparam>
        /// <param name="uri">The request's Uniform Resource Identifier (URI).</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="uri"/> is relative but no service endpoint base address has been configured.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Rule does not apply to the task-based asynchronous programming pattern.")]
        public async Task<WebServiceResponse<T>> GetAsync<T>(Uri uri)
            where T : class
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            if (BaseAddress == null && !uri.IsAbsoluteUri)
                throw new InvalidOperationException(Properties.Resources.UriIsRelativeAndMissingBaseAddress);

            using (var client = CreateHttpClient())
            {
                try
                {
                    var response = await client.GetAsync(uri);
                    return await response.AsWebServiceResponse<T>();
                }
                catch (HttpRequestException)
                {
                    return new WebServiceResponse<T> { Succeeded = false };
                }
            }
        }

        /// <summary>
        /// Invokes an asynchronous HTTP POST operation on the specified URI.
        /// </summary>
        /// <typeparam name="T">The type of the data to send.</typeparam>
        /// <param name="uri">The request's Uniform Resource Identifier (URI).</param>
        /// <param name="data">The data to send.</param>
        /// <returns>An instance of the StevenVolckaert.Web.Client.WebServiceResponse&lt;T&gt; class,
        /// containing the response's data.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> or <paramref name="data"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="uri"/> is relative but no service endpoint base address has been configured.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Rule does not apply to the task-based asynchronous programming pattern.")]
        public async Task<WebServiceResponse<T>> PostAsync<T>(Uri uri, T data)
            where T : class
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            if (data == null)
                throw new ArgumentNullException("data");

            if (BaseAddress == null && !uri.IsAbsoluteUri)
                throw new InvalidOperationException(Properties.Resources.UriIsRelativeAndMissingBaseAddress);

            using (var client = CreateHttpClient())
            {
                try
                {
                    var response = await client.PostAsJsonAsync(uri, data);
                    return await response.AsWebServiceResponse<T>();
                }
                catch (HttpRequestException)
                {
                    return new WebServiceResponse<T> { Succeeded = false };
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> or <paramref name="data"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="uri"/> is relative but no service endpoint base address has been configured.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Rule does not apply to the task-based asynchronous programming pattern.")]
        public async Task<WebServiceResponse<T>> PutAsync<T>(Uri uri, T data)
            where T : class
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            if (data == null)
                throw new ArgumentNullException("data");

            if (BaseAddress == null && !uri.IsAbsoluteUri)
                throw new InvalidOperationException(Properties.Resources.UriIsRelativeAndMissingBaseAddress);

            using (var client = CreateHttpClient())
            {
                try
                {
                    var response = await client.PutAsJsonAsync(uri, data);
                    return await response.AsWebServiceResponse<T>();
                }
                catch (HttpRequestException)
                {
                    return new WebServiceResponse<T> { Succeeded = false };
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="uri"/> is relative but no service endpoint base address has been configured.</exception>
        public async Task<WebServiceResponse> DeleteAsync(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            if (BaseAddress == null && !uri.IsAbsoluteUri)
                throw new InvalidOperationException(Properties.Resources.UriIsRelativeAndMissingBaseAddress);

            using (var client = CreateHttpClient())
            {
                try
                {
                    var response = await client.DeleteAsync(uri);
                    return new WebServiceResponse { Succeeded = response.IsSuccessStatusCode };
                }
                catch (HttpRequestException)
                {
                    return new WebServiceResponse { Succeeded = false };
                }
            }
        }
    }
}
