using LilAsserter.Asserter;
using System.Text;

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
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();

            context.Response.Body = responseBody;

            await _next(context);

            List<ErrorModel> errors = asserterService.GetErrorModels();
            if (errors.Count > 0)
            {
                context.Response.Clear();

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                StringBuilder errorMessageBuilder = new();
                errorMessageBuilder.AppendLine("Errors occurred:");

                foreach (var error in errors)
                {
                    errorMessageBuilder.AppendLine($"Message: {error.Message}");
                    errorMessageBuilder.AppendLine($"StackTrace: {error.StackTrace}");
                    errorMessageBuilder.AppendLine();
                }

                string responseContent = errorMessageBuilder.ToString();
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseContent);
                await context.Response.Body.WriteAsync(responseBytes);

                await context.Response.Body.FlushAsync();

                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);

                return;
            }

            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
