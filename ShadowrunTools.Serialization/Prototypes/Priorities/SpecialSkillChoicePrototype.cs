using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes.Priorities
{
    [DataContract(Name = "SpecialSkillChoicePrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class SpecialSkillChoicePrototype: ISpecialSkillChoice
    {
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public SkillChoiceKind Kind { get; set; }
        [DataMember]
        public string Choice { get; set; }
    }
}
