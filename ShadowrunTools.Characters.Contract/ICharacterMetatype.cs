namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICharacterMetatype : INotifyItemChanged
    {
        string Name { get; }
        IMetatypeAttribute this[string name] { get; }
        bool TryGetAttribute(string name, out IMetatypeAttribute attribute);
    }
}
