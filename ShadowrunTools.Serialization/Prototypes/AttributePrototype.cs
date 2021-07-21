using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Foundation;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [DataContract(Name = "AttributePrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class AttributePrototype: LeveledTraitPrototype, IAttributePrototype
    {
        private static int? _hash;

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string CustomOrder { get; set; }

        public AttributePrototype()
        {
            Category = Categories.Attributes;
        }

        public override int GetHashCode()
        {
            if (!_hash.HasValue)
            {
                _hash = FNV1aHash.AppendHash32(base.GetHashCode(), ShortName, CustomOrder);
            }

            return _hash.Value;
        }
    }
}
