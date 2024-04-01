using System;
using System.Collections.Generic;

namespace LilAsserter.AsserterNemagus
{
    /// <summary>
    /// Represents a contract for asserting conditions.
    /// </summary>
    public interface IAsserter
    {
        /// <summary>
        /// Verifies that the expression is true.
        /// </summary>
        /// <param name="condition">The condition to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the condition is false.</exception>
        Asserter True(bool condition, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the expression is true.
        /// </summary>
        /// <param name="conditionFunc">The condition to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the condition is false.</exception>
        Asserter True(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the expression is true.
        /// </summary>
        /// <param name="condition">The condition to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter TrueContinue(bool condition, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the expression is true.
        /// </summary>
        /// <param name="conditionFunc">The condition to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter TrueContinue(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null);

        /// <summary>
        /// Verifies that the expression is false.
        /// </summary>
        /// <param name="condition">The condition to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the condition is true.</exception>
        Asserter False(bool condition, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the expression is false.
        /// </summary>
        /// <param name="conditionFunc">The condition to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the condition is true.</exception>
        Asserter False(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the expression is false.
        /// </summary>
        /// <param name="condition">The condition to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter FalseContinue(bool condition, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the expression is false.
        /// </summary>
        /// <param name="conditionFunc">The condition to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter FalseContinue(Func<bool> conditionFunc, string? message = null, string? loggingDetails = null);

        /// <summary>
        /// Verifies that the object is null.
        /// </summary>
        /// <param name="nullableObject">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the condition is not null.</exception>
        Asserter Null(object? nullableObject, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the object is null.
        /// </summary>
        /// <param name="nullableObjectFunc">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the condition is not null.</exception>
        Asserter Null(Func<object?> nullableObjectFunc, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the object is null.
        /// </summary>
        /// <param name="nullableObject">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter NullContinue(object? nullableObject, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the object is null.
        /// </summary>
        /// <param name="nullableObjectFunc">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter NullContinue(Func<object?> nullableObjectFunc, string? message = null, string? loggingDetails = null);

        /// <summary>
        /// Verifies that the object is not null.
        /// </summary>
        /// <param name="nullableObject">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the condition is null.</exception>
        Asserter NotNull(object? nullableObject, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the object is not null.
        /// </summary>
        /// <param name="nullableObjectFunc">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the condition is null.</exception>
        Asserter NotNull(Func<object?> nullableObjectFunc, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the object is not null.
        /// </summary>
        /// <param name="nullableObject">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter NotNullContinue(object? nullableObject, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the object is not null.
        /// </summary>
        /// <param name="nullableObjectFunc">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter NotNullContinue(Func<object?> nullableObjectFunc, string? message = null, string? loggingDetails = null);

        /// <summary>
        /// Verifies that first and second objects are equal.
        /// </summary>
        /// <param name="first">The object to be inspected.</param>
        /// <param name="second">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the objects are not equal.</exception>
        Asserter Equal<T>(T first, T second, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that first and second objects are equal.
        /// </summary>
        /// <param name="first">The object to be inspected.</param>
        /// <param name="second">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter EqualContinue<T>(T first, T second, string? message = null, string? loggingDetails = null);

        /// <summary>
        /// Verifies that first and second objects are not equal.
        /// </summary>
        /// <param name="first">The object to be inspected.</param>
        /// <param name="second">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the objects are equal.</exception>
        Asserter NotEqual<T>(T first, T second, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that first and second objects are not equal.
        /// </summary>
        /// <param name="first">The object to be inspected.</param>
        /// <param name="second">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter NotEqualContinue<T>(T first, T second, string? message = null, string? loggingDetails = null);

        /// <summary>
        /// Verifies that the collection is empty.
        /// </summary>
        /// <param name="collection">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the collection is not empty.</exception>
        Asserter Empty<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the collection is empty.
        /// </summary>
        /// <param name="collection">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter EmptyContinue<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the collection is not empty.
        /// </summary>
        /// <param name="collection">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        /// <exception cref="AssertException">Thrown when the collection is empty.</exception>
        Asserter NotEmpty<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null);
        /// <summary>
        /// Verifies that the collection is not empty.
        /// </summary>
        /// <param name="collection">The object to be inspected.</param>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <returns>The current instance of the <see cref="Asserter"/> class.</returns>
        Asserter NotEmptyContinue<T>(IEnumerable<T> collection, string? message = null, string? loggingDetails = null);

        /// <summary>
        /// Throws <see cref="AssertException"/>.
        /// </summary>
        /// <param name="message">Optional message to be included in case of assertion failure.</param>
        /// <param name="loggingDetails">Optional details to be included in logging.</param>
        /// <exception cref="AssertException">Thrown when the collection is empty.</exception>
        void Fail(string? message = null, string? loggingDetails = null);

        /// <summary>
        /// Gets the list of current errors.
        /// </summary>
        /// <returns>List of errors as <see cref="List{ErrorModel}"/>.</returns>
        List<ErrorModel> GetErrorModels();
    }
}
