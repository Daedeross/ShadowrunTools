using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Characters.Validators;
using ShadowrunTools.Characters.ViewModels.Traits;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels
{
    public class CharacterViewModel: ViewModelBase, ICharacterViewModel
    {
        private readonly ICharacter _character;
        private readonly IViewModelFactory _viewModelFactory;

        public CharacterViewModel(DisplaySettings displaySettings, ICharacter character, IViewModelFactory viewModelFactory)
            : base(displaySettings)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            Priorities = _viewModelFactory.For<IPrioritiesViewModel, ICharacter>(_character);
            Common = _viewModelFactory.For<ICommonViewModel, ICharacter>(_character);
            Skills = _viewModelFactory.For<ICharacterSkillsViewModel, ICharacter>(_character);

            Statuses = new ObservableCollection<ValidatorItemViewModel>(_character.Statuses.Select(
                item => new ValidatorItemViewModel(displaySettings, item)));
            _character.Statuses.CollectionChanged += OnStatusesChanged;

        }

        #region Character Properties

        public string Name { get => _character.Name; set => _character.Name = value; }

        #endregion // Character Properties

        #region Child ViewModels

        public IPrioritiesViewModel Priorities { get; private set; }

        public ICommonViewModel Common { get; private set; }

        public ICharacterSkillsViewModel Skills { get; private set; }

        #endregion // Child ViewModels

        #region Status

        public ObservableCollection<ValidatorItemViewModel> Statuses { get; protected set; }

        private void OnStatusesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var oldItem in Statuses)
            {
                oldItem.Dispose();
            }

            Statuses = new ObservableCollection<ValidatorItemViewModel>(_character.Statuses.Select(
                   item => new ValidatorItemViewModel(_displaySettings, item)));
        }

        #endregion

        #region Commands

        private ICommand mAddTraitCommand;

        public ICommand AddTraitCommand
        {
            get
            {
                if (mAddTraitCommand is null)
                {
                    mAddTraitCommand = ReactiveCommand.Create<ITraitPrototype>(AddTraitExecute);
                }
                return mAddTraitCommand;
            }
        }

        private void AddTraitExecute(ITraitPrototype prototype)
        {
            
        }

        #endregion // Commands
    }
}
