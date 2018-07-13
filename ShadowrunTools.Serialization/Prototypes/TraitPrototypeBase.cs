using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [DataContract(Name = "TraitPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public abstract class TraitPrototypeBase: ITraitPrototype
    {
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public TraitType TraitType { get; set; }
        [DataMember]
        public string Name { get; set; }
        // Set by implementing class
        public string Category { get; protected set; }
        [DataMember]
        public string SubCategory { get; set; }
        [DataMember]
        public string Book { get; set; }
        [DataMember]
        public int Page { get; set; }
    }
}
