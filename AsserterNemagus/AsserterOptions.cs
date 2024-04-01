namespace LilAsserter.AsserterNemagus
{
    /// <summary>
    /// Options for configuring the Asserter service.
    /// </summary>
    public class AsserterOptions
    {
        /// <summary>
        /// Gets or sets a value indicating if logging is enabled for the Asserter service.
        /// </summary>
        /// <value><c>true</c> if logging is enabled; otherwise, <c>false</c>. Default is <c>false</c>.</value>
		public bool EnableLogging { get; set; } = false;

        /// <summary>
        /// Gets or sets the default title for a singular error.
        /// </summary>
        /// <value>Default is "An error occurred".</value>
        public string SingularErrorTitle { get; set; } = "An error occurred";

        /// <summary>
        /// Gets or sets the default title for multiple errors.
        /// </summary>
        /// <value>Default is "Multiple errors occurred".</value>
        public string MultipleErrorsTitle { get; set; } = "Multiple errors occurred";

        /// <summary>
        /// Gets or sets the default detail for a singular error.
        /// </summary>
        /// <value>Default is "An error occurred while processing the request".</value>
        public string SingularErrorDetail { get; set; } = "An error occurred while processing the request";

        /// <summary>
        /// Gets or sets the default detail for multiple errors.
        /// </summary>
        /// <value>Default is "Multiple errors occurred while processing the request".</value>
        public string MultipleErrorsDetail { get; set; } = "Multiple errors occurred while processing the request";

        /// <summary>
        /// Gets or sets the default error message used when no specific message is provided.
        /// </summary>
        /// <value>Default is "An error occurred".</value>
        public string DefaultErrorMessage { get; set; } = "An error occurred";
    }
}
