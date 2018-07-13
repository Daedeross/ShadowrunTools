using System;

namespace ShadowrunTools.Foundation
{
    public interface IProperty
    {
        string Label { get; }
        object Value { get; set; }
        Type TypeOf { get; }
        bool Editable { get; }
    }
}
