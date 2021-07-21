using ShadowrunTools.Characters.Model;

namespace ShadowrunTools.Characters.Prototypes
{
    /// <summary>
    /// Prototype for 
    /// </summary>
    public interface ITraitPrototype : IPrototype
    {
        TraitType TraitType { get; }

        string Name { get; }

        string Category { get; }

        string SubCategory { get; }

        string Book { get; }

        int Page { get; }
    }
}
