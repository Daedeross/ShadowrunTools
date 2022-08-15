using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Foundation;
using ShadowrunTools.Serialization;

namespace ShadowrunTools.Characters.Factories
{
    public class TraitFactory : ITraitFactoryInternal
    {
        private readonly IRules _rules;

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
            var container = character.Skills as ITraitContainer;
            var attribute = character.Attributes[prototype.LinkedAttribute];
            var skill = new Skill(prototype.Id, prototype.GetHashCode(), prototype.Name, prototype.Category, prototype.SkillType, attribute, container, character, _rules)
            {
                SubCategory = prototype.SubCategory,
                Book = prototype.Book,
                Page = prototype.Page,

                GroupName = prototype.GroupName,
                AllowDefault = !prototype.TrainedOnly,
                UsualLimit = prototype.UsualLimit,
                SuggestedSpecializations = prototype.Specializations,
            };

            return skill;
        }
    }
}
