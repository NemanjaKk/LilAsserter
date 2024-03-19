using System.Text;

namespace LilAsserter.AsserterFiles;
public class AsserterService
{
    private readonly List<ErrorModel> Errors = [];
    private readonly ILogger<AsserterService>? _logger;

    public AsserterService(AsserterOptions options, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(serviceProvider);

        _logger = options.EnableLogging
            ? serviceProvider.GetService<ILogger<AsserterService>>()
                ?? throw new InvalidOperationException("ILogger service not available in the service provider")
            : null;
    }

    public AsserterService AssertBreak(bool condition, string errorLocation, string? message = null)
    {
        if (!condition)
        {
            message ??= "Error placeholder " + Errors.Count;
            _logger?.LogError(message + Environment.NewLine + errorLocation);
            Errors.Add(new()
            {
                Message = message,
                Location = errorLocation ?? "Stack trace placeholder " + Errors.Count
            });
            throw new AssertException(GenerateErrorMessage());
        }
        else
        {
            return this;
        }
    }

    public AsserterService Assert(bool condition, string errorLocation, string? message = null)
    {
        if (!condition)
        {
            message ??= "Error placeholder " + Errors.Count;
            _logger?.LogWarning(message + Environment.NewLine + errorLocation);
            Errors.Add(new()
            {
                Message = message,
                Location = errorLocation ?? "Stack trace placeholder " + Errors.Count
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
            errorMessageBuilder.AppendLine($"Location: {error.Location}");
            errorMessageBuilder.AppendLine();
        }

        return errorMessageBuilder.ToString();
    }
}
