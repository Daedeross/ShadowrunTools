namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
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
