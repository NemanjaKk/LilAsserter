using AsserterNemagus;
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
        private readonly AssertionState State = new AssertionState();

        private readonly ILogger<Asserter>? _logger;
        private readonly AsserterOptions _options;

        private string DefaultErrorMessage => _options.DefaultErrorMessage;
        private string SingularErrorTitle => _options.SingularErrorTitle;
        private string MultipleErrorsTitle => _options.MultipleErrorsTitle;
        private string SingularErrorDetail => _options.SingularErrorDetail;
        private string MultipleErrorsDetail => _options.MultipleErrorsDetail;

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
            State.Message = _options.DefaultErrorMessage;
        }

        public Asserter True(bool condition)
        {
            return Assert(condition, true);
        }
        public Asserter False(bool condition)
        {
            return Assert(!condition, true);
        }
        public Asserter TrueContinue(bool condition)
        {
            return Assert(condition, false);
        }
        public Asserter FalseContinue(bool condition)
        {
            return Assert(!condition, false);
        }
        public Asserter True(Func<bool> conditionFunc)
        {
            return Assert(conditionFunc(), true);
        }
        public Asserter False(Func<bool> conditionFunc)
        {
            return Assert(!conditionFunc(), true);
        }
        public Asserter TrueContinue(Func<bool> conditionFunc)
        {
            return Assert(conditionFunc(), false);
        }
        public Asserter FalseContinue(Func<bool> conditionFunc)
        {
            return Assert(!conditionFunc(), false);
        }

        public Asserter Null(object? nullableObject)
        {
            return Assert(nullableObject == null, true);
        }

        public Asserter Null(Func<object?> nullableObject)
        {
            return Assert(nullableObject == null, true);
        }

        public Asserter NullContinue(object? nullableObject)
        {
            return Assert(nullableObject == null, true);
        }

        public Asserter NullContinue(Func<object?> nullableObject)
        {
            return Assert(nullableObject == null, true);
        }

        public Asserter NotNull(object? nullableObject)
        {
            return Assert(nullableObject != null, true);
        }

        public Asserter NotNull(Func<object?> nullableObject)
        {
            return Assert(nullableObject != null, true);
        }

        public Asserter NotNullContinue(object? nullableObject)
        {
            return Assert(nullableObject != null, true);
        }

        public Asserter NotNullContinue(Func<object?> nullableObject)
        {
            return Assert(nullableObject != null, true);
        }

        public Asserter Equal<T>(T first, T second)
        {
            var areEqual = EqualityComparer<T>.Default.Equals(first, second);
            return Assert(areEqual, true);
        }

        public Asserter EqualContinue<T>(T first, T second)
        {
            var areEqual = EqualityComparer<T>.Default.Equals(first, second);
            return Assert(areEqual, false);
        }

        public Asserter NotEqual<T>(T first, T second)
        {
            var areEqual = EqualityComparer<T>.Default.Equals(first, second);
            return Assert(!areEqual, true);
        }

        public Asserter NotEqualContinue<T>(T first, T second)
        {
            var areEqual = EqualityComparer<T>.Default.Equals(first, second);
            return Assert(!areEqual, false);
        }

        public List<ErrorModel> GetErrorModels() => Errors;

        public Asserter Empty<T>(IEnumerable<T> collection)
        {
            bool isEmpty = true;

            foreach (var _ in collection)
            {
                isEmpty = false;
                break;
            }

            return Assert(isEmpty, true);
        }

        public Asserter EmptyContinue<T>(IEnumerable<T> collection)
        {
            bool isEmpty = true;

            foreach (var _ in collection)
            {
                isEmpty = false;
                break;
            }

            return Assert(isEmpty, false);
        }

        public Asserter NotEmpty<T>(IEnumerable<T> collection)
        {
            bool isEmpty = false;

            foreach (var _ in collection)
            {
                isEmpty = true;
                break;
            }

            return Assert(isEmpty, true);
        }

        public Asserter NotEmptyContinue<T>(IEnumerable<T> collection)
        {
            bool isEmpty = false;

            foreach (var _ in collection)
            {
                isEmpty = true;
                break;
            }

            return Assert(isEmpty, false);
        }

        private Asserter Assert(bool condition, bool isBreaking)
        {
            State.Failed = !condition;
            State.IsBreaking = isBreaking;
            return this;
        }
        public Asserter Message(string message)
        {
            State.Message = message;
            return this;
        }
        public Asserter Log(string message, LogLevel? logLevel = null)
        {
            State.LoggingDetails = message;
            State.LoggingLevel = logLevel;
            return this;
        }
        public Asserter Log(LogLevel logLevel, string message)
        {
            State.LoggingDetails = message;
            State.LoggingLevel = logLevel;
            return this;
        }
        public void Assert()
        {
            if (State.Failed)
            {
                string fullStackTrace = new StackTrace(1, true).ToString();

                Log(State.IsBreaking
                        ? State.LoggingLevel ?? LogLevel.Error
                        : State.LoggingLevel ?? LogLevel.Warning,
                    fullStackTrace,
                    State.Message,
                    State.LoggingDetails);

                Errors.Add(new ErrorModel()
                {
                    Message = State.Message ?? DefaultErrorMessage,
                    StackTrace = fullStackTrace,
                    Details = State.LoggingDetails
                });

                if (State.IsBreaking)
                {
                    throw new AssertException(GenerateProblemDetails());
                }
            }
            ClearState();
        }
        public void Fail()
        {
            Assert(false, true);
            Assert();
        }

        private void ClearState()
        {
            State.IsBreaking = false;
            State.Failed = false;
            State.Message = DefaultErrorMessage;
            State.LoggingDetails = null;
            State.LoggingLevel = null;
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
    }
}
