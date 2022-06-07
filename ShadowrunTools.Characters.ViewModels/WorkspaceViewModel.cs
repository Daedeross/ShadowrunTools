namespace ShadowrunTools.Characters.ViewModels
{
    using DynamicData;
    using DynamicData.Binding;
    using ReactiveUI;
    using ShadowrunTools.Characters.Prototypes;
    using ShadowrunTools.Serialization;
    using ShadowrunTools.Serialization.Prototypes;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Windows.Input;

    public class WorkspaceViewModel : ViewModelBase, IWorkspaceViewModel
    {
        private IPrototypeRepository _prototypes;
        private readonly IDataLoader _dataLoader;
        private IRules _rules;
        private ITraitFactory _traitFactory;

        private SourceList<ICharacterViewModel> _characters;

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

        public IObservableCollection<IDocumentViewModel> Documents => throw new NotImplementedException();

        public IObservableCollection<ICharacterViewModel> Characters { get; } = new ObservableCollectionExtended<ICharacterViewModel>();


        private CharacterViewModel _currentCharacter;
        public CharacterViewModel CurrentCharacter
        {
            get => _currentCharacter;
            set => this.RaiseAndSetIfChanged(ref _currentCharacter, value);
        }

        public WorkspaceViewModel(IDataLoader dataLoader, IRules rules, DisplaySettings displaySettings)
            : base(displaySettings)
        {
            _dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
            _traitFactory = new Factories.TraitFactory(rules);

            _characters = new SourceList<ICharacterViewModel>();
            _characters.Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(Characters)
                .Subscribe()
                .DisposeWith(Disposables);
        }

        #region Commands

        private ICommand mNewCharacterCommand;

        public ICommand NewCharacterCommand
        {
            get
            {
                if (mNewCharacterCommand is null)
                {
                    mNewCharacterCommand = ReactiveCommand.Create(NewCharacterExecute);
                }
                return mNewCharacterCommand;
            }
        }

        protected virtual void NewCharacterExecute()
        {
            var prototypes = Prototypes;
            var defaultMeta = prototypes.DefaultMetavariant;

            var character = CharacterFactory.Create(_rules, prototypes, _traitFactory);
            character.Name = "New Character";
            var viewModel = new CharacterViewModel(_displaySettings, character, prototypes.Priorities);
            Characters.Add(viewModel);
            CurrentCharacter = viewModel;
        }

        #endregion
    }
}
