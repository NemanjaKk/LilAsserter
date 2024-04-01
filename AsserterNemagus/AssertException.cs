using Microsoft.AspNetCore.Mvc;
using System;

namespace LilAsserter.AsserterNemagus
{
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
}
