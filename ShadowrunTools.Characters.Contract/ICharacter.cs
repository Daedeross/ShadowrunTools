using ShadowrunTools.Characters.Traits;

namespace ShadowrunTools.Characters
{
    public interface ICharacter: ICategorizedTraitContainer
    {
        string Name { get; set; }
        ICharacterMetatype Metatype { get; }

        ITraitContainer<IAttribute> Attributes { get; }

        void AddAttribute(IAttribute attribute);
    }
}
