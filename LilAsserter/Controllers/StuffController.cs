using LilAsserter.Asserter;
using Microsoft.AspNetCore.Mvc;

namespace LilAsserter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StuffController : ControllerBase
    {
        private readonly IAsserter _asserterService;

        public StuffController(IAsserter asserterService)
        {
            _asserterService = asserterService;
        }

        [HttpGet(Name = "GetStuff")]
        public IActionResult Get()
        {
            _asserterService
                .Assert(false)
                .Assert(false)
                .AssertBreak(false)
                .Assert(false);

            return Ok("Stuff");
        }
    }
}
