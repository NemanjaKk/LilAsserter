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
        Asserter.Assert(false, "This statement is false");
        Asserter.Assert(false, "Statement is false", "Very usefull logging message that contains information of the highest order and stuff");
        Asserter.AssertBreak(false);
        Asserter.Assert(false, "You won't see me");

        return Ok("Stuff");
    }
}
