using LilAsserter.AsserterFiles;
using Microsoft.AspNetCore.Mvc;

namespace LilAsserter.Controllers;
[ApiController]
[Route("[controller]")]
public class StuffController : ControllerBase
{
    [HttpGet(Name = "GetStuff")]
    public IActionResult Get()
    {
        Asserter.Assert(false);
        Asserter.Assert(false, "Overridden message");
        Asserter.AssertBreak(false);
        Asserter.Assert(false);

        return Ok("Stuff");
    }
}
