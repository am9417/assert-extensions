using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using AssertionExtensions.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertionExtensions.Extensions
{
    public static class ObservablePropertyEqualsExtension
    {
        public static PropertyWrapper Property(this Assert a, object ofObject, string propertyName) =>
            new PropertyWrapper(a, ofObject, propertyName);

        public static PropertyWrapper ObservableProperty(this Assert a, INotifyPropertyChanged ofObject, string propertyName) =>
            new PropertyWrapper(a, ofObject, propertyName);
    }

    public class PropertyWrapper
    {
        internal readonly Assert _assert;
        internal readonly INotifyPropertyChanged _observableObject;
        internal readonly object _object;
        internal readonly string _propertyName;
        internal readonly bool _observable;

        internal PropertyWrapper(Assert a, object ofObject, string propertyName)
        {
            _assert = a;
            _object = ofObject;
            _propertyName = propertyName;
            _observable = false;
        }

        internal PropertyWrapper(Assert a, INotifyPropertyChanged ofObject, string propertyName)
        {
            _assert = a;
            _observableObject = ofObject;
            _propertyName = propertyName;
            _observable = true;
        }

        public PropertyValue<T> IsEqualTo<T>(T value) => new PropertyValue<T>(value, this);
    }

    public class PropertyValue<T>
    {
        private readonly T _value;
        private readonly PropertyWrapper _propertyWrapper;

        internal PropertyValue(T value, PropertyWrapper propertyWrapper)
        {
            _value = value;
            _propertyWrapper = propertyWrapper;
        }

        public void After(Action action)
        {
            action.Invoke();

            if (_propertyWrapper._observable)
            {
                PropertyAssertCore.CheckObservablePropertyEquality(_value, _propertyWrapper._observableObject, _propertyWrapper._propertyName);
            }
            else
            {
                // TODO: This.
                // PropertyAssertCore.CheckPropertyEquality(_value, _propertyWrapper._object, _propertyWrapper._propertyName);
            }

        }

        public void Now() =>
            throw new NotImplementedException();
    }

}
