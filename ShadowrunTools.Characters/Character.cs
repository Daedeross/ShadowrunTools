using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Linq;

namespace ShadowrunTools.Characters
{
    public class Character: CategorizedTraitContainer, ICharacter
    {
        private readonly ITraitFactory _traitFactory;

        public string Name { get; set; }

        public ICharacterPriorities Priorities { get; private set; }

        public ICharacterMetatype Metatype { get; private set; }

        public Character(ITraitFactory traitFactory, IMetavariantPrototype metavariant)
        {
            _traitFactory = traitFactory ?? throw new ArgumentNullException(nameof(traitFactory));

            Metatype = new CharacterMetatype(metavariant);
        }

        #region Attributes

        public ITraitContainer<IAttribute> Attributes
        {
            get
            {
                if (!TryGetValue(TraitCategories.Attribute, out ITraitContainer attributes))
                {
                    attributes = new TraitContainer<IAttribute>(TraitCategories.Attribute);
                    this[TraitCategories.Attribute] = attributes;
                }
                return attributes as ITraitContainer<IAttribute>;
            }
        }

        public void AddAttribute(IAttribute attribute)
        {
            Attributes[attribute.Name] = attribute;
        }

        internal IAttribute CreateAttribute(IAttributePrototype prototype)
        {
            return _traitFactory.CreateAttribute(this, prototype);
        }

        #endregion // Attributes

        #region Skills

        public ITraitContainer<ISkill> ActiveSkills
        {
            get
            {
                if (!TryGetValue(TraitCategories.ActiveSkill, out ITraitContainer skills))
                {
                    skills = new TraitContainer<ISkill>(TraitCategories.ActiveSkill);
                    this[TraitCategories.ActiveSkill] = skills;
                }
                return ActiveSkills as ITraitContainer<ISkill>;
            }
        }

        public void AddActiveSkill(ISkill skill)
        {
            ActiveSkills[skill.Name] = skill;
        }

        internal ISkill CreateSkill(ISkillPrototype prototype)
        {
            return _traitFactory.CreateSkill(this, prototype);
        }

        #endregion // Skills

        public static ICharacter CreateFromPrototype(
            IRules rules,
            IPrototypeRepository prototypes,
            ITraitFactory traitFactory)
        {
            var characterPrototype = CharacterPrototype.CreateFromRepository(prototypes);
            var meta = prototypes.Metavariants.First(m => m.Name == "Elf");
            var character = new Character(traitFactory, meta);

            switch (rules.GenerationMethod)
            {
                case Model.GenerationMethod.NPC:
                    throw new NotImplementedException();
                case Model.GenerationMethod.Priority:
                    character.Priorities = new CharacterPriorities(prototypes.Priorities);
                    break;
                case Model.GenerationMethod.SumToTen:
                    character.Priorities = new CharacterPointPriorities(prototypes.Priorities);
                    break;
                case Model.GenerationMethod.KarmaGen:
                    throw new NotImplementedException();
                case Model.GenerationMethod.LifeModules:
                    throw new NotImplementedException();
                case Model.GenerationMethod.BuildPoints:
                    throw new NotImplementedException();
                default:
                    break;
            }

            foreach (var attributePrototype in characterPrototype.CoreAttributes)
            {
                character.Attributes[attributePrototype.Name] = character.CreateAttribute(attributePrototype);
            }

            return character;
        }
    }
}
