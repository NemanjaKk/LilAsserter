using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace LilAsserter.AsserterFiles;
public class AsserterService
{
    private readonly List<ErrorModel> Errors = [];
    private readonly ILogger<AsserterService>? _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AsserterService(AsserterOptions options, IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(httpContextAccessor);

        _httpContextAccessor = httpContextAccessor;
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
            throw new AssertException(GenerateProblemDetails());
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

    public ProblemDetails GenerateProblemDetails()
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Errors occurred",
            Detail = "Errors occurred while processing the request.",
            Status = (int)HttpStatusCode.BadRequest
        };

        var request = _httpContextAccessor.HttpContext?.Request;
        if (request is not null)
        {
            problemDetails.Type = request.Scheme + "://" + request.Host;
            problemDetails.Instance = request.Scheme + "://" + request.Host + request.Path + request.QueryString;
        }

        var errorList = new List<object>();
        foreach (var error in Errors)
        {
            var errorDetail = new
            {
                Message = error.Message,
                Trace = error.Location
            };
            errorList.Add(errorDetail);
        }
        problemDetails.Extensions["errors"] = errorList;

        return problemDetails;
    }
}
