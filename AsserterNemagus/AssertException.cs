using Microsoft.AspNetCore.Mvc;
using System;

namespace LilAsserter.AsserterNemagus
{
    /// <summary>
    /// Represents an exception that is thrown when an assertion fails.
    /// </summary>
    public class AssertException : Exception
    {
        /// <summary>
        /// Gets or sets the details of the problem associated with the assertion failure.
        /// </summary>
        public ProblemDetails ProblemDetails { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertException"/> class with the specified problem details.
        /// </summary>
        /// <param name="problemDetails">The details of the problem associated with the assertion failure.</param>
        public AssertException(ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }
    }
}
