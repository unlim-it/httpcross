namespace HttpCross.Serialization
{
    public interface IContentSerializer
    {
        string ContentType { get; }
        string Serialize(object content);
        T Deserialize<T>(string content);
    }
}