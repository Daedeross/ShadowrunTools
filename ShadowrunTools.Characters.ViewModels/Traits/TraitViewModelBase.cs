using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels.Traits
{   
    public class TraitViewModelBase: NotificationObject, ITrait
    {
        private readonly ITrait _trait;



        public TraitViewModelBase(ITrait trait)
        {
            _trait = trait ?? throw new ArgumentNullException(nameof(trait));
        }

        public Guid Id => _trait.Id;

        public bool Independant => _trait.Independant;

        public string Category => _trait.Category;

        public string SubCategory { get => _trait.SubCategory; set => _trait.SubCategory = value; }
        public string UserNotes { get => _trait.UserNotes; set => _trait.UserNotes = value; }

        public TraitType TraitType => _trait.TraitType;

        public string Book { get => _trait.Book; set => _trait.Book = value; }
        public int Page { get => _trait.Page; set => _trait.Page = value; }
        public string Name { get => _trait.Name; set => _trait.Name = value; }

        public IPropertyList BeginEdit()
        {
            return _trait.BeginEdit();
        }

        public void CommitEdit(IPropertyList newProperties)
        {
            _trait.CommitEdit(newProperties);
        }

        public void Dispose()
        {
            _trait.Dispose();
        }

        public bool Equals(ITrait other)
        {
            return _trait.Equals(other);
        }

        public bool ValidateEdit(IPropertyList newProperties)
        {
            return _trait.ValidateEdit(newProperties);
        }

        #region Commands



        #endregion // Commands
    }
}
