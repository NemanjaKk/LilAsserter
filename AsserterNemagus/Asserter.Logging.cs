using Microsoft.Extensions.Logging;
using System;

namespace AsserterNemagus
{
    public partial class Asserter
    {
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
