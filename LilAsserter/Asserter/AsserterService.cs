using System.Text;

namespace LilAsserter.Asserter;
public class AsserterService
{
    private readonly List<ErrorModel> Errors = [];
    private readonly ILogger<AsserterService>? _logger;

    public AsserterService(AsserterOptions options, ILogger<AsserterService> logger)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(logger);

        _logger = options.EnableLogging
            ? logger ?? throw new ArgumentNullException(nameof(logger))
            : null;

    }

    public AsserterService AssertBreak(bool condition, string? message = null)
    {
        if (!condition)
        {
            _logger?.LogError("Error placeholder " + Errors.Count);
            Errors.Add(new()
            {
                Message = message ?? "Error placeholder " + Errors.Count,
                StackTrace = "Stack trace placeholder " + Errors.Count
            });
            throw new AssertException(GenerateErrorMessage());
        }
        else
        {
            return this;
        }
    }

    public AsserterService Assert(bool condition, string? message = null)
    {
        if (!condition)
        {
            _logger?.LogWarning("Error placeholder " + Errors.Count);
            Errors.Add(new()
            {
                Message = message ?? "Error placeholder " + Errors.Count,
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
