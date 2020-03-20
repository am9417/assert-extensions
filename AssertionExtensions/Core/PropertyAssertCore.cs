using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                    $"Assert.Property{(assertEquality ? "" : "Not")}Equals failed. {(!assertEquality ? "Not" : "")}Expected:<{expected}>. Actual:<{actual}>.";
                message = message ?? defaultMessage;
                message = parameters == null ? message : string.Format(message, parameters);

                throw new AssertFailedException(message);
            }
        }

        /// <summary>
        /// Tests whether an observable property with given name matches the expected value
        /// and throws an exception if the two values are not equal either immediately or within a timeout.
        /// <para>
        /// The <paramref name="model"/> must raise a <see cref="PropertyChangedEventHandler"/> event 
        /// when the value changes for this method to work.
        /// </para>
        /// </summary>
        /// <param name="expected">The first value to compare. This is the value that the test expects.</param>
        /// <param name="model">The model of which property is being evaluated.</param>
        /// <param name="propertyName">Name of the property of <paramref name="model"/> to evaluate, case sensitive.</param>
        /// <param name="assertEquality">If false, the assertion is inverted, i.e. the test passes if the values are not equal.</param>
        /// <param name="timeout">The timeout for the assertion in milliseconds. Disabled if <paramref name="immediate"/> is true.</param>
        /// <param name="message">
        /// The message to include in the exception when actual is not equal to expected. 
        /// The message is shown in test results.
        /// </param>
        /// <param name="parameters">An array of parameters to use when formatting message.</param>
        /// <exception cref="AssertFailedException">
        /// Thrown if the <paramref name="expected"/> and the value of the property with name <paramref name="propertyName"/> don't
        /// equal within the given <paramref name="timeout"/> or immeadiately if <paramref name="immediate"/> is true.
        /// </exception>
        internal static void CheckObservablePropertyEquality<T>(T expected, INotifyPropertyChanged model, string propertyName,
            bool assertEquality = true, int timeout = Constants.DEFAULT_TIMEOUT, string message = null, params object[] parameters)
        {
            var comparer = EqualityComparer<T>.Default;
            bool match = false;
            T actual = actual = (T)Utils.GetPropertyValue(model, propertyName);
            
            // Immediate match, no need to wait for the event
            if (Utils.Evaluate(comparer.Equals(actual, expected), assertEquality))
            {
                match = true;
            }

            void propertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == propertyName)
                {
                    actual = (T)Utils.GetPropertyValue(model, propertyName);

                    if (Utils.Evaluate(comparer.Equals(actual, expected), assertEquality))
                    {
                        match = true;
                    }
                }
            }

            try
            {
                model.PropertyChanged += propertyChanged;

                DateTime start = DateTime.Now;

                while (!match && (DateTime.Now - start).TotalMilliseconds < timeout)
                {
                    Task.Delay(timeout / 100).Wait();
                }

                if (!match)
                {
                    string defaultMessage =
                        $"Assert.Property{(assertEquality ? "" : "Not")}Equals failed. {(!assertEquality ? "Not" : "")}Expected:<{expected}>. Actual:<{actual}>.";
                    message = message ?? defaultMessage;
                    message = parameters == null ? message : string.Format(message, parameters);

                    throw new AssertFailedException(message);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                model.PropertyChanged -= propertyChanged;
            }
        }

        /// <summary>
        /// Tests whether a property with given name matches the expected value
        /// and throws an exception if the two values are not equal either immediately or within a timeout.
        /// </summary>
        /// <param name="expected">The first value to compare. This is the value that the test expects.</param>
        /// <param name="model">The model of which property is being evaluated.</param>
        /// <param name="propertyName">Name of the property of <paramref name="model"/> to evaluate, case sensitive.</param>
        /// <param name="immediate">Whether to assert the value immediately and only once. If this is true, <paramref name="timeout"/> has no effect.</param>
        /// <param name="assertEquality">If false, the assertion is inverted, i.e. the test passes if the values are not equal.</param>
        /// <param name="timeout">The timeout for the assertion in milliseconds. Disabled if <paramref name="immediate"/> is true.</param>
        /// <param name="message">
        /// The message to include in the exception when actual is not equal to expected. 
        /// The message is shown in test results.
        /// </param>
        /// <param name="parameters">An array of parameters to use when formatting message.</param>
        /// <exception cref="AssertFailedException">
        /// Thrown if the <paramref name="expected"/> and the value of the property with name <paramref name="propertyName"/> don't
        /// equal within the given <paramref name="timeout"/> or immeadiately if <paramref name="immediate"/> is true.
        /// </exception>
        internal static void CheckNamedPropertyEquality<T>(T expected, object model, string propertyName, bool immediate = false,
            bool assertEquality = true, int timeout = Constants.DEFAULT_TIMEOUT, string message = null, params object[] parameters)
        {
            var comparer = EqualityComparer<T>.Default;
            bool result = false;
            T actual = actual = (T)Utils.GetPropertyValue(model, propertyName);

            // Immediate match, no need to wait for the event
            if (Utils.Evaluate(comparer.Equals(actual, expected), assertEquality))
            {
                result = true;
            }

            // Loop only if we don't want the result immediately
            if (!immediate)
            {
                DateTime start = DateTime.Now;

                while (!result && (DateTime.Now - start).TotalMilliseconds < timeout)
                {
                    actual = (T)Utils.GetPropertyValue(model, propertyName);
                    result = Utils.Evaluate(comparer.Equals(expected, actual), assertEquality);
                    Task.Delay(timeout / 100).Wait();
                }
            }

            if (!result)
            {
                string defaultMessage =
                    $"Assert.Property{(assertEquality ? "" : "Not")}Equals failed. {(!assertEquality ? "Not" : "")}Expected:<{expected}>. Actual:<{actual}>.";
                message = message ?? defaultMessage;
                message = parameters == null ? message : string.Format(message, parameters);

                throw new AssertFailedException(message);
            }
        }
    }
}