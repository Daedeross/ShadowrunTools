namespace ShadowrunTools.Characters.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShadowrunTools.Characters.Prototypes;
    using ShadowrunTools.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows.Input;

    public class WorkspaceViewModel: NotificationObject
    {
        private IPrototypeRepository _prototypes;
        private readonly DataLoader _dataLoader;
        private IRules _rules;

        public IPrototypeRepository Prototypes
        {
            get
            {
                if (_prototypes == null)
                {
                    _prototypes = _dataLoader.ReloadAll();
                }
                return _prototypes;
            }
        }

        public ObservableCollection<CharacterViewModel> Characters { get; set; }

        public WorkspaceViewModel(DataLoader dataLoader, IRules rules)
        {
            _dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            _rules = rules;
        }

        #region Commands

        private ICommand mNewCharacterCommand;

        public ICommand NewCharacterCommand
        {
            get
            {
                if (mNewCharacterCommand is null)
                {
                    mNewCharacterCommand = new RelayCommand(NewCharacterExecute);
                }
                return mNewCharacterCommand;
            }
        }

        protected virtual void NewCharacterExecute()
        {
            var character = new Character(_rules, null);
            var viewModel = new CharacterViewModel(character);
            Characters.Add(viewModel);
        }

        #endregion
    }
}
