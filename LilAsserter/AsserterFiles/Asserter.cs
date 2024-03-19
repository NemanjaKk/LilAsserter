namespace LilAsserter.AsserterFiles;
public static class Asserter
{
    private static AsserterService _asserterService;
    public static void Initialize(AsserterService asserterService)
    {
        _asserterService = asserterService;
    }

    public static void Assert(bool condition, string? message = null)
    {
        _asserterService.Assert(condition, message);
    }

    public static void AssertBreak(bool condition, string? message = null)
    {
        _asserterService.AssertBreak(condition, message);
    }
}
