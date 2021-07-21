using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Foundation;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [DataContract(Name = "AttributePrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class LeveledTraitPrototype: TraitPrototypeBase, ILeveledTraitPrototype
    {
        private static int? _hash;

        public int Min { get; set; }
        public int Max { get; set; }

        public override int GetHashCode()
        {
            if (!_hash.HasValue)
            {
                _hash = FNV1aHash.AppendHash32(base.GetHashCode(), Min, Max);
            }

            return _hash.Value;
        }
    }
}
