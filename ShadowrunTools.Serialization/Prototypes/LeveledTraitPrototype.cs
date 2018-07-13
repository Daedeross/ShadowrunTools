using ShadowrunTools.Characters.Prototypes;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [DataContract(Name = "AttributePrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class LeveledTraitPrototype: TraitPrototypeBase, ILeveledTraitPrototype
    {
    }
}
