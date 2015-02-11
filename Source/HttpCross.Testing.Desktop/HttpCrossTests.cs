namespace HttpCross.Testing.Desktop
{
    using System.IO;
    using NUnit.Framework;

    public class HttpCrossTests
    {
        [Test]
        public async void Test1()
        {
            var result = await Http.Get("http://google.com")
                .ExecuteResult<string>();
        }
    }
}
