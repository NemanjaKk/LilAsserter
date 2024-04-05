namespace AsserterNemagus
{
    /// <summary>
    /// Represents an error model containing information about an error.
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// Gets or sets the message describing the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets additional details about the error, if available.
        /// </summary>
        public string? Details { get; set; }

        /// <summary>
        /// Gets or sets the stack trace associated with the error, if available.
        /// </summary>
        public string? StackTrace { get; set; }
    }
}
