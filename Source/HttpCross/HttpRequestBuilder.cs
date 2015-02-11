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

        public HttpRequestBuilder WithHeader(string name, string value)
        {
            this.request.Headers[name] = value;
            return this;
        }

        public HttpRequestBuilder WithBody(object body)
        {
            return this;
        }
    }
}