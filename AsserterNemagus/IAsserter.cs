using System;
using System.Collections.Generic;

namespace LilAsserter.AsserterNemagus
{
	public interface IAsserter
	{
		Asserter True(bool condition, string? message = null, string? loggingDetails = null);
		Asserter True(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null);
		Asserter TrueContinue(bool condition, string? message = null, string? loggingDetails = null);
		Asserter TrueContinue(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null);
		Asserter False(bool condition, string? message = null, string? loggingDetails = null);
		Asserter False(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null);
		Asserter FalseContinue(bool condition, string? message = null, string? loggingDetails = null);
		Asserter FalseContinue(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null);
		List<ErrorModel> GetErrorModels();
	}
}
