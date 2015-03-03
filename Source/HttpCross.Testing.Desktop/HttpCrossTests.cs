namespace HttpCross.Testing.Desktop
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    public class HttpCrossTests
    {
        [Test]
        public async void Should_GetValue_When_CalledGetMethod()
        {
            const int id = 12345;

            var result = await Http.Get("http://localhost:5055/api/test/" + id).Call();
            var receivedId = Convert.ToInt32(result.Body);

            receivedId.Should().Be(id);
        }

        [Test]
        public async void Should_ReturnStatusCodeOK_When_CalledMethodExceutedSuccessfully()
        {
            const int id = 12345;

            var response = await Http.Get("http://localhost:5055/api/test/" + id).Call();

            response.StatusCode.Should().Be(200);
        }

        [Test]
        public async void Should_ReturnStatusCodeNotFound_When_ResourceNotFound()
        {
            HttpCrossException exception = null;

            try
            {
                await Http.Get("http://localhost:5055/no_existing_path").Call();
            }
            catch (HttpCrossException ex)
            {
                exception = ex;
            }

            exception.Should().NotBeNull();
            exception.Response.StatusCode.Should().Be(404);
        }

        [Test, ExpectedException(typeof(HttpCrossException))]
        public async void Should_ThrowCustomException_When_WebExceptionAppear()
        {
            await Http.Get("http://localhost:5055/not_existing_path").Call();
        }
    }
}
