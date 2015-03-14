namespace HttpCross
{
    using System.Collections.Generic;

    public class HttpCrossRequest
    {
        internal HttpCrossRequest()
        {
            this.Headers = new Dictionary<string, string>();
        }
        
        public string URL { get; internal set; }

        public IDictionary<string, string> Headers { get; private set; }

        public string Body { get; internal set; }

        public string Method { get; internal set; }

        public string ContentType { get; internal set; }
    }
}