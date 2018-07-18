namespace ShadowrunTools.Characters.Prototypes
{
    public interface IAttributePrototype: ILeveledTraitPrototype, IOrderOverride
    {
        string ShortName { get; }
    }
}
