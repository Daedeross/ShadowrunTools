namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Foundation;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface for sessioned editing of an object;
    /// </summary>
    public interface IEditable
    {
        /// <summary>
        /// Start an edit session
        /// </summary>
        /// <returns>The list of properties on the object that can be edited. <see cref="IPropertyList"/></returns>
        IPropertyList BeginEdit();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newProperties"></param>
        /// <returns></returns>
        bool ValidateEdit(IPropertyList newProperties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newProperties"></param>
        void CommitEdit(IPropertyList newProperties);
    }
}
