using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Characters.Validators;
using System.Collections.ObjectModel;

namespace ShadowrunTools.Characters
{
    public interface ICharacter: ICategorizedTraitContainer
    {
        string Name { get; set; }

        GenerationMethod GenerationMethod { get; }

        ICharacterPriorities Priorities { get; }

        ICharacterMetatype Metatype { get; }

        ISpecialChoice SpecialChoice { get; }

        ITraitContainer<IAttribute> Attributes { get; }

        ITraitContainer<ISkill> ActiveSkills { get; }

        ObservableCollection<IValidatorItem> Statuses { get; }

        // ITraitContainer<IQuality> Qualities { get; }

        void AddAttribute(IAttribute attribute);
    }
}
