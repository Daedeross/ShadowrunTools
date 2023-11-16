using DynamicData.Binding;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Characters.Validators;

namespace ShadowrunTools.Characters
{
    public interface ICharacter: ICategorizedTraitContainer, INamedItem
    {
        GenerationMethod GenerationMethod { get; }

        ICharacterPriorities Priorities { get; }

        ICharacterMetatype Metatype { get; }

        ISpecialChoice SpecialChoice { get; }

        ITraitContainer<IAttribute> Attributes { get; }

        ITraitContainer<ISkill> Skills { get; }

        ITraitContainer<ISkillGroup> SkillGroups { get; }

        ITraitContainer<IQuality> Qualities { get; }

        IObservableCollection<IValidatorItem> Statuses { get; }

        void AddAttribute(IAttribute attribute);
    }
}
