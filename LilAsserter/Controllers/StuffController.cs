using LilAsserter.AsserterNemagus;
using Microsoft.AspNetCore.Mvc;

namespace LilAsserter.Controllers;
[ApiController]
[Route("[controller]")]
public class StuffController : ControllerBase
{
	private readonly IAsserter Asserter;

	public StuffController(IAsserter asserter)
    {
		Asserter = asserter;
	}

    [HttpGet(Name = "GetStuff")]
    public IActionResult Get()
    {
        Asserter.AssertContinue(false, "This statement is false");
        Asserter.AssertContinue(false, "Statement is false", "Very useful logging message that contains information of the highest order and stuff");
        Asserter.Assert(false, "The last error you will see...");
        Asserter.AssertContinue(false, "You won't see me");

        return Ok("Stuff");
    }
}
