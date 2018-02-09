using System;
using System.ComponentModel;

namespace ShadowrunTools.Foundation
{
    public class Property : IProperty, INotifyPropertyChanged
    {
        public string Label { get; private set; }

        private object mValue;
        public object Value
        {
            get { return mValue; }
            set
            {
                if (!Equals(mValue, value))
                {
                    mValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                }
            }
        }

        public Type TypeOf { get; private set; }

        public bool Editable { get; private set; }

        public Property(string label, object value, Type type, bool editable)
        {
            Label = label;
            mValue = value;
            TypeOf = type;
            Editable = editable;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
