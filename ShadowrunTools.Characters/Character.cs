using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using System;

namespace ShadowrunTools.Characters
{
    public class Character: CategorizedTraitContainer, ICharacter
    {
        private readonly ITraitFactory _traitFactory;

        public string Name { get; set; }

        public ICharacterMetatype Metatype { get; }

        public Character(ITraitFactory traitFactory, IMetavariantPrototype metavariant)
        {
            _traitFactory = traitFactory ?? throw new ArgumentNullException(nameof(traitFactory));

            Metatype = new CharacterMetatype(metavariant);
        }

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

        public static ICharacter CreateFromPrototype(ICharacterPrototype characterPrototype, IMetavariantPrototype metavariant, ITraitFactory traitFactory)
        {
            var character = new Character(traitFactory, metavariant);

            foreach (var attributePrototype in characterPrototype.CoreAttributes)
            {
                character.Attributes[attributePrototype.Name] = character.CreateAttribute(attributePrototype);
            }

            return character;
        }
    }
}
