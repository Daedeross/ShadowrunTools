using Moq;
using ReactiveUI;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
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
    public class CharacterViewModel: ViewModelBase
    {
        private readonly ICharacter _character;

        public CharacterViewModel(DisplaySettings displaySettings, ICharacter character, IPriorities priorities)
            : base(displaySettings)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));

            _priorities = new PrioritiesViewModel(displaySettings, priorities, character.Priorities);

            InitializeAttributes();
        }

        private void InitializeAttributes()
        {
            var attributes = _character[TraitCategories.Attribute];

            var body     = attributes["Body"];
            var agility  = attributes["Agility"];
            var reaction = attributes["Reaction"];
            var strength = attributes["Strength"];

            var willpower = attributes["Willpower"];
            var logic     = attributes["Logic"];
            var intuition = attributes["Intuition"];
            var charisma  = attributes["Charisma"];

            Body = body as IAttribute;
            Agility = agility as IAttribute;
            Reaction = reaction as IAttribute;
            Strength = strength as IAttribute;

            Willpower = willpower as IAttribute;
            Logic = logic as IAttribute;
            Intuition = intuition as IAttribute;
            Charisma = charisma as IAttribute;

            Attributes = new ObservableCollection<AttributeViewModel>
                (attributes.Values.Select(x => new AttributeViewModel(_displaySettings, x as IAttribute)));
        }

        #region Character Properties

        public string Name { get => _character.Name; set => _character.Name = value; }

        #endregion // Character Properties

        #region Core Attributes

        public ObservableCollection<AttributeViewModel> Attributes { get; set; }

        public ILeveledTrait Body { get; private set; }
        public ILeveledTrait Agility { get; private set; }
        public ILeveledTrait Reaction { get; private set; }
        public ILeveledTrait Strength { get; private set; }

        public ILeveledTrait Willpower { get; private set; }
        public ILeveledTrait Logic { get; private set; }
        public ILeveledTrait Intuition { get; private set; }
        public ILeveledTrait Charisma { get; private set; }

        #endregion // Core Attributes

        private PrioritiesViewModel _priorities;
        public PrioritiesViewModel Priorities => _priorities;

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
