using System.Text.Json.Serialization;

namespace LilAsserter.AsserterNemagus
{
    public class ErrorModel
    {
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Details { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Trace { get; set; }
    }
}
