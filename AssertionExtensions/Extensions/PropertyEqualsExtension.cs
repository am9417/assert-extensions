using System;
using System.Collections.Generic;
using System.Text;
using AssertionExtensions.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AssertionExtensions.Extensions
{
    public static class PropertyEqualsExtension
    {
        public static void PropertyEquals<T>(this Assert a, T expected, Func<T> propertyExpression) => 
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression);

        public static void PropertyEquals<T>(this Assert a, T expected, Func<T> propertyExpression, int timeout) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, timeout: timeout);

        public static void PropertyEquals<T>(this Assert a, T expected, Func<T> propertyExpression, string message) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, message: message);

        public static void PropertyEquals<T>(this Assert a, T expected, Func<T> propertyExpression, string message, params object[] args) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, message: message, parameters: args);

        public static void PropertyEquals<T>(this Assert a, T expected, Func<T> propertyExpression, int timeout, string message) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, timeout: timeout, message: message);

        public static void PropertyEquals<T>(this Assert a, T expected, Func<T> propertyExpression, int timeout, string message, params object[] args) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, timeout: timeout, message: message, parameters: args);

    }
}
