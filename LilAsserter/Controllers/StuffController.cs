using AsserterNemagus;
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
        _asserter
            .TrueContinue(0 == 3)
            .Message("1. Two numbers are not true.")
            .Log("Logging Default that the two numbers are not true.")
            .Assert();

        _asserter
            .TrueContinue(() =>
            {
                return 0 == 3;
            })
            .Message("2. Two numbers are not true.")
            .Log("Logging Critical that the two numbers are not true.", LogLevel.Critical)
            .Assert();

        _asserter
            .True(false)
            .Log(LogLevel.Information, "Logging Information that the two numbers are not true.")
            .Assert();

        return Ok("Stuff");
    }
}
