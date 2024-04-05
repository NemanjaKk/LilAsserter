using Microsoft.Extensions.Logging;

namespace AsserterNemagus
{
    internal class AssertionState
    {
        public string Message { get; set; }
        public string? LoggingDetails { get; set; }
        public LogLevel? LoggingLevel { get; set; } = null;
        public bool Failed { get; set; }
        public bool IsBreaking { get; set; }
        public bool AssertionSet { get; set; } = false;
    }
}
