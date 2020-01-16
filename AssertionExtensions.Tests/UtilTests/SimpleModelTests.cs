using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssertionExtensions.Tests.Models;

namespace AssertionExtensions.Tests.UtilTests
{
    [TestClass]
    public class SimpleModelTests
    {
        [TestMethod]
        public void TestModels()
        {
            var model = new SimpleModel
            {
                TestProperty = new SimpleProperty()
            };

            var clonedProperty = model.TestProperty.Clone();
            Assert.AreEqual(model.TestProperty, clonedProperty);
            Assert.AreNotSame(model.TestProperty, clonedProperty);

            var newProperty = new SimpleProperty();
            Assert.AreNotEqual(model.TestProperty, newProperty);
            Assert.AreNotSame(model.TestProperty, newProperty);
        }
    }
}
