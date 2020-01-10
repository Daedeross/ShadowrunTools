using ShadowrunTools.Characters.Priorities;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes.Priorities
{
    [DataContract(Name = "AttributesPriorityPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class AttributesPriorityPrototype : IAttributesPriority
    {
        [DataMember(Name = "AttibutePoints")]
        public int AttibutePoints { get; set; }
    }
}
