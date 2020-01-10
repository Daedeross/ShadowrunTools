namespace ShadowrunTools.Serialization.Prototypes
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    public class PrototypeFile
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public List<AttributePrototype> Attributes { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public List<MetavariantPrototype> Metavariants { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public PrioritiesPrototype Priorities { get; set; }
    }
}
