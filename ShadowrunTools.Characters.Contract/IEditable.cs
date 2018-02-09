namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Foundation;
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public interface IEditable
    {
        IPropertyList BeginEdit();
        bool ValidateEdit(IPropertyList newProperties);
        void CommitEdit(IPropertyList newProperties);
    }
}
