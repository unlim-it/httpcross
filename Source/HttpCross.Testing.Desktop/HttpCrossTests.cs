namespace HttpCross.Testing.Desktop
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    public class HttpCrossTests
    {
        [Test]
        public async void Test1()
        {
            const int id = 12345;

            var result = await Http.Get("http://localhost:5055/api/test/" + id).Call();
            var receivedId = Convert.ToInt32(result.Body);

            receivedId.Should().Be(id);
        }
    }
}
