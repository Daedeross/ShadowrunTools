namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Prototypes;

    public interface ITraitFromLoaderFactory<TTrait, TPrototype>
        where TTrait: class
        where TPrototype: class

    {
        TTrait ToClass(ICharacter character, IPrototypeRepository prototypes);
    }
}
