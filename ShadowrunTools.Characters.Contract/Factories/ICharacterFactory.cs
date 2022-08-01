using ShadowrunTools.Characters.Prototypes;

namespace ShadowrunTools.Characters.Factories
{
    public interface ICharacterFactory: IFactory
    {
        ICharacter Create(IPrototypeRepository prototypes);
        ICharacter Create(IPrototypeRepository prototypes, string metavariant);
        ICharacter Create(IPrototypeRepository prototypes, ICharacterMetatype metavariant);
    }
}
