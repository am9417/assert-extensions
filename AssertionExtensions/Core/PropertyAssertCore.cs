using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertionExtensions.Core
{
    public static class PropertyAssertCore
    {
        /// <summary>
        /// Tests whether the specified property equals with the expected value
        /// and throws an exception if the two values are not equal within a timeout.
        /// </summary>
        /// <param name="expected">The first value to compare. This is the value that the test expects.</param>
        /// <param name="propertyExpression">
        /// Expression that returns the second value when invoked. 
        /// This is the value produced by the code under test, typically an expression to get a value of a property.
        /// <para>
        /// Typically, this is a lambda: () => myObject.MyProperty
        /// </para>
        /// </param>
        /// <param name="assertEquality">If false, the assertion is inverted, i.e. the test passes if the values are not equal.</param>
        /// <param name="timeout">The timeout for the assertion in milliseconds.</param>
        /// <param name="message">
        /// The message to include in the exception when actual is not equal to expected. 
        /// The message is shown in test results.
        /// </param>
        /// <param name="parameters">An array of parameters to use when formatting message.</param>
        /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">
        /// Thrown if the <paramref name="expected"/> and the value <paramref name="propertyExpression"/> don't
        /// equal within the given <paramref name="timeout"/>
        /// </exception>
        internal static void CheckPropertyEquality<T>(T expected, Func<T> propertyExpression, bool assertEquality = true, int timeout = Constants.DEFAULT_TIMEOUT, string message = null, params object[] parameters)
        {
            var comparer = EqualityComparer<T>.Default;
            T actual = propertyExpression.Invoke();
            bool result = comparer.Equals(expected, actual);

            if (Utils.Evaluate(result, assertEquality))
            {
                return;
            }

            DateTime start = DateTime.Now;

            while (!result && (DateTime.Now - start).TotalMilliseconds < timeout)
            {
                actual = propertyExpression.Invoke();
                result = Utils.Evaluate(comparer.Equals(expected, actual), assertEquality);
                Task.Delay(timeout / 100).Wait();
            }

            if (!result)
            {
                string defaultMessage = 
                    $"Assert.Property{(assertEquality ? "" : "Not")}Equals failed. {(assertEquality ? "Not" : "")}Expected:<{expected}>. Actual:<{actual}>.";
                message = message ?? defaultMessage;
                message = parameters == null ? message : string.Format(message, parameters);

                throw new AssertFailedException(message);
            }
        }
    }
}