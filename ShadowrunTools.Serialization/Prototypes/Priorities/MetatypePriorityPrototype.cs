using ShadowrunTools.Characters.Priorities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes.Priorities
{
    [DataContract(Name = "MetatypePriorityPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class MetatypePriorityPrototype : IMetatypePriority
    {
        public IReadOnlyCollection<IPriorityMetavariantOption> MetavariantOptions => _MetavariantOptions;
        [DataMember(Name = "MetavariantOptions")]
        public List<PriorityMetavariantOptionPrototype> _MetavariantOptions { get; set; } = new List<PriorityMetavariantOptionPrototype>();
    }
}
