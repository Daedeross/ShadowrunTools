namespace ShadowrunTools.Characters.Prototypes
{
    public interface ILeveledTraitPrototype: ITraitPrototype
    {
        int Min { get; }
        int Max { get; }
    }
}
