namespace HttpCross
{
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

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

        /// <summary>
        /// Adds request header given name and value.
        /// </summary>
        public HttpRequestBuilder WithHeader(string name, string value)
        {
            this.request.Headers[name] = value;
            return this;
        }
        
        /// <summary>
        /// Adds request body given object. 
        /// </summary>
        public HttpRequestBuilder WithBody(object body)
        {
            this.data = Encoding.UTF8.GetBytes(body.ToString());
            return this;
        }

        /// <summary>
        /// Executes request with prepared settings, returns response body.
        /// </summary>
        public Task<HttpCrossResponse> Call()
        {
            var resultTask = this.ExecuteInternal()
                .ContinueWith(task => HttpCrossResponse.Create(null, task.Result));

            return resultTask;
        }
        
        private Task<WebResponse> ExecuteInternal()
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