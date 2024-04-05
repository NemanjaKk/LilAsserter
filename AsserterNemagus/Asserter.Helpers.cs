using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace LilAsserter.AsserterNemagus
{
    public partial class Asserter
    {
        private ProblemDetails GenerateProblemDetails()
        {
            var problemDetails = GetBaseProblemDetails();

            List<ErrorModel> formattedErrors = new List<ErrorModel>();
            foreach (var error in Errors)
            {
                var formattedError = new ErrorModel
                {
                    Message = error.Message,
                    Details = IsDevelopment ? error.Details : null,
                    StackTrace = IsDevelopment ? error.StackTrace : null
                };
                formattedErrors.Add(formattedError);
            }
            problemDetails.Extensions["errors"] = formattedErrors;

            return problemDetails;
        }
        private ProblemDetails GetBaseProblemDetails()
        {
            string title = Errors.Count > 1
                ? MultipleErrorsTitle
                : SingularErrorTitle;

            string detail = Errors.Count > 1
                ? MultipleErrorsDetail
                : SingularErrorDetail;

            return new ProblemDetails()
            {
                Title = title,
                Detail = detail,
                Status = (int)HttpStatusCode.BadRequest
            };
        }
        private void ClearState()
        {
            State.AssertionSet = false;
            State.IsBreaking = false;
            State.Failed = false;
            State.Message = DefaultErrorMessage;
            State.LoggingDetails = null;
            State.LoggingLevel = null;
        }
    }
}
