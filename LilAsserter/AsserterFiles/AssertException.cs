using Microsoft.AspNetCore.Mvc;

namespace LilAsserter.AsserterFiles;
public class AssertException : Exception
{
    public ProblemDetails ProblemDetails { get; set; }

    public AssertException()
    {
    }

    public AssertException(ProblemDetails problemDetails)
    {
        ProblemDetails = problemDetails;
    }

    public AssertException(string message)
        : base(message)
    {
    }

    public AssertException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
