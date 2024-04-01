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

		Asserter Equal<T>(T first, T second, string? message = null, string? loggingDetails = null);
		Asserter EqualContinue<T>(T first, T second, string? message = null, string? loggingDetails = null);

		Asserter NotEqual<T>(T first, T second, string? message = null, string? loggingDetails = null);
		Asserter NotEqualContinue<T>(T first, T second, string? message = null, string? loggingDetails = null);

		Asserter Empty<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null);
		Asserter EmptyContinue<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null);
		Asserter NotEmpty<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null);
		Asserter NotEmptyContinue<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null);

		void Fail(string? message = null, string? loggingDetails = null);

		List<ErrorModel> GetErrorModels();
	}
}
