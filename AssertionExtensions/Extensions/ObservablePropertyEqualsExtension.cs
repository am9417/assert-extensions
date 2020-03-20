using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssertionExtensions.Extensions.Models;

namespace AssertionExtensions.Extensions
{
    public static class ObservablePropertyEqualsExtension
    {
        public static PropertyWrapper Property(this Assert a, object ofObject, string propertyName) =>
            new PropertyWrapper(a, ofObject, propertyName);

        public static PropertyWrapper ObservableProperty(this Assert a, INotifyPropertyChanged ofObject, string propertyName) =>
            new PropertyWrapper(a, ofObject, propertyName);
    }

}
