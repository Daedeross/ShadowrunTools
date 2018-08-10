namespace ShadowrunTools.Characters.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShadowrunTools.Characters.Prototypes;
    using ShadowrunTools.Serialization;
    using ShadowrunTools.Serialization.Prototypes;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows;
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

        public ObservableCollection<CharacterViewModel> Characters { get; private set; }

        private CharacterViewModel _currentCharacter;

        public CharacterViewModel CurrentCharacter
        {
            get { return _currentCharacter; }
            set
            {
                if (value != _currentCharacter)
                {
                    _currentCharacter = value;
                    RaisePropertyChanged(nameof(CurrentCharacter));
                }
            }
        }


        public WorkspaceViewModel(DataLoader dataLoader, IRules rules)
        {
            _dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
            Characters = new ObservableCollection<CharacterViewModel>();
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
            var prototypes = Prototypes;
            var defaultMeta = prototypes.DefaultMetavariant;

            var charProto = CharacterPrototype.CreateFromRepository(prototypes);

            var meta = Prototypes.Metavariants.Find(m => m.Name == "Elf");

            var character = Character.CreateFromPrototype(charProto, meta, _rules);
            //var character = new Character(_rules, Prototypes.DefaultMetavariant);
            character.Name = "New Character";
            var viewModel = new CharacterViewModel(character);
            Characters.Add(viewModel);
            CurrentCharacter = viewModel;
        }

        #endregion
    }
}
