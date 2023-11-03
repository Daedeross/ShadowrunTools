namespace ShadowrunTools.Characters.Traits
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Disposables;
    using NLog;
    using ReactiveUI;
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Foundation;

    public abstract class BaseTrait : ReactiveObject, ITrait, IDisposable
    {
        protected static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        protected readonly ITraitContainer mOwner;
        protected readonly ICategorizedTraitContainer mRoot;
        protected readonly IRules mRules;

        protected CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public BaseTrait(Guid id,
            int prototypeHash,
            string name,
            string category,
            ITraitContainer container,
            ICategorizedTraitContainer root,
            IRules rules)
        {
            Args.NotNull(category, nameof(category));
            Args.NotNull(container, nameof(container));
            Args.NotNull(root, nameof(root));

            Id = id;
            PrototypeHash = prototypeHash;
            mOwner = container;
            mRoot = root;
            Category = category;
            Name = name;
            mRules = rules;

            m_Hidden = false;
        }

        public Guid Id { get; private set; }

        public int PrototypeHash { get; private set; }

        private bool m_Hidden;
        [Display(Editable = false)]
        public virtual bool Hidden
        {
            get => m_Hidden;
            set => this.RaiseAndSetIfChanged(ref m_Hidden, value);
        }

        private string m_Name;
        [Display(Label = "Name", Editable = false)]
        public string Name
        {
            get => m_Name;
            set => this.RaiseAndSetIfChanged(ref m_Name, value);
        }

        private string m_Category;
        [Display(Editable = false)]
        public string Category
        {
            get => m_Category;
            set => this.RaiseAndSetIfChanged(ref m_Category, value);
        }

        private string m_SubCategory;
        [Display(Editable = true)]
        public string SubCategory
        {
            get => m_SubCategory;
            set => this.RaiseAndSetIfChanged(ref m_SubCategory, value);
        }

        private string m_UserNotes;
        [Display(Editable = true)]
        public string UserNotes
        {
            get => m_UserNotes;
            set => this.RaiseAndSetIfChanged(ref m_UserNotes, value);
        }

        private string m_Book;
        [Display(Editable = true)]
        public string Book
        {
            get => m_Book;
            set => this.RaiseAndSetIfChanged(ref m_Book, value);
        }

        private int m_Page;
        [Display(Editable = true)]
        public int Page
        {
            get => m_Page;
            set => this.RaiseAndSetIfChanged(ref m_Page, value);
        }

        public abstract TraitType TraitType { get; }

        public abstract bool Independant { get; }

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
                foreach (var name in names)
                {
                    this.RaisePropertyChanged(name);
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

        public bool Equals(ITrait other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other is null)
            {
                return false;
            }
            return Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as ITrait);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Disposables.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
