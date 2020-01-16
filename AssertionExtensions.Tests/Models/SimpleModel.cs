using System;
using System.Threading.Tasks;

namespace AssertionExtensions.Tests.Models
{
    internal class SimpleModel
    {
        internal SimpleProperty TestProperty { get; set; }

        /// <summary>
        /// Sets the property after the given number of milliseconds.
        /// </summary>
        /// <param name="newVal">New value of the property</param>
        /// <param name="millisecondsDelay">Delay before the property is set</param>
        /// <returns>The previous value of the property</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if afterMillis is not a positive integer.
        /// </exception>
        internal async Task<SimpleProperty> SetProperty(SimpleProperty newVal, int millisecondsDelay)
        {
            if (millisecondsDelay <= 0)
            {
                throw new ArgumentOutOfRangeException("millisecondsDelay", millisecondsDelay, "Delay must be a positive integer");
            }

            SimpleProperty previous = TestProperty;

            await Task.Run(async () =>
            {
                await Task.Delay(millisecondsDelay);
                TestProperty = newVal;
            });

            return previous;
        }
    }
}
