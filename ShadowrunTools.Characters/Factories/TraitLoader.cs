using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowrunTools.Characters.Factories
{
    public class TraitLoader : ITraitLoader
    {
        private readonly IPrototypeRepository _prototypeRepository;
        private readonly IRules _rules;

        public TraitLoader(IPrototypeRepository prototypeRepository, IRules rules)
        {
            _prototypeRepository = prototypeRepository;
            _rules = rules;
        }

        #region In

        public IAttribute FromDto(ICharacter owner, AttributeDto dto)
        {
            if (!owner.TryGetValue(TraitCategories.Attribute, out var container))

            {
                throw new ArgumentOutOfRangeException(nameof(owner), "Character missing Attribute category.");
            }

            var attribute = new Traits.Attribute(dto.Id, dto.PrototypeHash, dto.Name, container, owner, owner.Metatype, _rules);
            FillOutBase(attribute, dto);

            attribute.BaseIncrease = dto.BaseIncrease;
            attribute.Improvement = dto.Improvement;
            attribute.CustomOrder = dto.CustomOrder;

            return attribute;
        }

        public ICharacterMetatype FromDto(CharacterMetatypeDto dto)
        {
            var prototype = _prototypeRepository.Metavariants.FirstOrDefault(m => string.Equals(dto.Name, m.Name));
            if (prototype is null)
            {
                throw new ArgumentException(nameof(dto.Name), $"No metavariant found with name {dto.Name}");
            }

            return new CharacterMetatype(prototype);
        }

        public ICharacterPriorities FromDto(GenerationMethod method, CharacterPrioritiesDto dto)
        {
            switch (_rules.GenerationMethod)
            {
                case GenerationMethod.NPC:
                    throw new NotImplementedException();
                case GenerationMethod.Priority:
                    return new CharacterPriorities(_prototypeRepository.Priorities)
                    {
                        AttributePriority = dto.AttributePriority,
                        MetatypePriority = dto.MetatypePriority,
                        ResourcePriority = dto.ResourcePriority,
                        SkillPriority = dto.SkillPriority,
                        SpecialPriority = dto.SpecialPriority,
                    };
                case GenerationMethod.SumToTen:
                    return new CharacterPointPriorities(_prototypeRepository.Priorities)
                    {
                        AttributePriority = dto.AttributePriority,
                        MetatypePriority = dto.MetatypePriority,
                        ResourcePriority = dto.ResourcePriority,
                        SkillPriority = dto.SkillPriority,
                        SpecialPriority = dto.SpecialPriority,
                    };
                case GenerationMethod.KarmaGen:
                    throw new NotImplementedException();
                case GenerationMethod.LifeModules:
                    throw new NotImplementedException();
                case GenerationMethod.BuildPoints:
                    throw new NotImplementedException();
                default:
                    throw new InvalidOperationException();
            }
        }

        public ISpecialChoice FromDto(SpecialChoiceDto dto)
        {
            throw new NotImplementedException();
        }

        public ISpecialSkillChoice FromDto(SpecialSkillChoiceDto dto)
        {
            throw new NotImplementedException();
        }

        public IQuality FromDto(QualityDto dto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Out

        public AttributeDto ToDto(IAttribute attribute)
        {
            return new AttributeDto
            {
                Id = attribute.Id,
                TraitType = TraitType.Attribute,
                Name = attribute.Name,
                Category = attribute.Category,
                SubCategory = attribute.SubCategory,
                UserNotes = attribute.UserNotes,
                Book = attribute.Book,
                Page = attribute.Page,
                //PrototypeHash = attribute.
                BaseIncrease = attribute.BaseIncrease,
                Improvement = attribute.Improvement,
                CustomOrder = attribute.CustomOrder
            };
        }

        public CharacterMetatypeDto ToDto(ICharacterMetatype metatype)
        {
            return new CharacterMetatypeDto
            {
                Name = metatype.Name
            };
        }

        public CharacterPrioritiesDto ToDto(ICharacterPriorities priorities)
        {
            return new CharacterPrioritiesDto
            {
                MetatypePriority = priorities.MetatypePriority,
                AttributePriority = priorities.AttributePriority,
                SpecialPriority = priorities.SpecialPriority,
                SkillPriority = priorities.SkillPriority,
                ResourcePriority = priorities.ResourcePriority
            };
        }

        public SpecialChoiceDto ToDto(ISpecialChoice specialChoice)
        {
            throw new NotImplementedException();
        }

        public SpecialSkillChoiceDto ToDto(ISpecialSkillChoice specialSkillChoice)
        {
            throw new NotImplementedException();
        }

        public QualityDto ToDto(IQuality quality)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Helpers

        private static void FillOutBase(BaseTrait target, TraitDtoBase source)
        {
            target.Category = source.Category;
            target.SubCategory = source.SubCategory;
            target.UserNotes = source.UserNotes;
            target.Book = source.Book;
            target.Page = source.Page;
        }

        #endregion
    }
}
