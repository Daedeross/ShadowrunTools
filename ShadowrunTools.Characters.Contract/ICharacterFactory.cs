using ShadowrunTools.Characters.Prototypes;

namespace ShadowrunTools.Characters
{
    public interface ICharacterFactory
    {
        ICharacter Create(IPrototypeRepository prototypes);
        ICharacter Create(IPrototypeRepository prototypes, string metavariant);
        ICharacter Create(IPrototypeRepository prototypes, ICharacterMetatype metavariant);
    }
}
