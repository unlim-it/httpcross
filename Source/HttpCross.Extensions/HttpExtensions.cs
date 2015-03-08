namespace HttpCross
{
    using System;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public static class HttpExtensions
    {
        /// <summary>
        /// Sets and serializes the body object to JSON string.
        /// </summary>
        public static Http WithJsonBody(this Http http, object content)
        {
            if (content == null)
            {
                throw new ArgumentException("Content can not be null");
            }
            
            http.WithBody(JsonConvert.SerializeObject(content), "application/json");
            return http;
        }

        public static Task<TResult> GetJson<TResult>(this Http http, string url = "")
        {
            var taskResult = http.Get(url)
                .ContinueWith(responseTask => JsonConvert.DeserializeObject<TResult>(responseTask.Result.Body));

            return taskResult;
        }

        public static Task<TResult> PostJson<TResult>(this Http http, string url = "")
        {
            var taskResult = http.Post(url)
                .ContinueWith(responseTask => JsonConvert.DeserializeObject<TResult>(responseTask.Result.Body));

            return taskResult;
        }

        public static Task<TResult> PutJson<TResult>(this Http http, string url = "")
        {
            var taskResult = http.Post(url)
                .ContinueWith(responseTask => JsonConvert.DeserializeObject<TResult>(responseTask.Result.Body));

            return taskResult;
        }
    }
}
