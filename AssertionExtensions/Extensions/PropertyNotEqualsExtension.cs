using System;
using System.Collections.Generic;
using System.Text;
using AssertionExtensions.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AssertionExtensions.Extensions
{
    public static class PropertyNotEqualsExtension
    {
        public static void PropertyNotEquals<T>(this Assert a, T expected, Func<T> propertyExpression) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, false);

        public static void PropertyNotEquals<T>(this Assert a, T expected, Func<T> propertyExpression, int timeout) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, false, timeout);

        public static void PropertyNotEquals<T>(this Assert a, T expected, Func<T> propertyExpression, string message) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, false, message: message);

        public static void PropertyNotEquals<T>(this Assert a, T expected, Func<T> propertyExpression, string message, params object[] args) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, false, message: message, parameters: args);

        public static void PropertyNotEquals<T>(this Assert a, T expected, Func<T> propertyExpression, int timeout, string message) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, false, timeout, message);

        public static void PropertyNotEquals<T>(this Assert a, T expected, Func<T> propertyExpression, int timeout, string message, params object[] args) =>
            PropertyAssertCore.CheckPropertyEquality(expected, propertyExpression, false, timeout, message, args);

    }
}
