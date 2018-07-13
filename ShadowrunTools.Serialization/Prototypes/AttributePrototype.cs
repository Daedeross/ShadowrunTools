using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [DataContract(Name = "AttributePrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class AttributePrototype: TraitPrototypeBase, IAttributePrototype
    {
        [DataMember]
        public string ShortName { get; set; }

        public AttributePrototype()
        {
            Category = Categories.Attributes;
        }
    }
}
