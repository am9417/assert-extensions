using System;
using AssertionExtensions.Core;

namespace AssertionExtensions.Extensions.Models
{
    public class PropertyValue<T>
    {
        private readonly T _value;
        private readonly PropertyWrapper _propertyWrapper;
        private readonly bool _assertEquality;

        internal PropertyValue(T value, PropertyWrapper propertyWrapper, bool assertEquality)
        {
            _value = value;
            _propertyWrapper = propertyWrapper;
            _assertEquality = assertEquality;
        }

        public void After(Action action)
        {
            action.Invoke();

            if (_propertyWrapper._observable)
            {
                PropertyAssertCore.CheckObservablePropertyEquality(_value, _propertyWrapper._observableObject, _propertyWrapper._propertyName, _assertEquality);
            }
            else
            {
                // TODO: This.
                PropertyAssertCore.CheckNamedPropertyEquality(_value, _propertyWrapper._object, _propertyWrapper._propertyName, assertEquality: _assertEquality);
            }

        }

        public void Now() => PropertyAssertCore.CheckNamedPropertyEquality(_value, _propertyWrapper._object, _propertyWrapper._propertyName, immediate: true, assertEquality: _assertEquality);

    }

}
