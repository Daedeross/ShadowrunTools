using ShadowrunTools.Characters.Priorities;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes.Priorities
{
    [DataContract(Name = "ResourcesPriorityPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class ResourcesPriorityPrototype : IResourcesPriority
    {
        [DataMember]
        public decimal Resources { get; set; }
    }
}
