# AsserterNemagus

AsserterNemagus is a library designed to facilitate assertion and validation tasks within .NET applications. It provides an intuitive API for performing assertions on various conditions, making it easier to ensure the correctness of your code.

## Usage
1. Add the library to your project

2. Add the Asserter service to your application
```csharp
// In your Program.cs
builder.Services.AddAsserter();
// or with options
builder.Services.AddAsserter(new AsserterOptions()
{
    EnableLogging = true,
    // Add other options
});
```	 

3. (Optional) You can add the default exception filter to your controllers if you are developing a web application and want to return ProblemDetails. Otherwise you will be able to catch the AssertException and handle it yourself.
```csharp
// Program.cs
builder.Services.AddControllers(options =>
{
    options.Filters.Add<AsserterExceptionFilter>();
});
```	 

4. Inject the service into your classes
```csharp
public class BlogService
{
    private readonly IAsserter _asserter;
    private readonly IBlogRepository _blogRepository;

    public BlogService(IAsserter asserter, IBlogRepository blogRepository)
    {
        _asserter = asserter;
        _blogRepository = blogRepository;
    }

    public List<Blog> GetBlogs(string searchTerm)
    {
        _asserter
            .NotNull(searchTerm)
            .NotEmpty(searchTerm)
            .Message("Error occurred while retrieving blog posts.")
            .Log($"Error while searching blogs, {nameof(searchTerm)} must be not null or empty.")
            .Assert();

	return _blogRepository.GetBlogs(searchTerm);
    }
}
```	 

## Logging
You can enable or disable logging using the AsserterOptions.EnableLogging. If enabled, breaking assertions like .True() will log to Error level, while non breaking ones like .TrueContinue() to Warning level.
Each assertions logging default can be overridden by:
```csharp
_asserter
    .NotNull(searchTerm)
    .Log(LogLevel.Critical, "Logging message")
    .Assert();
// OR
_asserter
    .NotNull(searchTerm)
    .Log("Logging message", LogLevel.Critical)
    .Assert();
```	

## Assertion error handling
If a breaking assertion fails, a AssertException is throw. If you are using the AsserterExceptionFilter, the response will be in the form of a BadRequestObjectResult with ProblemDetails. Otherwise you can catch the exception and use the AssertException.ProblemDetails data to handle it yourself.