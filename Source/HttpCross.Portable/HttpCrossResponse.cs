namespace HttpCross
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    public class HttpCrossResponse
    {
        private HttpCrossResponse()
        {
        }
        
        public IDictionary<string, string> Headers { get; private set; }
        public string Body { get; private set; }
        public int StatusCode { get; private set; }

        internal static HttpCrossResponse Create(HttpWebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
            using (var streamReader = new StreamReader(responseStream))
            {
                var crossResponse = new HttpCrossResponse
                {
                    StatusCode = (int)response.StatusCode,
                    Body = streamReader.ReadToEnd()
                };

                if (response.SupportsHeaders)
                {
                    crossResponse.Headers = response.Headers.AllKeys
                        .Select(it => new { key = it, value = response.Headers[it] })
                        .ToDictionary(it => it.key, it => it.value);
                }
                
                return crossResponse;
            }
        }
    }
}