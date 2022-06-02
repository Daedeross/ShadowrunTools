using System;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Foundation;

namespace ShadowrunTools.Characters.Factories
{
    public class TraitFactory : ITraitFactory
    {
        private IRules _rules;

        public TraitFactory(IRules rules)
        {
            Args.NotNull(rules, nameof(rules));
            _rules = rules;
        }

        public IAttribute CreateAttribute(ICharacter character, IAttributePrototype prototype)
        {
            var id = Guid.NewGuid();
            var container = character.Attributes as ITraitContainer;
            var attribute = new Traits.Attribute(id, prototype.GetHashCode(), prototype.Name, container, character, character.Metatype, _rules)
            {
                SubCategory = prototype.SubCategory,
                Book = prototype.Book,
                Page = prototype.Page,
                ShortName = prototype.ShortName,
                CustomOrder = prototype.CustomOrder,
            };

            return attribute;
        }

        public ISkill CreateSkill(ICharacter character, ISkillPrototype prototype)
        {
            throw new NotImplementedException();
        }
    }
}
