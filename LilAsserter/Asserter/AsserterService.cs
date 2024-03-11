using System.Text;

namespace LilAsserter.Asserter
{
	public class AsserterService : IAsserter
	{
        private readonly List<ErrorModel> Errors = [];
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Stream _ogStream;
        private Stream _freshStream;

        public AsserterService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void SetContext()
        {
            var originalResponseStream = _httpContextAccessor.HttpContext.Response.Body
                ?? throw new ArgumentNullException(nameof(_httpContextAccessor));
            _ogStream = originalResponseStream;
            using var freshStream = new MemoryStream();
            _freshStream = freshStream;
            _httpContextAccessor.HttpContext.Response.Body = _freshStream;
        }

        public AsserterService AssertBreak(bool condition)
		{
            Assert(condition);
            if (Errors.Count > 0)
            {
                EndRequest();
                return this;
            }
            else
            {
                return this;
            }
		}        
        
        public AsserterService Assert(bool condition)
		{
			if (!condition)
			{
				Errors.Add(new ()
				{
					Message = "Error placeholder " + Errors.Count,
					StackTrace = "Stack trace placeholder " + Errors.Count
				});
			}
			return this;
		}

		public async void EndRequest(string? body = null)
		{
            body ??= GenerateErrorMessage(Errors);

            _freshStream.Position = 0;
            _httpContextAccessor.HttpContext.Response.Body = _ogStream;

            await _httpContextAccessor.HttpContext.Response.WriteAsync(body);
            await _freshStream.CopyToAsync(_httpContextAccessor.HttpContext.Response.Body);
            await _httpContextAccessor.HttpContext.Response.Body.FlushAsync();
        }

        private static string GenerateErrorMessage(List<ErrorModel> errors)
        {
            StringBuilder errorMessageBuilder = new();
            errorMessageBuilder.AppendLine("Errors occurred:");

            foreach (var error in errors)
            {
                errorMessageBuilder.AppendLine($"Message: {error.Message}");
                errorMessageBuilder.AppendLine($"StackTrace: {error.StackTrace}");
                errorMessageBuilder.AppendLine();
            }

            return errorMessageBuilder.ToString();
        }
        public List<ErrorModel> GetErrorModels() => Errors;
	}
}
