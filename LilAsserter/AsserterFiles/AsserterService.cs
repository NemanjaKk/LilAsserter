using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace LilAsserter.AsserterFiles;
public class AsserterService
{
    private readonly List<ErrorModel> Errors = [];
    private readonly ILogger<AsserterService>? _logger;

    private readonly bool IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

    public AsserterService(AsserterOptions options, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(serviceProvider);

        _logger = options.EnableLogging
            ? serviceProvider.GetService<ILogger<AsserterService>>()
                ?? throw new InvalidOperationException(nameof(ILogger<AsserterService>) + " not available in the service provider")
            : null;
    }

    public AsserterService AssertBreak(bool condition, string? message = null, string? loggingDetails = null)
    {
        if (!condition)
        {
            string fullStackTrace = new StackTrace(2, true).ToString();
            LogError(fullStackTrace, message, loggingDetails);

            Errors.Add(new()
            {
                Message = message ?? "An error occured",
                Trace = fullStackTrace,
                Details = loggingDetails
            });
            throw new AssertException(GenerateProblemDetails());
        }
        return this;
    }

    public AsserterService Assert(bool condition, string? message = null, string? loggingDetails = null)
    {
        if (!condition)
        {
            string fullStackTrace = new StackTrace(2, true).ToString();
            LogWarning(fullStackTrace, message, loggingDetails);

            Errors.Add(new()
            {
                Message = message ?? "An error occured",
                Trace = fullStackTrace,
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
