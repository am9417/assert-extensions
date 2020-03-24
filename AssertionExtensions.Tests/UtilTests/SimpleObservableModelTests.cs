using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using AssertionExtensions.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertionExtensions.Tests.UtilTests
{
    [TestClass]
    public class SimpleObservableModelTests
    {
        SimpleObservableModel model;

        [TestInitialize]
        public void TestInit()
        {
            model = new SimpleObservableModel();
        }

        [TestMethod]
        public void TestNotify()
        {
            bool eventOccurred = false;
            model.ObservableProperty = new SimpleProperty("val", 1);

            void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "TestProperty")
                {
                    eventOccurred = true;
                }
            }

            model.PropertyChanged += Model_PropertyChanged;
            model.ObservableProperty = new SimpleProperty("new_value", 2);

            try
            {
                Task.Delay(125).Wait();
                Assert.IsTrue(eventOccurred);

                eventOccurred = false;
                model.NonObservableProperty = new SimpleProperty("val2", 3);

                Task.Delay(125).Wait();
                Assert.IsFalse(eventOccurred);

                // Copy the value; same value, event not raised
                model.ObservableProperty = model.ObservableProperty.Clone();
                Task.Delay(125).Wait();

                Assert.IsFalse(eventOccurred);

                model.ObservableProperty = new SimpleProperty();
                Task.Delay(125).Wait();

                Assert.IsTrue(eventOccurred);
            }
            finally
            {
                model.PropertyChanged -= Model_PropertyChanged;
            }
        }

    }
}
