namespace ShadowrunTools.Serialization
{
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Characters.Priorities;
    using System.Runtime.Serialization;

    [DataContract(Name = "CharacterPriorities", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class CharacterPrioritiesDto
    {
        [DataMember]
        public PriorityLevel MetatypePriority { get; set; }

        [DataMember]
        public PriorityLevel AttributePriority { get; set; }

        [DataMember]
        public PriorityLevel SpecialPriority { get; set; }

        [DataMember]
        public PriorityLevel SkillPriority { get; set; }

        [DataMember]
        public PriorityLevel ResourcePriority { get; set; }
    }
}
