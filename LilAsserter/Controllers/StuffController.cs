using LilAsserter.AsserterNemagus;
using Microsoft.AspNetCore.Mvc;

namespace LilAsserter.Controllers;
[ApiController]
[Route("[controller]")]
public class StuffController : ControllerBase
{
	private readonly IAsserter _asserter;

	public StuffController(IAsserter asserter)
    {
		_asserter = asserter;
	}

    [HttpGet(Name = "GetStuff")]
    public IActionResult Get()
    {
        _asserter.True(() =>
            {
                var value1 = 5;
                var value2 = 15;
                return value1 == value2;
            }, 
            "This statement is false");
        _asserter.FalseContinue(true, "This statement is false");
        _asserter.TrueContinue(false, "Statement is false", "Very useful logging message that contains information of the highest order and stuff");
        _asserter.False(true, "The last error you will see...");
        _asserter.TrueContinue(false, "You won't see me");

        return Ok("Stuff");
    }
}
