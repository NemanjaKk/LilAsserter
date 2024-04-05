using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AsserterNemagus
{
    /// <summary>
    /// Represents a contract for asserting conditions.
    /// </summary>
    public interface IAsserter
    {
        /// <summary>
        /// Asserts that the condition is true.
        /// </summary>
        Asserter True(bool condition);

        /// <summary>
        /// Asserts that the condition, evaluated by a function, is true.
        /// </summary>
        Asserter True(Func<bool> conditionFunc);

        /// <summary>
        /// Asserts that the condition is true and continues execution.
        /// </summary>
        Asserter TrueContinue(bool condition);

        /// <summary>
        /// Asserts that the condition, evaluated by a function, is true and continues execution.
        /// </summary>
        Asserter TrueContinue(Func<bool> conditionFunc);

        /// <summary>
        /// Asserts that the condition is false.
        /// </summary>
        Asserter False(bool condition);

        /// <summary>
        /// Asserts that the condition, evaluated by a function, is false.
        /// </summary>
        Asserter False(Func<bool> conditionFunc);

        /// <summary>
        /// Asserts that the condition is false and continues execution.
        /// </summary>
        Asserter FalseContinue(bool condition);

        /// <summary>
        /// Asserts that the condition, evaluated by a function, is false and continues execution.
        /// </summary>
        Asserter FalseContinue(Func<bool> conditionFunc);

        /// <summary>
        /// Asserts that the object is null.
        /// </summary>
        Asserter Null(object? nullableObject);

        /// <summary>
        /// Asserts that the object, evaluated by a function, is null.
        /// </summary>
        Asserter Null(Func<object?> nullableObjectFunc);

        /// <summary>
        /// Asserts that the object is null and continues execution.
        /// </summary>
        Asserter NullContinue(object? nullableObject);

        /// <summary>
        /// Asserts that the object, evaluated by a function, is null and continues execution.
        /// </summary>
        Asserter NullContinue(Func<object?> nullableObjectFunc);

        /// <summary>
        /// Asserts that the object is not null.
        /// </summary>
        Asserter NotNull(object? nullableObject);

        /// <summary>
        /// Asserts that the object, evaluated by a function, is not null.
        /// </summary>
        Asserter NotNull(Func<object?> nullableObjectFunc);

        /// <summary>
        /// Asserts that the object is not null and continues execution.
        /// </summary>
        Asserter NotNullContinue(object? nullableObject);

        /// <summary>
        /// Asserts that the object, evaluated by a function, is not null and continues execution.
        /// </summary>
        Asserter NotNullContinue(Func<object?> nullableObjectFunc);

        /// <summary>
        /// Asserts that two objects are equal.
        /// </summary>
        Asserter Equal<T>(T first, T second);

        /// <summary>
        /// Asserts that two objects are equal and continues execution.
        /// </summary>
        Asserter EqualContinue<T>(T first, T second);

        /// <summary>
        /// Asserts that two objects are not equal.
        /// </summary>
        Asserter NotEqual<T>(T first, T second);

        /// <summary>
        /// Asserts that two objects are not equal and continues execution.
        /// </summary>
        Asserter NotEqualContinue<T>(T first, T second);

        /// <summary>
        /// Asserts that a collection is empty.
        /// </summary>
        Asserter Empty<T>(IEnumerable<T> collection);

        /// <summary>
        /// Asserts that a collection is empty and continues execution.
        /// </summary>
        Asserter EmptyContinue<T>(IEnumerable<T> collection);

        /// <summary>
        /// Asserts that a collection is not empty.
        /// </summary>
        Asserter NotEmpty<T>(IEnumerable<T> collection);

        /// <summary>
        /// Asserts that a collection is not empty and continues execution.
        /// </summary>
        Asserter NotEmptyContinue<T>(IEnumerable<T> collection);

        /// <summary>
        /// Throws an assertion failure exception.
        /// </summary>
        /// <exception cref="AssertException"></exception>

        void Fail();

        /// <summary>
        /// Executes all assertions and throws an exception if any fail.
        /// </summary>
        /// <exception cref="AssertException"></exception>
        void Assert();

        /// <summary>
        /// Specifies a message for the next assertion.
        /// </summary>
        Asserter Message(string message);

        /// <summary>
        /// Sets the logging message with the optional log level.
        /// </summary>
        Asserter Log(string message, LogLevel? logLevel = null);

        /// <summary>
        /// Sets the logging message with the log level.
        /// </summary>
        Asserter Log(LogLevel logLevel, string message);

        /// <summary>
        /// Retrieves a list of error models generated during assertion.
        /// </summary>
        List<ErrorModel> GetErrorModels();
    }
}
