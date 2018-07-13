using ShadowrunTools.Characters.Model;

namespace ShadowrunTools.Characters.Prototypes
{
    public interface ITraitPrototype
    {
        TraitType TraitType { get; }

        string Name { get; }

        string Category { get; }

        string SubCategory { get; }

        string Book { get; }

        int Page { get; }
    }
}
