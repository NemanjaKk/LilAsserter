using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LilAsserter.AsserterNemagus
{
    public class AsserterExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AssertException assertException)
            {
                context.Result = new BadRequestObjectResult(assertException.ProblemDetails);
                context.ExceptionHandled = true;
            }
        }
    }
}
