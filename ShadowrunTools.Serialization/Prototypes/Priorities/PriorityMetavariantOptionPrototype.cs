using ShadowrunTools.Characters.Priorities;
using System;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes.Priorities
{
    [DataContract(Name = "PriorityMetavariantOption", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class PriorityMetavariantOptionPrototype : IPriorityMetavariantOption
    {
        [DataMember]
        public string Metavariant { get; set; }
        [DataMember]
        public int SpecialAttributePoints { get; set; }
        [DataMember]
        public int AdditionalKarmaCost { get; set; }
    }
}
