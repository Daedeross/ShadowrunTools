namespace ShadowrunTools.Foundation
{
    using System;

    public class DisplayAttribute : Attribute
    {
        public string Label { get; set; }
        public bool Editable { get; set; } = false;

        public DisplayAttribute()
        { }
    }
}
