namespace HttpCross
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    public class HttpCrossResponse
    {
        private HttpCrossResponse()
        {
        }
        
        public IDictionary<string, string> Headers { get; private set; }
        public string Body { get; private set; }
        public int Status { get; private set; }

        internal static HttpCrossResponse Create(WebRequest request, WebResponse response)
        {
            using (response)
            using (var stream = response.GetResponseStream())
            using (var streamReader = new StreamReader(stream))
            {
                var httpResponse = (HttpWebResponse)response;
                var crossResponse = new HttpCrossResponse
                {
                    Body = streamReader.ReadToEnd(),
                    Headers = new Dictionary<string, string>(),
                    Status = (int)httpResponse.StatusCode
                };

                foreach (var key in response.Headers.AllKeys)
                {
                    crossResponse.Headers.Add(key, response.Headers[key]);
                }

                return crossResponse;
            }
        }
    }
}