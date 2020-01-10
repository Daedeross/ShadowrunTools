using ShadowrunTools.Characters.Priorities;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes.Priorities
{
    [DataContract(Name = "SkillsPriorityPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class SkillsPriorityPrototype : ISkillsPriority
    {
        [DataMember]
        public int SkillPoints { get; set; }
        [DataMember]
        public int SkillGroupPoints { get; set; }
    }
}
