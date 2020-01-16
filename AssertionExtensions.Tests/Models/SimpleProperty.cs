using System;

namespace AssertionExtensions.Tests.Models
{
    internal class SimpleProperty
    {
        private readonly string _strVal;
        private readonly long _longVal;

        public SimpleProperty()
        {
            _strVal = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.ffffff");
            _longVal = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public SimpleProperty(string strVal, long intVal)
        {
            _strVal = strVal;
            _longVal = intVal;
        }

        public override bool Equals(object obj)
        {
            return obj is SimpleProperty property &&
                   _strVal == property._strVal &&
                   _longVal == property._longVal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_strVal, _longVal);
        }

        public SimpleProperty Clone()
        {
            return new SimpleProperty(this._strVal, this._longVal);
        }

        public override string ToString()
        {
            return $"{_strVal}: {_longVal}";
        }
    }
}
