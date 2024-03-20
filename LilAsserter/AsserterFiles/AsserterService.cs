using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LilAsserter.AsserterFiles;
public class AsserterService
{
    private readonly List<ErrorModel> Errors = [];
    private readonly ILogger<AsserterService>? _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly bool IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

    public AsserterService(AsserterOptions options, IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(httpContextAccessor);

        _httpContextAccessor = httpContextAccessor;
        _logger = options.EnableLogging
            ? serviceProvider.GetService<ILogger<AsserterService>>()
                ?? throw new InvalidOperationException(nameof(ILogger<AsserterService>) + " not available in the service provider")
            : null;
    }

    public AsserterService AssertBreak(bool condition, string errorLocation, string? message = null, string? loggingDetails = null)
    {
        if (!condition)
        {
            LogError(errorLocation, message, loggingDetails);

            Errors.Add(new()
            {
                Message = message ?? "An error occured",
                Trace = errorLocation,
                Details = loggingDetails
            });
            throw new AssertException(GenerateProblemDetails());
        }
        return this;
    }

    public AsserterService Assert(bool condition, string errorLocation, string? message = null, string? loggingDetails = null)
    {
        if (!condition)
        {
            LogWarning(errorLocation, message, loggingDetails);

            Errors.Add(new()
            {
                Message = message ?? "An error occured",
                Trace = errorLocation,
                Details = loggingDetails
            });
        }
        return this;
    }

    public List<ErrorModel> GetErrorModels() => Errors;

    private ProblemDetails GenerateProblemDetails()
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Errors occurred",
            Detail = "Errors occurred while processing the request",
            Status = (int)HttpStatusCode.BadRequest
        };

        var request = _httpContextAccessor.HttpContext?.Request;
        if (request is not null)
        {
            problemDetails.Type = request.Scheme + "://" + request.Host;
            problemDetails.Instance = request.Scheme + "://" + request.Host + request.Path + request.QueryString;
        }

        List<ErrorModel> formattedErrors = [];
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

    private void LogWarning(string errorLocation, string? message, string? loggingDetails)
    {
        Log(LogLevel.Warning, errorLocation, message, loggingDetails);
    }
    private void LogError(string errorLocation, string? message, string? loggingDetails)
    {
        Log(LogLevel.Error, errorLocation, message, loggingDetails);
    }
    private void Log(LogLevel logLevel, string errorLocation, string? message, string? loggingDetails)
    {
        string logMessage = "";
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
