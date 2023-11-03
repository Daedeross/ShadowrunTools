namespace ShadowrunTools.Characters.ViewModels
{
    using DynamicData;
    using DynamicData.Binding;
    using ReactiveUI;
    using ShadowrunTools.Characters.Factories;
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
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IDataLoader _dataLoader;
        private IRules _rules;
        private readonly ITraitFactory _traitFactory;
        private readonly CharacterFactory _characterFactory;

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

        public IObservableCollection<IViewContainer> Documents { get; } = new ObservableCollectionExtended<IViewContainer>();

        public IObservableCollection<ICharacterViewModel> Characters { get; } = new ObservableCollectionExtended<ICharacterViewModel>();


        private IViewContainer _currentTab;
        public IViewContainer CurrentTab
        {
            get => _currentTab;
            set => this.RaiseAndSetIfChanged(ref _currentTab, value);
        }

        public WorkspaceViewModel(IViewModelFactory viewModelFactory,
                                  IDataLoader dataLoader,
                                  IRules rules,
                                  ITraitFactory traitFactory,
                                  DisplaySettings displaySettings)
            : base(displaySettings)
        {
            _viewModelFactory = viewModelFactory;
            _dataLoader = dataLoader;
            _rules = rules;

            // TODO: these will eventually be injected
            _traitFactory = traitFactory; //= new Factories.TraitFactory(rules);
            _characterFactory = new CharacterFactory(rules, _traitFactory);

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
                mNewCharacterCommand ??= ReactiveCommand.Create(NewCharacterExecute);
                return mNewCharacterCommand;
            }
        }

        protected virtual void NewCharacterExecute()
        {
            var prototypes = Prototypes;

            var character = _characterFactory.Create(prototypes);
            character.Name = "New Character";
            //var viewModel = new CharacterViewModel(_displaySettings, character, prototypes.Priorities);
            //var viewModel = _viewModelFactory.For<ICharacterViewModel, ICharacter>(character);
            var viewModel = _viewModelFactory.Character(character, prototypes.Priorities);
            Characters.Add(viewModel);
            var document = _viewModelFactory.CreateContainer(character.Name, viewModel, true);
            CurrentTab = document;
            Documents.Add(document);
        }

        public ICommand LoadDataFile { get; }

        public void LoadDataFileExecute()
        {

        }

        public void LoadDataFiles(string[] filesNames)
        {
            if (filesNames.Any())
            {
                _dataLoader.CurrentFiles.AddRange(filesNames);
                _dataLoader.ReloadAll();
            }
        }

        #endregion
    }
}
