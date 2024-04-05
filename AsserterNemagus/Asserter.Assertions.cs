using System;
using System.Collections.Generic;

namespace LilAsserter.AsserterNemagus
{
    public partial class Asserter
    {
        public Asserter True(bool condition)
        {
            return Assert(condition, true);
        }
        public Asserter False(bool condition)
        {
            return Assert(!condition, true);
        }
        public Asserter TrueContinue(bool condition)
        {
            return Assert(condition, false);
        }
        public Asserter FalseContinue(bool condition)
        {
            return Assert(!condition, false);
        }
        public Asserter True(Func<bool> conditionFunc)
        {
            return Assert(conditionFunc(), true);
        }
        public Asserter False(Func<bool> conditionFunc)
        {
            return Assert(!conditionFunc(), true);
        }
        public Asserter TrueContinue(Func<bool> conditionFunc)
        {
            return Assert(conditionFunc(), false);
        }
        public Asserter FalseContinue(Func<bool> conditionFunc)
        {
            return Assert(!conditionFunc(), false);
        }
        public Asserter Null(object? nullableObject)
        {
            return Assert(nullableObject == null, true);
        }
        public Asserter Null(Func<object?> nullableObject)
        {
            return Assert(nullableObject == null, true);
        }
        public Asserter NullContinue(object? nullableObject)
        {
            return Assert(nullableObject == null, true);
        }
        public Asserter NullContinue(Func<object?> nullableObject)
        {
            return Assert(nullableObject == null, true);
        }
        public Asserter NotNull(object? nullableObject)
        {
            return Assert(nullableObject != null, true);
        }
        public Asserter NotNull(Func<object?> nullableObject)
        {
            return Assert(nullableObject != null, true);
        }
        public Asserter NotNullContinue(object? nullableObject)
        {
            return Assert(nullableObject != null, true);
        }
        public Asserter NotNullContinue(Func<object?> nullableObject)
        {
            return Assert(nullableObject != null, true);
        }
        public Asserter Equal<T>(T first, T second)
        {
            var areEqual = EqualityComparer<T>.Default.Equals(first, second);
            return Assert(areEqual, true);
        }
        public Asserter EqualContinue<T>(T first, T second)
        {
            var areEqual = EqualityComparer<T>.Default.Equals(first, second);
            return Assert(areEqual, false);
        }
        public Asserter NotEqual<T>(T first, T second)
        {
            var areEqual = EqualityComparer<T>.Default.Equals(first, second);
            return Assert(!areEqual, true);
        }
        public Asserter NotEqualContinue<T>(T first, T second)
        {
            var areEqual = EqualityComparer<T>.Default.Equals(first, second);
            return Assert(!areEqual, false);
        }
        public Asserter Empty<T>(IEnumerable<T> collection)
        {
            bool isEmpty = true;

            foreach (var _ in collection)
            {
                isEmpty = false;
                break;
            }

            return Assert(isEmpty, true);
        }
        public Asserter EmptyContinue<T>(IEnumerable<T> collection)
        {
            bool isEmpty = true;

            foreach (var _ in collection)
            {
                isEmpty = false;
                break;
            }

            return Assert(isEmpty, false);
        }
        public Asserter NotEmpty<T>(IEnumerable<T> collection)
        {
            bool isEmpty = false;

            foreach (var _ in collection)
            {
                isEmpty = true;
                break;
            }

            return Assert(isEmpty, true);
        }
        public Asserter NotEmptyContinue<T>(IEnumerable<T> collection)
        {
            bool isEmpty = false;

            foreach (var _ in collection)
            {
                isEmpty = true;
                break;
            }

            return Assert(isEmpty, false);
        }

        private Asserter Assert(bool condition, bool isBreaking)
        {
            if (State.AssertionSet)
            {
                throw new InvalidOperationException("Can not have multiple assertions at once.");
            }

            State.AssertionSet = true;
            State.Failed = !condition;
            State.IsBreaking = isBreaking;
            return this;
        }
    }
}
