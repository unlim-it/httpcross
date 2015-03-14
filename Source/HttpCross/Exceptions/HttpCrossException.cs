namespace HttpCross.Exceptions
{
    using System;

    public class HttpCrossException : Exception
    {
        public HttpCrossException()
        {
        }

        public HttpCrossException(string message)
            : base(message)
        {
        }

        public HttpCrossException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}