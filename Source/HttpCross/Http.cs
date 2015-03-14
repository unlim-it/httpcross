namespace HttpCross
{
    using System;
    using System.Threading.Tasks;

    using HttpCross.Exceptions;

    public class Http
    {
        private readonly HttpCrossRequest request;
        private Action<HttpOperationException> exceptionHandler;

        private Http()
        {
            this.request = new HttpCrossRequest();
        }

        /// <summary>
        /// Creates new instance of Http request.
        /// </summary>
        public static Http New 
        {
            get { return new Http(); }
        }
        
        /// <summary>
        /// Sets the base server URL.
        /// </summary>
        public Http WithBaseURL(string baseURL)
        {
            if (string.IsNullOrWhiteSpace(baseURL))
            {
                throw new ArgumentException("Base URL can't be null or empty");
            }

            this.request.URL = baseURL;
            return this;
        }

        /// <summary>
        /// By default request body will not 
        /// </summary>
        public Http WithBody(string content, string contentType = "text/html")
        {
            if (content == null)
            {
                throw new ArgumentException("Content can not be null");
            }

            this.request.ContentType = contentType;
            this.request.Body = content;
            return this;
        }

        /// <summary>
        /// Adds request header by name.
        /// </summary>
        public Http WithHeader(string name, string value)
        {
            this.request.Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Exception handler.
        /// </summary>
        public Http HandleException(Action<HttpOperationException> handler)
        {
            this.exceptionHandler = handler;
            return this;
        }

        /// <summary>
        /// Requests a representation of the specified resource.
        /// </summary>
        public async Task<HttpCrossResponse> Get(string url = "")
        {
            this.request.URL += url;
            this.request.Method = "GET";

            return await WebInvoker.Invoke(this.request, this.exceptionHandler);
        }

        /// <summary>
        /// Submits data to be processed to a specified resource.
        /// </summary>
        public async Task<HttpCrossResponse> Post(string url = "")
        {
            this.request.URL += url;
            this.request.Method = "POST";

            return await WebInvoker.Invoke(this.request, this.exceptionHandler);
        }

        /// <summary>
        /// Submits data to be processed to a specified resource.
        /// </summary>
        public async Task<HttpCrossResponse> Put(string url = "")
        {
            this.request.URL += url;
            this.request.Method = "PUT";

            return await WebInvoker.Invoke(this.request, this.exceptionHandler);
        }
        
        /// <summary>
        /// Deletes the specified resource.
        /// </summary>
        public async Task<HttpCrossResponse> Delete(string url = "")
        {
            this.request.URL += url;
            this.request.Method = "DELETE";

            return await WebInvoker.Invoke(this.request, this.exceptionHandler);
        }
    }
}