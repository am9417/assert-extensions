using System;
using System.Collections.Generic;
using System.Text;

namespace AssertionExtensions.Core
{
    internal static class Utils
    {
        internal static bool Evaluate(bool val, bool assertEquality) => assertEquality ? val : !val;

        internal static object GetPropertyValue(object source, string propertyName)
            => source.GetType().GetProperty(propertyName).GetValue(source, null);
    }
}
