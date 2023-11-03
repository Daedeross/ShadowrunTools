namespace ShadowrunTools.Characters.Factories
{
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Characters.Prototypes;
    using ShadowrunTools.Characters.Traits;
    using ShadowrunTools.Foundation;
    using System;
    using System.Collections.Generic;

    public class TraitFactory : ITraitFactory//, ITraitFactoryInternal
    {
        private readonly IRules _rules;
        private readonly IParserFactory _parserFactory;

        public TraitFactory(IRules rules, IParserFactory parserFactory)
        {
            Args.NotNull(rules, nameof(rules));
            _rules = rules;
            _parserFactory = parserFactory;
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
            var parser = _parserFactory.Create<ITrait>();

            var groupName = string.IsNullOrEmpty(prototype.GroupName)
                ? prototype.Name : prototype.GroupName;

            var skillGroup = GetOrCreateSkillGroup(character, groupName);

            var skill = new Skill(prototype.Id, prototype.GetHashCode(), prototype.Name, prototype.Category,
                prototype.SkillType, attribute, skillGroup, Array.Empty<Improvement>(), container, character, _rules, parser)
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

        public ISkillGroup GetOrCreateSkillGroup(ICharacter character, string groupName)
        {
            var container = character.GetOrAdd(TraitCategories.SkillGroup, () => new TraitContainer<ISkillGroup>(TraitCategories.SkillGroup));

            if (container.TryGetValue(groupName, out var trait))
            {
                if (trait is ISkillGroup group)
                {
                    return group;
                }
                else
                {
                    throw new InvalidCastException($"Trait of incorrect type in SkillGroup container. {trait.TraitType}");
                }
            }

            return CreateSkillGroup(character, container, groupName);
        }

        private ISkillGroup CreateSkillGroup(ICharacter character, ITraitContainer category, string groupName)
        {
            var parser = _parserFactory.Create<ITrait>();
            return new SkillGroup(Guid.NewGuid(), groupName.GetHashCode(), groupName, category.Name, category, character, _rules, parser);
        }
    }
}
