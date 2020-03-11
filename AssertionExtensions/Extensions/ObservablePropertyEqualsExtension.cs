using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertionExtensions.Extensions
{
    public static class ObservablePropertyEqualsExtension
    {
        public static PropertyWrapper Property(this Assert a, object ofObject, string propertyName) =>
            throw new NotImplementedException();

        public static PropertyWrapper ObservableProperty(this Assert a, INotifyPropertyChanged ofObject, string propertyName) =>
            throw new NotImplementedException();
    }

    public class PropertyWrapper
    {
        public PropertyValue<T> IsEqualTo<T>(T value) =>
            throw new NotImplementedException();
    }

    public class PropertyValue<T>
    {
        public void After(Action action) =>
            throw new NotImplementedException();
        public void Now() =>
            throw new NotImplementedException();
    }

}
