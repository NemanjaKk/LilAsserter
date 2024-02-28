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
                .Assert(1 == 1)
                .Assert(2 == 3)
                .Assert(3 == 3);

            return Ok("Stuff");
        }
    }
}
