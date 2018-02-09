﻿namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using ShadowrunTools.Foundation;

    public abstract class BaseTrait : ITrait, INotifyItemChanged
    {
        protected ITraitContainer mOwner;
        protected ICategorizedTraitContainer mRoot;

        public BaseTrait(ITraitContainer container, ICategorizedTraitContainer root)
        {
            Args.NotNull(container, nameof(container));
            Args.NotNull(root, nameof(root));

            mOwner = container;
            mRoot = root;
        }

        public Guid Id { get; private set; }

        [Display(Label = "Name", Editable = false)]
        public string Name { get; set; }
        [Display(Editable = false)]
        public string Category { get; }
        [Display(Editable = true)]
        public string SubCategory { get; set; }
        [Display(Editable = true)]
        public string UserNotes { get; set; }
        [Display(Editable = true)]
        public string Book { get; set; }
        [Display(Editable = true)]
        public int Page { get; set; }

        public virtual IPropertyList BeginEdit()
        {
            return PropertyFactory.CreateFromObject(this, false);
        }

        public virtual bool ValidateEdit(IPropertyList newProperties)
        {
            if (newProperties.TryGetValue("Name", out IProperty pName))
            {
                var name = pName.Value as string;
                if (string.IsNullOrWhiteSpace(name))
                {
                    return false;
                }
            }
            return true;
        }

        public void CommitEdit(IPropertyList newProperties)
        {
            var (valid, names) = OnBeforeCommitEdit(newProperties);
            if (valid)
            {
                if (names.Any())
                {
                    RaiseItemChanged(names.ToArray());
                }
            }
        }

        protected virtual (bool, IEnumerable<string>) OnBeforeCommitEdit(IPropertyList properties)
        {
            var propNames = new List<string>();
            string name;
            string subCat;
            string userNotes;
            string book;
            int page;

            if (properties.TryGetValue("Name", out IProperty pName))
            {
                name = pName.Value as string;
                if (string.IsNullOrWhiteSpace(name))
                {
                    return (false, null);
                }
            }
            if (properties.TryGetValue("SubCategory", out IProperty pSubCat))
            {
                subCat = pSubCat.Value as string;
                if (!string.Equals(subCat, SubCategory))
                {
                    propNames.Add("SubCategory");
                }
            }
            if (properties.TryGetValue("UserNotes", out IProperty pUserNotes))
            {
                userNotes = pUserNotes.Value as string;
                if (!string.Equals(userNotes, UserNotes))
                {
                    propNames.Add("UserNotes");
                }
            }
            if (properties.TryGetValue("Book", out IProperty pBook))
            {
                book = pBook.Value as string;
                if (!string.Equals(book, Book))
                {
                    propNames.Add("Book");
                    Book = book;
                }
            }
            if (properties.TryGetValue("Page", out IProperty pPage))
            {
                page = (int)pPage.Value;
                if (page != Page)
                {
                    propNames.Add("Page");
                    Page = page;
                }
            }
            return (true, propNames);
        }

        public event ItemChangedEventHandler ItemChanged;

        protected void RaiseItemChanged(string[] propertyNames)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(propertyNames));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
