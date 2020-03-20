using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertionExtensions.Extensions.Models
{
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

        public PropertyValue<T> IsEqualTo<T>(T value) => new PropertyValue<T>(value, this, true);

        public PropertyValue<T> IsNotEqualTo<T>(T value) => new PropertyValue<T>(value, this, false);
    }

}
