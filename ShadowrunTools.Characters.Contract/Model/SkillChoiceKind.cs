using System.Runtime.Serialization;

namespace ShadowrunTools.Characters.Model
{
    [DataContract(Name = "SkillChoiceKind", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public enum SkillChoiceKind
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        Skill = 1,
        [EnumMember]
        Group = 1 << 1,
        [EnumMember]
        Category = 1 << 2
    }
}
