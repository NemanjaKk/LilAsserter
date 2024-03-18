using LilAsserter.Asserter;
using Microsoft.AspNetCore.Mvc;

namespace LilAsserter.Controllers;
[ApiController]
[Route("[controller]")]
public class StuffController : ControllerBase
{
    private readonly AsserterService _asserterService;

    public StuffController(AsserterService asserterService)
    {
        _asserterService = asserterService;
    }

    [HttpGet(Name = "GetStuff")]
    public IActionResult Get()
    {
        _asserterService
            .Assert(false)
            .Assert(false, "Overridden message")
            .AssertBreak(false)
            .Assert(false);

        return Ok("Stuff");
    }
}
