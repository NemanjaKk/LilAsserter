using System.Diagnostics;

namespace LilAsserter.AsserterFiles;
public static class Asserter
{
    private static AsserterService _asserterService;
    public static void Initialize(AsserterService asserterService)
    {
        _asserterService = asserterService;
    }

    public static void Assert(
        bool condition,
        string? message = null,
        [System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int callerLineNumber = 0)
    {
        _asserterService.Assert(condition, callerFilePath + " line " + callerLineNumber, message);
    }

    public static void AssertBreak(
        bool condition,
        string? message = null,
        [System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int callerLineNumber = 0)
    {
        _asserterService.AssertBreak(condition, callerFilePath + " line " + callerLineNumber, message);
    }
}
