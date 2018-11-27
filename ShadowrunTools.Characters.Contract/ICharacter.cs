using ShadowrunTools.Characters.Traits;

namespace ShadowrunTools.Characters
{
    public interface ICharacter: ICategorizedTraitContainer
    {
        string Name { get; set; }
        ICharacterMetatype Metatype { get; }

        void AddAttribute(IAttribute attribute);
    }
}
