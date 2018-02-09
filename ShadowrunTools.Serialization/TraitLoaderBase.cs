namespace ShadowrunTools.Serialization
{
    using System;
    using System.Runtime.Serialization;

    [DataContract(Name = "Trait")]
    public class TraitLoaderBase
    {
        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public Guid Id { get; set; }

        [DataMember(IsRequired = true, EmitDefaultValue = true)]
        public string TraitType { get; set; }
    }
}
