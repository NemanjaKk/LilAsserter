namespace LilAsserter.AsserterFiles;
public class ErrorModel
{
    public string Message { get; set; }
    public string StatusCode { get; set; } = "400";
    public string StackTrace { get; set; }
}
