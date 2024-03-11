using LilAsserter.Asserter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace LilAsserter;
public class ErrorFilter : IActionFilter
{
    private readonly IAsserter _asserterService;
    public ErrorFilter(IAsserter asserterService)
    {
        _asserterService = asserterService
            ?? throw new ArgumentNullException(nameof(asserterService));
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _asserterService.SetContext();
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        List<ErrorModel> errors = _asserterService.GetErrorModels();
        if (errors.Count > 0)
        {
            context.Result = new BadRequestObjectResult(GenerateErrorMessage(errors));
        }
    }

    private static string GenerateErrorMessage(List<ErrorModel> errors)
    {
        StringBuilder errorMessageBuilder = new();
        errorMessageBuilder.AppendLine("Errors occurred:");

        foreach (var error in errors)
        {
            errorMessageBuilder.AppendLine($"Message: {error.Message}");
            errorMessageBuilder.AppendLine($"StackTrace: {error.StackTrace}");
            errorMessageBuilder.AppendLine();
        }

        return errorMessageBuilder.ToString();
    }
}
