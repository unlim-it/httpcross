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
        internal static Task<HttpCrossResponse> Invoke(HttpCrossRequest crossRequest)
        {
            var task = Task.Factory.StartNew(() =>
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
                        var streamTask = Task.Factory.FromAsync<Stream>(
                            request.BeginGetRequestStream,
                            request.EndGetRequestStream,
                            request);

                        var requestStream = streamTask.Result;
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
                    throw PrepareOperationException(ex, crossRequest);
                }
                catch (AggregateException ex)
                {
                    var webException = ex.InnerExceptions.OfType<WebException>().FirstOrDefault();
                    if (webException != null)
                    {
                        throw PrepareOperationException(webException, crossRequest);
                    }

                    throw;
                }
            });

            return task;
        }

        private static HttpOperationException PrepareOperationException(WebException ex, HttpCrossRequest crossRequest)
        {
            HttpCrossResponse crossResponse;
            using (var response = (HttpWebResponse)ex.Response)
            {
                crossResponse = HttpCrossResponse.Create(response);
            }

            throw new HttpOperationException(ex.Message, crossRequest, crossResponse);
        }
    }
}