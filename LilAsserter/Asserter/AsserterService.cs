using LilAsserter.Exceptions;
using System.Text;

namespace LilAsserter.Asserter
{
    public class AsserterService : IAsserter
    {
        private readonly List<ErrorModel> Errors = [];
        private readonly AsserterOptions _options;
        private readonly ILogger<AsserterService>? _logger;

        public AsserterService(AsserterOptions options, ILogger<AsserterService> logger)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            if (_options.EnableLogging)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }
        }

        public IAsserter AssertBreak(bool condition)
        {
            if (!condition)
            {
                _logger?.LogError("Error placeholder " + Errors.Count);
                Errors.Add(new()
                {
                    Message = "Error placeholder " + Errors.Count,
                    StackTrace = "Stack trace placeholder " + Errors.Count
                });
                throw new AssertException(GenerateErrorMessage());
            }
            else
            {
                return this;
            }
        }

        public IAsserter Assert(bool condition)
        {
            if (!condition)
            {
                _logger?.LogWarning("Error placeholder " + Errors.Count);
                Errors.Add(new()
                {
                    Message = "Error placeholder " + Errors.Count,
                    StackTrace = "Stack trace placeholder " + Errors.Count
                });
            }
            return this;
        }

        public List<ErrorModel> GetErrorModels() => Errors;

        public string GenerateErrorMessage()
        {
            StringBuilder errorMessageBuilder = new();
            errorMessageBuilder.AppendLine("Errors occurred:");

            foreach (var error in Errors)
            {
                errorMessageBuilder.AppendLine($"Message: {error.Message}");
                errorMessageBuilder.AppendLine($"StackTrace: {error.StackTrace}");
                errorMessageBuilder.AppendLine();
            }

            return errorMessageBuilder.ToString();
        }
    }
}
