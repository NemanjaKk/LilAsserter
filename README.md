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