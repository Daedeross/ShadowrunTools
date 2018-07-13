using System;

namespace ShadowrunTools.Characters
{
    public class ItemChangedEventArgs: EventArgs
    {
        private readonly string[] _propertyNames;

        public ItemChangedEventArgs(string[] propertyNames)
        {
            _propertyNames = propertyNames;
        }

        public virtual string[] PropertyNames { get => _propertyNames; }
    }
}
