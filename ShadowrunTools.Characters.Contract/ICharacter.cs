namespace ShadowrunTools.Characters
{
    public interface ICharacter: ICategorizedTraitContainer
    {
        ICharacterMetatype Metatype { get; }
    }
}
