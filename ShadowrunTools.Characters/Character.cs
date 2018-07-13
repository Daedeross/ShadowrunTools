using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using System;

namespace ShadowrunTools.Characters
{
    public class Character: CategorizedTraitContainer, ICharacter
    {
        private readonly IRules _rules;

        public string Name { get; set; }

        public ICharacterMetatype Metatype { get; }

        public Character(IRules rules, IMetavariantPrototype metavariant)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));

            Metatype = new CharacterMetatype(metavariant);
        }

        public ITraitContainer Attributes
        {
            get
            {
                if (!TryGetValue(TraitCategories.Attribute, out ITraitContainer attributes))
                {
                    attributes = new TraitContainer<IAttribute>();
                    this[TraitCategories.Attribute] = attributes;
                }
                return attributes;
            }
        }

        internal IAttribute CreateAttribute(IAttributePrototype prototype)
        {
            var id = Guid.NewGuid();
            var container = Attributes;
            var attribute = new Traits.Attribute(id, prototype.Name, container, this, Metatype, _rules)
            {
                SubCategory = prototype.SubCategory,
                Book = prototype.Book,
                Page = prototype.Page,
                ShortName = prototype.ShortName,
            };

            return attribute;
        }

        public static ICharacter CreateFromPrototype(ICharacterPrototype characterPrototype, IMetavariantPrototype metavariant, IRules rules)
        {
            var character = new Character(rules, metavariant);

            foreach (var attributePrototype in characterPrototype.CoreAttributes)
            {
                character.Attributes[attributePrototype.Name] = character.CreateAttribute(attributePrototype);
            }

            return character;
        }
    }
}
