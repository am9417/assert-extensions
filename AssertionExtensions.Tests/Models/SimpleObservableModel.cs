using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AssertionExtensions.Tests.Models
{
    internal class SimpleObservableModel : INotifyPropertyChanged
    {
        private SimpleProperty _observableProperty;

        internal SimpleProperty ObservableProperty
        {
            get => _observableProperty;
            set
            {
                _observableProperty = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TestProperty"));
            }
        }

        internal SimpleProperty NonObservableProperty { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
