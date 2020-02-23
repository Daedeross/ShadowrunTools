using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Traits;

namespace ShadowrunTools.Characters
{
    public interface ICharacter: ICategorizedTraitContainer
    {
        string Name { get; set; }

        ICharacterPriorities Priorities { get; }

        ICharacterMetatype Metatype { get; }

        ITraitContainer<IAttribute> Attributes { get; }

        ITraitContainer<ISkill> ActiveSkills { get; }

        // ITraitContainer<IQuality> Qualities { get; }

        void AddAttribute(IAttribute attribute);
    }
}
