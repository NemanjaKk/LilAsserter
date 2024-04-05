using AsserterNemagus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AsserterNemagus
{
    public partial class Asserter : IAsserter
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

        public List<ErrorModel> GetErrorModels() => Errors;

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

        public Asserter Message(string message)
        {
            State.Message = message;
            return this;
        }
    }
}
