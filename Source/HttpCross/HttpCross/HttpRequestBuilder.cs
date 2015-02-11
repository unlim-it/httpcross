namespace HttpCross
{
    using System.Net;

    public class HttpRequestBuilder
    {
        private readonly WebRequest request;

        internal HttpRequestBuilder(string url, string method)
        {
            this.request = WebRequest.Create(url);
            request.Method = method;
        }
    }
}