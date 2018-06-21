namespace ShadowrunTools.Serialization.Prototypes
{
    using ShadowrunTools.Characters;
    using ShadowrunTools.Characters.Prototypes;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [KnownType(typeof(MetatypeAttributePrototype))]
    [DataContract(Name = "MetatypePrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class MetavariantPrototype: IMetavariantPrototype
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
        public List<IMetatypeAttribute> Attributes { get; set; }
    }
}
