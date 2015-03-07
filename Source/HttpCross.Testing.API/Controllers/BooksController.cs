namespace HttpCross.Testing.API.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BooksController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var books = new[]
            {
                "The Da Vinci Code (Dan Brown)", 
                "Pride and Prejudice (Jane Austen)",
                "To Kill A Mockingbird (Harper Lee)", 
                "Gone With The Wind (Margaret Mitchell)",
                "The Lord of the Rings: Return of the King (Tolkien)",
                "The Lord of the Rings: Fellowship of the Ring (Tolkien)",
                "The Lord of the Rings: Two Towers (Tolkien)", 
                "Anne of Green Gables (L.M. Montgomery)",
                "Outlander (Diana Gabaldon)", 
                "A Fine Balance (Rohinton Mistry)"
            };

            var response = this.Request.CreateResponse(HttpStatusCode.OK, books);

            IEnumerable<string> authHeader;
            if (this.Request.Headers.TryGetValues("x-auth", out authHeader))
            {
                response.Headers.Add("x-auth", authHeader);
            }

            return response;
        }
    }
}
