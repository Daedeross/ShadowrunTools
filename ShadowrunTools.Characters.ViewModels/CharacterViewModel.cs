using GalaSoft.MvvmLight.Command;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Characters.ViewModels.Traits;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels
{
    public class CharacterViewModel: NotificationObject
    {
        private readonly ICharacter _character;

        public CharacterViewModel(ICharacter character)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));

            InitializeAttributes();
        }

        private void InitializeAttributes()
        {
            var body = _character[TraitCategories.Attribute]["Body"];
            var agility = _character[TraitCategories.Attribute]["Agility"];
            var reaction = _character[TraitCategories.Attribute]["Reaction"];
            var strength = _character[TraitCategories.Attribute]["Strength"];

            var willpower = _character[TraitCategories.Attribute]["Willpower"];
            var logic = _character[TraitCategories.Attribute]["Logic"];
            var intuition = _character[TraitCategories.Attribute]["Intuition"];
            var charisma = _character[TraitCategories.Attribute]["Charisma"];

            Body = body as IAttribute;
            Agility = agility as IAttribute;
            Reaction = reaction as IAttribute;
            Strength = strength as IAttribute;

            Willpower = willpower as IAttribute;
            Logic = logic as IAttribute;
            Intuition = intuition as IAttribute;
            Charisma = charisma as IAttribute;

            Attributes = new ObservableCollection<LeveledTraitViewModel>
                (_character[TraitCategories.Attribute].Values.Select(x => new LeveledTraitViewModel(x as IAttribute)));
        }

        #region Character Properties

        public string Name { get => _character.Name; set => _character.Name = value; }

        #endregion // Character Properties

        #region Core Attributes

        public ObservableCollection<LeveledTraitViewModel> Attributes { get; set; }

        public ILeveledTrait Body { get; private set; }
        public ILeveledTrait Agility { get; private set; }
        public ILeveledTrait Reaction { get; private set; }
        public ILeveledTrait Strength { get; private set; }

        public ILeveledTrait Willpower { get; private set; }
        public ILeveledTrait Logic { get; private set; }
        public ILeveledTrait Intuition { get; private set; }
        public ILeveledTrait Charisma { get; private set; }

        #endregion // Core Attributes

        #region Commands

        private ICommand mAddTraitCommand;

        public ICommand AddTraitCommand
        {
            get
            {
                if (mAddTraitCommand is null)
                {
                    mAddTraitCommand = new RelayCommand<ITraitPrototype>(AddTraitExecute);
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
