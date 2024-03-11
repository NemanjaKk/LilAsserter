using LilAsserter.Asserter;

namespace LilAsserter
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAsserter asserterService)
        {
            asserterService.SetContext();

            await _next(context);

            List<ErrorModel> errors = asserterService.GetErrorModels();
            if (errors.Count > 0)
            {
                asserterService.EndRequest();
            }
        }
    }
}
