namespace ShadowrunTools.Serialization.Prototypes
{
    using ShadowrunTools.Characters.Model;
    using System.Runtime.Serialization;

    [DataContract(Name = "TraitPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class TraitPrototypeBase
    {
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
