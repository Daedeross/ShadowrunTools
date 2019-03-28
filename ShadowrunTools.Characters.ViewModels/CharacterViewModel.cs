using GalaSoft.MvvmLight.Command;
using Moq;
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

        public CharacterViewModel(DisplaySettings displaySettings, ICharacter character)
            : base(displaySettings)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));

            _priorities = new PrioritiesViewModel(displaySettings, TestPriorities());

            InitializeAttributes();
        }

        private IPriorities TestPriorities()
        {
            var attributePoints = new[] { 12, 14, 16, 20, 24 };
            var attributes = new Dictionary<PriorityLevel, IAttributesPriority>(5);

            var metaTuples = new (string, int)[][]
            {
                new[] { ("Human", 1) },
                new[] { ("Human", 3), ("Elf", 0) },
                new[] { ("Human", 5), ("Elf", 3) },
                new[] { ("Human", 7), ("Elf", 6) },
                new[] { ("Human", 9), ("Elf", 8) },
            };
            var metas = new Dictionary<PriorityLevel, IMetatypePriority>(5);

            var specials = new Dictionary<PriorityLevel, ISpecialsPriority>(5);

            var skillPoints = new[] { (18, 0), (22, 0), (28, 2), (36, 5), (46, 10) };
            var skills = new Dictionary<PriorityLevel, ISkillsPriority>(5);

            var resourceValues = new[] { 6000m, 50000m, 140000m, 275000m, 450000m };
            var resources = new Dictionary<PriorityLevel, IResourcesPriority>(5);

            for (int i = 0; i < 5; i++)
            {
                var level = (PriorityLevel)i;

                var p = new Mock<IAttributesPriority>();
                p.SetupGet(x => x.AttibutePoints).Returns(attributePoints[i]);

                attributes[level] = p.Object;

                var metaOptions = new List<IPriorityMetavariantOption>();
                foreach (var tuple in metaTuples[i])
                {
                    var m = new Mock<IPriorityMetavariantOption>();
                    m.SetupGet(x => x.AdditionalKarmaCost).Returns(0);
                    m.SetupGet(x => x.Metavariant).Returns(tuple.Item1);
                    m.SetupGet(x => x.SpecialAttributePoints).Returns(tuple.Item2);

                    metaOptions.Add(m.Object);
                }

                var metasMock = new Mock<IMetatypePriority>();
                metasMock.SetupGet(x => x.MetavariantOptions).Returns(metaOptions);
                metas[level] = metasMock.Object;

                var specialsMock = new Mock<ISpecialsPriority>();
                specials[level] = specialsMock.Object;

                var skillsMock = new Mock<ISkillsPriority>();
                skillsMock.SetupGet(x => x.SkillPoints).Returns(skillPoints[i].Item1);
                skillsMock.SetupGet(x => x.SkillGroupPoints).Returns(skillPoints[i].Item2);
                skills[level] = skillsMock.Object;

                var resourcesMock = new Mock<IResourcesPriority>();
                resourcesMock.SetupGet(x => x.Resources).Returns(resourceValues[i]);
                resources[level] = resourcesMock.Object;
            }

            var mock = new Mock<IPriorities>();

            mock.SetupGet(x => x.Attributes)
                .Returns(attributes);
            mock.SetupGet(x => x.Metatype)
                .Returns(metas);
            mock.SetupGet(x => x.Specials)
                .Returns(specials);
            mock.SetupGet(x => x.Skills)
                .Returns(skills);
            mock.SetupGet(x => x.Resources)
                .Returns(resources);

            return mock.Object;
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
