using AsserterNemagus;
using Microsoft.AspNetCore.Mvc;

namespace LilAsserter.Controllers;
[ApiController]
[Route("[controller]")]
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
			.Log(LogLevel.Critical, "Logging message")
			.Assert();
		// OR
		_asserter
			.NotNull(searchTerm)
			.Log("Logging message", LogLevel.Critical)
			.Assert();

		return _blogRepository.GetBlogs(searchTerm);
    }
}
