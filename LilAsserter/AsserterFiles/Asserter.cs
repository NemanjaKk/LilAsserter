namespace LilAsserter.AsserterFiles;
public static partial class Asserter
{
    private static AsserterService _asserterService;
    public static void Initialize(AsserterService asserterService)
    {
        ArgumentNullException.ThrowIfNull(asserterService);

        _asserterService = asserterService;
    }

    public static void Assert(
        bool condition,
        string? message = null,
        string? loggingDetails = null)
    {
        _asserterService.Assert(condition, message, loggingDetails);
    }

    public static void AssertBreak(
        bool condition,
        string? message = null,
        string? loggingDetails = null)
    {
        _asserterService.AssertBreak(condition, message, loggingDetails);
    }
}
