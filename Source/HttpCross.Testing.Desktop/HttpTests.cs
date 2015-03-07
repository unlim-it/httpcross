namespace HttpCross.Testing.Desktop
{
    using System;
    using FluentAssertions;

    using HttpCross.Exceptions;

    using NUnit.Framework;

    public class HttpTests
    {
        [Test]
        public async void Should_AddRequestHeader()
        {
            var guid = Guid.NewGuid().ToString();

            var result = await Http.New
                .WithHeader("x-auth", guid)
                .Get("http://localhost:5055/api/books");

            result.Headers["x-auth"].Should().Be(guid);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public async void Should_ThrowArgumentExceptiont_When_BodyObjectIsNull()
        {
            await Http.New
                .WithBody(null)
                .Post("http://localhost:5055/api/test");
        }

        [Test]
        public async void Should_ConcatenateBaseURLWithURLPart_When_BaseURLSet()
        {
            const int id = 12345;
            var response = await Http.New.WithBaseURL("http://localhost:5055/api/").Get("test/" + id);
            var receivedId = Convert.ToInt32(response.Body);

            receivedId.Should().Be(id);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public async void Should_ThrowArgumentException_When_PassNullOrEmptyBaseUrlParameter()
        {
            const int id = 12345;
            var response = await Http.New.WithBaseURL("").Get("test/" + id);
            var receivedId = Convert.ToInt32(response.Body);

            receivedId.Should().Be(id);
        }

        [Test]
        public void Should_CreateNewInstance_When_CallsNewProperty()
        {
            var http1 = Http.New;
            var http2 = Http.New;

            http1.Should().NotBe(http2);
        }

        [Test]
        public async void Should_GetValue_When_CalledGetMethod()
        {
            const int id = 12345;

            var result = await Http.New.Get("http://localhost:5055/api/test/" + id);
            var receivedId = Convert.ToInt32(result.Body);

            receivedId.Should().Be(id);
        }

        [Test]
        public async void Should_ReturnStatusCodeOK_When_CalledMethodExceutedSuccessfully()
        {
            const int id = 12345;

            var response = await Http.New.Get("http://localhost:5055/api/test/" + id);

            response.StatusCode.Should().Be(200);
        }

        [Test]
        public async void Should_ReturnStatusCodeNotFound_When_ResourceNotFound()
        {
            HttpOperationException exception = null;

            try
            {
                await Http.New.Get("http://localhost:5055/no_existing_path");
            }
            catch (HttpOperationException ex)
            {
                exception = ex;
            }

            exception.Should().NotBeNull();
            exception.Response.StatusCode.Should().Be(404);
        }

        [Test, ExpectedException(typeof(HttpOperationException))]
        public async void Should_ThrowCustomException_When_WebExceptionAppear()
        {
            await Http.New.Get("http://localhost:5055/not_existing_path");
        }
    }
}
