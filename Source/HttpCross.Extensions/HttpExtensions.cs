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

        public static async Task<TResult> GetJson<TResult>(this Http http, string url = "")
        {
            var response = await http.Get(url);
            return JsonConvert.DeserializeObject<TResult>(response.Body);
        }

        public static async Task<TResult> PostJson<TResult>(this Http http, string url = "")
        {
            var response = await http.Post(url);
            return JsonConvert.DeserializeObject<TResult>(response.Body);
        }

        public async static Task<TResult> PutJson<TResult>(this Http http, string url = "")
        {
            var response = await http.Post(url);
            return JsonConvert.DeserializeObject<TResult>(response.Body);
        }

        public async static Task<TResult> DeleteJson<TResult>(this Http http, string url = "")
        {
            var response = await http.Post(url);
            return JsonConvert.DeserializeObject<TResult>(response.Body);
        }
    }
}
