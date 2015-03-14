namespace HttpCross.Exceptions
{
    public class HttpOperationException : HttpCrossException
    {
        private readonly HttpCrossRequest request;
        private readonly HttpCrossResponse response;

        public HttpOperationException(string message, HttpCrossRequest request, HttpCrossResponse response)
            : base(message)
        {
            this.request = request;
            this.response = response;
        }

        public HttpCrossRequest Request 
        {
            get
            {
                return this.request;
            }
        }
        public HttpCrossResponse Response
        {
            get
            {
                return this.response;
            }
        }
    }
}