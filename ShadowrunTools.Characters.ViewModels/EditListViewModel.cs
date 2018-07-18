namespace ShadowrunTools.Characters.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShadowrunTools.Foundation;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    public class EditListViewModel: NotificationObject
    {
        private readonly IEditable _editable;
        public ObservableCollection<KeyValuePair<string, IProperty>> Properties { get; set; }

        private bool _visible = true;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    RaisePropertyChanged(nameof(Visible));
                }
            }
        }

        private bool _valid = true;
        public bool Valid
        {
            get { return _valid; }
            set
            {
                if (_valid != value)
                {
                    _valid = value;
                    RaisePropertyChanged(nameof(Valid));
                }
            }
        }


        private EditListViewModel(IEditable editable, IPropertyList properties)
        {
            Properties = new ObservableCollection<KeyValuePair<string, IProperty>>(properties);
            _editable = editable ?? throw new ArgumentNullException(nameof(editable));
        }

        private ICommand mCommitEditCommand;

        public ICommand CommitEditCommand
        {
            get
            {
                if (mCommitEditCommand is null)
                {
                    mCommitEditCommand = new RelayCommand(CommitEditExecute);
                }
                return mCommitEditCommand;
            }
        }

        protected void CommitEditExecute()
        {
            var newProperties = Properties.ToDictionary(p => p.Key, p => p.Value) as IPropertyList;
            if (_editable.ValidateEdit(newProperties))
            {
                Visible = false;
                _editable.CommitEdit(newProperties);
                return;
            }

            Valid = false;
        }

        public static EditListViewModel Create(IEditable editable)
        {
            return new EditListViewModel(editable, editable.BeginEdit());
        }
    }
}
