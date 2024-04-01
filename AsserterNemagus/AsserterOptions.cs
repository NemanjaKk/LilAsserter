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
        /// Gets or sets a value indicating if the <see cref="AsserterExceptionFilter"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if the exception filter is enabled; otherwise, <c>false</c>. Default is <c>false</c>.</value>
        public bool EnableExceptionFilter { get; set; } = false;
    }
}
