using ShadowrunTools.Characters.Loaders;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization
{
    [DataContract(Name = "Trait", Namespace = "http://schemas.shadowruntools.com/loaders")]
    [KnownType(typeof(AttributeLoader))]
    public abstract class TraitLoaderBase
    {
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public Guid Id { get; set; }
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public TraitType TraitType { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string SubCategory { get; set; }
        [DataMember]
        public string UserNotes { get; set; }
        [DataMember]
        public string Book { get; set; }
        [DataMember]
        public int Page { get; set; }
        [DataMember]
        public int PrototypeHash { get; set; }
    }
}
