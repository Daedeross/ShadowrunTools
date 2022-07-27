using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Prototypes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes
{
    [KnownType(typeof(MetatypeAttributePrototype))]
    [DataContract(Name = "MetatypePrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class MetavariantPrototype: IMetavariantPrototype
    {
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public Guid Id { get; set; }

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

        [DataMember(Name = "Attributes")]
        private List<MetatypeAttributePrototype> _attributes;

        public IReadOnlyCollection<IMetatypeAttribute> Attributes => _attributes;
    }
}
