namespace LilAsserter.Asserter
{
	public class AsserterService : IAsserter
	{
		private readonly List<ErrorModel> Errors = [];

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

		public List<ErrorModel> GetErrorModels() => Errors;
	}
}
