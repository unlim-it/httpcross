namespace HttpCross.Serialization
{
    public class FormContentSerializer : IContentSerializer
    {
        private const string ContentTypeString = "application/x-www-form-urlencoded";
        
        public string ContentType { get { return ContentTypeString; }}

        public string Serialize(object content)
        {
            throw new System.NotImplementedException();
        }

        public T Deserialize<T>(string content)
        {
            throw new System.NotImplementedException();
        }
    }
}