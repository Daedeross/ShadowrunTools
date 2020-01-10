using GalaSoft.MvvmLight.Command;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels.Traits
{   
    public class TraitViewModelBase: ViewModelBase, ITrait
    {
        private readonly ITrait _trait;
        private static readonly ISet<string> _propertyNames;

        static TraitViewModelBase()
        {
            _propertyNames = new HashSet<string>(typeof(TraitViewModelBase)
                .GetProperties(System.Reflection.BindingFlags.Instance).Select(pi => pi.Name));
        }

        public TraitViewModelBase(DisplaySettings displaySettings, ITrait trait)
            : base(displaySettings)
        {
            _trait = trait ?? throw new ArgumentNullException(nameof(trait));
            var notify = trait as INotifyItemChanged ?? throw new ArgumentException("Trait must implement INotifyItemChanged");

            notify.ItemChanged += OnTraitChanged;
        }

        #region ITrait

        public Guid Id => _trait.Id;

        public bool Independant => _trait.Independant;

        public string Category => _trait.Category;

        public string SubCategory { get => _trait.SubCategory; set => _trait.SubCategory = value; }
        public string UserNotes { get => _trait.UserNotes; set => _trait.UserNotes = value; }

        public TraitType TraitType => _trait.TraitType;

        public string Book { get => _trait.Book; set => _trait.Book = value; }
        public int Page { get => _trait.Page; set => _trait.Page = value; }
        public string Name { get => _trait.Name; set => _trait.Name = value; }

        public virtual IPropertyList BeginEdit()
        {
            return _trait.BeginEdit();
        }

        public void CommitEdit(IPropertyList newProperties)
        {
            _trait.CommitEdit(newProperties);
        }

        public bool Equals(ITrait other)
        {
            return _trait.Equals(other);
        }

        public bool ValidateEdit(IPropertyList newProperties)
        {
            return _trait.ValidateEdit(newProperties);
        }

        #endregion

        #region Editing

        public EditListViewModel EditViewModel { get; protected set; }

        #endregion

        #region Commands

        private ICommand mBeginEditCommand;

        public ICommand BeginEditCommand
        {
            get
            {
                if (mBeginEditCommand is null)
                {
                    mBeginEditCommand = new RelayCommand(BeginEditExecute);
                }
                return mBeginEditCommand;
            }
        }

        protected virtual void BeginEditExecute()
        {
            var props = BeginEdit();
            EditViewModel = EditListViewModel.Create(this);
        }

        #endregion // Commands

        protected virtual void OnTraitChanged(object sender, ItemChangedEventArgs e)
        {
            foreach (var propName in e.PropertyNames)
            {
                if (_propertyNames.Contains(propName))
                {
                    RaisePropertyChanged(propName);
                }
            }
        }

        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (!_disposed)
                {
                    _trait.Dispose();

                    _disposed = true;
                }
            }
        }
    }
}
