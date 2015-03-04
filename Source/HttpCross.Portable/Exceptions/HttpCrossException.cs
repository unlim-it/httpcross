namespace HttpCross.Exceptions
{
    using System;

    public class HttpCrossException : Exception
    {
        private readonly HttpCrossResponse response;

        public HttpCrossException(string message, HttpCrossResponse response)
            : base(message)
        {
            this.response = response;
        }

        public HttpCrossRequest Request 
        {
            get
            {
                throw new NotImplementedException();
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