using System;

namespace ShadowrunTools.Foundation
{
    public class DisplayAttribute : Attribute
    {
        public string Label { get; set; }
        public bool Editable { get; set; } = false;

        public DisplayAttribute()
        { }
    }
}
