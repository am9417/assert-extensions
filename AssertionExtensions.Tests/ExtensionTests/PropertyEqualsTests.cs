using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssertionExtensions.Extensions;
using AssertionExtensions.Tests.Models;

namespace AssertionExtensions.Tests.ExtensionTests
{
    [TestClass]
    public class PropertyEqualsTests
    {
        SimpleModel model;
        SimpleProperty clonedProperty;
        SimpleProperty newProperty;

        [TestInitialize]
        public void OnTestStartup()
        {
            model = new SimpleModel
            {
                TestProperty = new SimpleProperty()
            };

            clonedProperty = model.TestProperty.Clone();
            Task.Delay(5).Wait();
            newProperty = new SimpleProperty();
        }

        [TestMethod]
        public async Task TestPropertyEquality()
        {
            Assert.That.PropertyEquals(model.TestProperty, () => model.TestProperty);
            Assert.That.PropertyEquals(clonedProperty, () => model.TestProperty);

            var start = DateTime.Now;
            Task t = model.SetProperty(newProperty, 500);
            Assert.That.PropertyEquals(newProperty, () => model.TestProperty);
            await t;

            Assert.IsTrue((DateTime.Now - start).TotalMilliseconds > 500);
        }


        private static void Example()
        {
            System.ComponentModel.INotifyPropertyChanged o = null;
            Assert.That.Property(o, "status").IsEqualTo(32).After(() => o.GetType());
        }

        [TestMethod]
        public async Task TestPropertyEqualityWithNull()
        {
            var start = DateTime.Now;
            Task t = model.SetProperty(null, 500);
            Assert.That.PropertyEquals(null, () => model.TestProperty);
            await t;

            Assert.IsNull(model.TestProperty);
            Assert.IsTrue((DateTime.Now - start).TotalMilliseconds > 500);
        }

        [TestMethod]
        public async Task TestPropertyEqualityFail()
        {
            Task t = model.SetProperty(newProperty, 250);

            var ex = Assert.ThrowsException<AssertFailedException>(() => Assert.That.PropertyEquals(newProperty, () => model.TestProperty, 125));
            await t;

            Console.WriteLine(ex);
            Assert.That.PropertyEquals(newProperty, () => model.TestProperty, 250);

            newProperty = null;
            t = model.SetProperty(newProperty, 250);

            ex = Assert.ThrowsException<AssertFailedException>(() => Assert.That.PropertyEquals(newProperty, () => model.TestProperty, 125));
            await t;
        }

        [TestMethod]
        public void TestPropertyAssertionMessages()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => Assert.That.PropertyEquals(newProperty, () => model.TestProperty, 250));
            StringAssert.StartsWith(ex.Message, "Assert.PropertyEquals failed.");

            ex = Assert.ThrowsException<AssertFailedException>(() =>
                Assert.That.PropertyEquals(newProperty, () => model.TestProperty, 250, "Custom message"));
            Assert.AreEqual("Custom message", ex.Message);

            ex = Assert.ThrowsException<AssertFailedException>(() =>
                Assert.That.PropertyEquals(newProperty, () => model.TestProperty, 250, "Custom message {0} {1}", "param1", "param2"));

            Assert.AreEqual("Custom message param1 param2", ex.Message);
        }
    }
}
