using ShadowrunTools.Characters.Model;
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
        public string Category { get; private set; }
        [DataMember]
        public string SubCategory { get; set; }
        [DataMember]
        public string UserNotes { get; set; }
        [DataMember]
        public string Book { get; set; }
        [DataMember]
        public int Page { get; set; }
    }
}
