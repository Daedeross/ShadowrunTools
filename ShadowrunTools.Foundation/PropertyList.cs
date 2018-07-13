using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowrunTools.Foundation
{
    public class PropertyList: Dictionary<string, IProperty>, IPropertyList
    {
        public PropertyList()
        {
        }

        public PropertyList(int capacity) : base(capacity)
        {
        }

        public PropertyList(IDictionary<string, IProperty> dictionary) : base(dictionary)
        {
        }

        public PropertyList(IEqualityComparer<string> comparer) : base(comparer)
        {
        }

        public PropertyList(IDictionary<string, IProperty> dictionary, IEqualityComparer<string> comparer) : base(dictionary, comparer)
        {
        }

        public PropertyList(int capacity, IEqualityComparer<string> comparer) : base(capacity, comparer)
        {
        }

        protected PropertyList(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
