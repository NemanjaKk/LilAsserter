using System.Collections.Generic;

namespace LilAsserter.AsserterNemagus
{
	public interface IAsserter
	{
		Asserter Assert(bool condition, string? message = null, string? loggingDetails = null);
		Asserter AssertContinue(bool condition, string? message = null, string? loggingDetails = null);
		List<ErrorModel> GetErrorModels();
	}
}
