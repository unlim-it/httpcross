namespace HttpCross.Serialization
{
    public class XmlContentSerializer : IContentSerializer
    {
        private const string ContentTypeString = "application/xml";
        
        public string ContentType { get { return ContentTypeString; }}

        public byte[] Serialize(object requestBody)
        {
            throw new System.NotImplementedException();
        }

        public T Deserialize<T>(byte[] responseBody)
        {
            throw new System.NotImplementedException();
        }
    }
}