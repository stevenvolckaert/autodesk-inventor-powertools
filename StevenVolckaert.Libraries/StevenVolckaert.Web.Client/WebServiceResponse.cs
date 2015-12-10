namespace StevenVolckaert.Web.Client
{
    /// <summary>
    /// Represents the response of a web service operation.
    /// </summary>
    public class WebServiceResponse
    {
        /// <summary>
        /// Gets or sets a value that indicates whether the operation was successful.
        /// </summary>
        public bool Succeeded { get; set; }
    }

    /// <summary>
    /// Represents the response of a web service operation.
    /// <para>This class cannot be inherited.</para>
    /// </summary>
    /// <typeparam name="T">The type of the response's data.</typeparam>
    public sealed class WebServiceResponse<T> : WebServiceResponse
        where T : class
    {
        /// <summary>
        /// Gets or sets the data of the service response.
        /// </summary>
        public T Data { get; set; }
    }
}
