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
		Asserter Null(object? nullableObject, string? message = null, string? loggingDetails = null);
		Asserter Null(Func<object?> nullableObject, string? message = null, string? loggingDetails = null);
		Asserter NullContinue(object? nullableObject, string? message = null, string? loggingDetails = null);
		Asserter NullContinue(Func<object?> nullableObject, string? message = null, string? loggingDetails = null);
		Asserter NotNull(object? nullableObject, string? message = null, string? loggingDetails = null);
		Asserter NotNull(Func<object?> nullableObject, string? message = null, string? loggingDetails = null);
		Asserter NotNullContinue(object? nullableObject, string? message = null, string? loggingDetails = null);
		Asserter NotNullContinue(Func<object?> nullableObject, string? message = null, string? loggingDetails = null);
		List<ErrorModel> GetErrorModels();
	}
}
