namespace ShadowrunTools.Characters.ViewModels
{
    using ReactiveUI;
    using ShadowrunTools.Foundation;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    public class EditListViewModel: ReactiveObject
    {
        private readonly IEditable _editable;
        public ObservableCollection<KeyValuePair<string, IProperty>> Properties { get; set; }

        private bool _visible = true;
        public bool Visible
        {
            get => _visible;
            set => this.RaiseAndSetIfChanged(ref _visible, value);
        }

        private bool _valid = true;
        public bool Valid
        {
            get => _valid;
            set => this.RaiseAndSetIfChanged(ref _valid, value);
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
                    mCommitEditCommand = ReactiveCommand.Create(CommitEditExecute);
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
