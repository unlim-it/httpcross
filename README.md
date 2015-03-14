## HttpCross ##

**HttpCross** is a simple and easy in use HTTP client for .NET solutions

### Supported Platforms ###

- .NET Framework 4+
- Windows Store
- Windows Phone 8+
- Silverlight 5
- Xamarin IOS/Android

### Downloads ###

- [HttpCross](https://www.nuget.org/packages/HttpCross/)
- [HttpCross.Extensions](https://www.nuget.org/packages/HttpCross.Extensions/)

### Simple Usage ###

**GET**

```c#
var books = await Http.New
	.WithHeader("x-auth-token", "YW55IGNhcm5hbCBwb")
	.GetJson<List<Book>>("http://localhost:5055/api/books");
```

**POST**

```c#
await Http.New
	.WithHeader("x-auth-token", "YW55IGNhcm5hbCBwb")
	.WithJsonBody(new
	{
		name = "Domain-Driven Design: Tackling Complexity in the Heart of Software",
		author = "Eric Evans",
		publication_date = new DateTime(2003, 08, 01)
	})
	.Post("http://localhost:5055/api/books");
```

**PUT**

```c#
await Http.New
	.WithHeader("x-auth-token", "YW55IGNhcm5hbCBwb")
	.WithJsonBody(new
        {
        	name = "Domain-Driven Design: Tackling Complexity in the Heart of Software",
        	author = "Eric Evans",
        	publication_date = new DateTime(2003, 08, 01),
		number_of_pages = 320
        })
	.Put("http://localhost:5055/api/books/7439");
```

**DELETE**

```c#
await Http.New
	.WithHeader("x-auth-token", "YW55IGNhcm5hbCBwb")
	.Delete("http://localhost:5055/api/books/7439");
```
