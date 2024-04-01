using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace LilAsserter.AsserterNemagus
{
    public class Asserter : IAsserter
    {
        private readonly List<ErrorModel> Errors = new List<ErrorModel>();
        private readonly ILogger<Asserter>? _logger;
        private readonly AsserterOptions _options;

        private readonly bool IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        public Asserter(IOptions<AsserterOptions> options, ILogger<Asserter> logger)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger));
			}

			_options = options.Value;
            _logger = _options.EnableLogging ? logger : null;
        }

		public Asserter True(bool condition, string? message = null, string? loggingDetails = null)
        {
            return Assert(condition, true, message, loggingDetails);
        }
        public Asserter False(bool condition, string? message = null, string? loggingDetails = null)
        {
            return Assert(!condition, true, message, loggingDetails);
        }
        public Asserter TrueContinue(bool condition, string? message = null, string? loggingDetails = null)
        {
            return Assert(condition, false, message, loggingDetails);
        }
        public Asserter FalseContinue(bool condition, string? message = null, string? loggingDetails = null)
        {
            return Assert(!condition, false, message, loggingDetails);
        }
		public Asserter True(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null)
		{
			return Assert(conditionFunc(), true, message, loggingDetails);
		}
        public Asserter False(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null)
		{
			return Assert(!conditionFunc(), true, message, loggingDetails);
		}
		public Asserter TrueContinue(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null)
		{
			return Assert(conditionFunc(), false, message, loggingDetails);
		}
		public Asserter FalseContinue(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null)
		{
			return Assert(!conditionFunc(), false, message, loggingDetails);
		}


		public Asserter Null(object? nullableObject, string? message = null, string? loggingDetails = null)
		{
			return Assert(nullableObject == null, true, message, loggingDetails);
		}

		public Asserter Null(Func<object?> nullableObject, string? message = null, string? loggingDetails = null)
		{
			return Assert(nullableObject == null, true, message, loggingDetails);
		}

		public Asserter NullContinue(object? nullableObject, string? message = null, string? loggingDetails = null)
		{
			return Assert(nullableObject == null, true, message, loggingDetails);
		}

		public Asserter NullContinue(Func<object?> nullableObject, string? message = null, string? loggingDetails = null)
		{
			return Assert(nullableObject == null, true, message, loggingDetails);
		}

		public Asserter NotNull(object? nullableObject, string? message = null, string? loggingDetails = null)
		{
			return Assert(nullableObject != null, true, message, loggingDetails);
		}

		public Asserter NotNull(Func<object?> nullableObject, string? message = null, string? loggingDetails = null)
		{
			return Assert(nullableObject != null, true, message, loggingDetails);
		}

		public Asserter NotNullContinue(object? nullableObject, string? message = null, string? loggingDetails = null)
		{
			return Assert(nullableObject != null, true, message, loggingDetails);
		}

		public Asserter NotNullContinue(Func<object?> nullableObject, string? message = null, string? loggingDetails = null)
		{
			return Assert(nullableObject != null, true, message, loggingDetails);
		}

		public Asserter Equal<T>(T first, T second, string? message = null, string? loggingDetails = null)
		{
			var areEqual = EqualityComparer<T>.Default.Equals(first, second);
			return Assert(areEqual, true, message, loggingDetails);
		}

		public Asserter EqualContinue<T>(T first, T second, string? message = null, string? loggingDetails = null)
		{
			var areEqual = EqualityComparer<T>.Default.Equals(first, second);
			return Assert(areEqual, false, message, loggingDetails);
		}

		public Asserter NotEqual<T>(T first, T second, string? message = null, string? loggingDetails = null)
		{
			var areEqual = EqualityComparer<T>.Default.Equals(first, second);
			return Assert(!areEqual, true, message, loggingDetails);
		}

		public Asserter NotEqualContinue<T>(T first, T second, string? message = null, string? loggingDetails = null)
		{
			var areEqual = EqualityComparer<T>.Default.Equals(first, second);
			return Assert(!areEqual, false, message, loggingDetails);
		}

		public List<ErrorModel> GetErrorModels() => Errors;

		private Asserter Assert(bool condition, bool isBreaking, string? message = null, string? loggingDetails = null)
        {
            if (!condition)
            {
                string fullStackTrace = new StackTrace(2, true).ToString();
                Log(isBreaking ? LogLevel.Error : LogLevel.Warning, fullStackTrace, message, loggingDetails);

                Errors.Add(new ErrorModel()
                {
                    Message = message ?? "An error occurred",
                    Trace = fullStackTrace,
                    Details = loggingDetails
                });

                if (isBreaking)
                {
                    throw new AssertException(GenerateProblemDetails());
                }
            }
            return this;
        }

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
                    Trace = IsDevelopment ? error.Trace : null
                };
                formattedErrors.Add(formattedError);
            }
            problemDetails.Extensions["errors"] = formattedErrors;

            return problemDetails;
        }

        private ProblemDetails GetBaseProblemDetails()
        {
            return new ProblemDetails()
            {
                Title = Errors.Count > 1
                    ? "Errors occurred"
                    : "Error occurred",
                Detail = Errors.Count > 1
                    ? "Errors occurred while processing the request"
                    : "Error occurred while processing the request",
                Status = (int)HttpStatusCode.BadRequest
            };
        }

        private void Log(LogLevel logLevel, string errorLocation, string? message, string? loggingDetails)
        {
            string logMessage = string.Empty;
            if (!string.IsNullOrEmpty(message))
            {
                logMessage += message + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(loggingDetails))
            {
                logMessage += loggingDetails + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorLocation))
            {
                logMessage += errorLocation + Environment.NewLine;
            }
            _logger?.Log(logLevel, logMessage);
        }

		public void Fail(string? message = null, string? loggingDetails = null)
		{
			Assert(false, true, message, loggingDetails);
		}

		public Asserter Empty<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null)
		{
			bool isEmpty = true;

			foreach (var _ in collection)
			{
				isEmpty = false;
				break;
			}

			return Assert(isEmpty, true, message, loggingDetails);
		}

		public Asserter EmptyContinue<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null)
		{
			bool isEmpty = true;

			foreach (var _ in collection)
			{
				isEmpty = false;
				break;
			}

			return Assert(isEmpty, false, message, loggingDetails);
		}

		public Asserter NotEmpty<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null)
		{
			bool isEmpty = false;

			foreach (var _ in collection)
			{
				isEmpty = true;
				break;
			}

			return Assert(isEmpty, true, message, loggingDetails);
		}

		public Asserter NotEmptyContinue<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null)
		{
			bool isEmpty = false;

			foreach (var _ in collection)
			{
				isEmpty = true;
				break;
			}

			return Assert(isEmpty, false, message, loggingDetails);
		}
	}
}
