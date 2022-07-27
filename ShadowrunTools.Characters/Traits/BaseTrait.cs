namespace ShadowrunTools.Characters.Traits
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using NLog;
    using ReactiveUI;
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Foundation;

    public abstract class BaseTrait : ReactiveObject, ITrait
    {
        protected static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        protected readonly ITraitContainer mOwner;
        protected readonly ICategorizedTraitContainer mRoot;
        protected readonly IRules mRules;

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
        }

        public Guid Id { get; private set; }

        public int PrototypeHash { get; private set; }

        [Display(Label = "Name", Editable = false)]
        private string m_Name;
        public string Name
        {
            get => m_Name;
            set => this.RaiseAndSetIfChanged(ref m_Name, value);
        }

        [Display(Editable = false)]
        private string m_Category;
        public string Category
        {
            get => m_Category;
            set => this.RaiseAndSetIfChanged(ref m_Category, value);
        }

        [Display(Editable = true)]
        private string m_SubCategory;
        public string SubCategory
        {
            get => m_SubCategory;
            set => this.RaiseAndSetIfChanged(ref m_SubCategory, value);
        }

        [Display(Editable = true)]
        private string m_UserNotes;
        public string UserNotes
        {
            get => m_UserNotes;
            set => this.RaiseAndSetIfChanged(ref m_UserNotes, value);
        }

        [Display(Editable = true)]
        private string m_Book;
        public string Book
        {
            get => m_Book;
            set => this.RaiseAndSetIfChanged(ref m_Book, value);
        }

        [Display(Editable = true)]
        private int m_Page;
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
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BaseTrait()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
