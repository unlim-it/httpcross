namespace HttpCross
{
    public class Http
    {
        /// <summary>
        /// Requests a representation of the specified resource.
        /// </summary>
        public static HttpRequestBuilder Get(string url)
        {
            return new HttpRequestBuilder(url, "GET");
        }

        /// <summary>
        /// Submits data to be processed to a specified resource.
        /// </summary>
        public static HttpRequestBuilder Post(string url)
        {
            return new HttpRequestBuilder(url, "POST");
        }

        /// <summary>
        /// Requests that the enclosed entity be stored under the supplied URI.
        /// </summary>
        public static HttpRequestBuilder Put(string url)
        {
            return new HttpRequestBuilder(url, "PUT");
        }

        /// <summary>
        /// Deletes the specified resource.
        /// </summary>
        public static HttpRequestBuilder Delete(string url)
        {
            return new HttpRequestBuilder(url, "DELETE");
        }
    }
}