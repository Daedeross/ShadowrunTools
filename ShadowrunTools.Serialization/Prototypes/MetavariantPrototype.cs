namespace ShadowrunTools.Serialization.Prototypes
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "MetatypePrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class MetavariantPrototype
    {
        /// <summary>
        /// The name of the Metavariant. eg: Human, Nartaki, Cyclops, etc
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// The "Primary Subspecies." eg: Human, Elf, Ork, etc.
        /// </summary>
        [DataMember]
        public string Metatype { get; set; }
        [DataMember]
        public List<MetatypeAttribute> Attributes { get; set; }
    }
}
