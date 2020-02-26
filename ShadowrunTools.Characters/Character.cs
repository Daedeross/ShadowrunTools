using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace ShadowrunTools.Characters
{
    public class Character: CategorizedTraitContainer, ICharacter, INotifyItemChanged
    {
        private readonly ITraitFactory _traitFactory;

        public string Name { get; set; }

        public ICharacterPriorities Priorities { get; private set; }

        public ICharacterMetatype Metatype { get; private set; }

        public Character(
            ITraitFactory traitFactory,
            ICharacterMetatype characterMetatype,
            ICharacterPriorities characterPriorities)
        {
            _traitFactory = traitFactory ?? throw new ArgumentNullException(nameof(traitFactory));

            Metatype = characterMetatype;
            Priorities = characterPriorities;
        }

        #region INotifyItemChanged

        public event EventHandler<ItemChangedEventArgs> ItemChanged;

        protected void RaiseItemChanged(params string[] propertyNames)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(propertyNames));
        }

        #endregion

        #region Attributes

        public ITraitContainer<IAttribute> Attributes
        {
            get
            {
                if (!TryGetValue(TraitCategories.Attribute, out ITraitContainer attributes))
                {
                    attributes = new TraitContainer<IAttribute>(TraitCategories.Attribute);
                    this[TraitCategories.Attribute] = attributes;
                    attributes.CollectionChanged += OnAttributesCollectionChanged;
                }
                return attributes as ITraitContainer<IAttribute>;
            }
        }

        private void OnAttributesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
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
    }
}
