namespace HttpCross
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using HttpCross.Exceptions;

    internal class WebInvoker
    {
        internal static async Task<HttpCrossResponse> Invoke(HttpCrossRequest crossRequest, Action<HttpOperationException> exceptionHandler)
        {
            try
            {
                var request = WebRequest.Create(crossRequest.URL);
                request.Method = crossRequest.Method;

                foreach (var header in crossRequest.Headers)
                {
                    request.Headers[header.Key] = header.Value;
                }

                if (request.Method != "GET")
                {
                    var requestStream = await Task.Factory 
                        .FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, request);

                    using (requestStream)
                    {
                        var requestData = Encoding.UTF8.GetBytes(crossRequest.Body);
                        requestStream.Write(requestData, 0, requestData.Length);
                    }
                }

                var webResponse = Task<WebResponse>.Factory
                    .FromAsync(request.BeginGetResponse, request.EndGetResponse, request).Result;

                return HttpCrossResponse.Create((HttpWebResponse)webResponse);
            }
            catch (WebException ex)
            {
                HandleOperationException(ex, crossRequest, exceptionHandler);
            }
            catch (AggregateException ex)
            {
                var webException = ex.InnerExceptions.OfType<WebException>().FirstOrDefault();
                if (webException != null)
                {
                    HandleOperationException(webException, crossRequest, exceptionHandler);
                }
            }

            return null;
        }

        private static void HandleOperationException(WebException ex, HttpCrossRequest crossRequest, Action<HttpOperationException> exceptionHandler)
        {
            using (var response = (HttpWebResponse)ex.Response)
            {
                var crossResponse = HttpCrossResponse.Create(response);

                var operationException = new HttpOperationException(ex.Message, crossRequest, crossResponse);

                if (exceptionHandler != null)
                {
                    exceptionHandler(operationException);
                    return;
                }

                throw operationException;
            }
        }
    }
}