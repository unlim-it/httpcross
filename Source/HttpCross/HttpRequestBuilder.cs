namespace HttpCross
{
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class HttpRequestBuilder
    {
        private readonly HttpWebRequest request;
        private byte[] data;

        internal HttpRequestBuilder(string url, string method)
        {
            this.request = (HttpWebRequest)WebRequest.Create(url);
            this.request.Method = method;
            this.data = null;
        }

        public HttpRequestBuilder WithHeader(string name, string value)
        {
            this.request.Headers[name] = value;
            return this;
        }

        public HttpRequestBuilder WithBody(object body)
        {
            this.data = Encoding.UTF8.GetBytes(body.ToString());
            return this.WithHeader("ContentLength", data.Length.ToString());
        }

        public Task<TResult> ExecuteResult<TResult>()
        {
            var resultTask = this.Execute()
                .ContinueWith(task =>
                {
                    var responseStream = task.Result.GetResponseStream();

                    using (responseStream)
                    using (var streamReader = new StreamReader(responseStream))
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        var result = serializer.Deserialize<TResult>(jsonReader);
                        return result;
                    }
                });

            return resultTask;
        }

        public Task<WebResponse> Execute()
        {
            Task<WebResponse> responseTask;

            if (this.data != null)
            {
                responseTask = Task.Factory
                    .FromAsync<Stream>(this.request.BeginGetRequestStream, this.request.EndGetRequestStream, this.request)
                    .ContinueWith(
                        task =>
                        {
                            var requestStream = task.Result;
                            using (requestStream)
                            {
                                requestStream.Write(this.data, 0, this.data.Length);
                            }

                            var webResponse = Task<WebResponse>.Factory
                                .FromAsync(this.request.BeginGetResponse, this.request.EndGetResponse, this.request).Result;

                            return webResponse;
                        });

                return responseTask;
            }

            responseTask = Task<WebResponse>.Factory
                .FromAsync(this.request.BeginGetResponse, this.request.EndGetResponse, this.request);

            return responseTask;
        }
    }
}