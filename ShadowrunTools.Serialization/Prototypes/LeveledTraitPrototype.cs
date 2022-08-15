using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Foundation;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [DataContract(Name = "LeveledTraitPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class LeveledTraitPrototype: TraitPrototypeBase, ILeveledTraitPrototype
    {
    }
}
