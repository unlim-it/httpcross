namespace HttpCross.Serialization
{
    public interface IContentSerializer
    {
        string ContentType { get; }
        byte[] Serialize(object requestBody);
        T Deserialize<T>(byte[] responseBody);
    }
}