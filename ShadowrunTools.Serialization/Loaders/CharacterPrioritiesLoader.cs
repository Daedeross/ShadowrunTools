namespace ShadowrunTools.Serialization.Loaders
{
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Characters.Priorities;
    using System.Runtime.Serialization;

    [DataContract(Name = "CharacterPriorities", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class CharacterPrioritiesLoader
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

        public static CharacterPrioritiesLoader Create(ICharacterPriorities priorities)
        {
            return new CharacterPrioritiesLoader
            {
                MetatypePriority = priorities.MetatypePriority,
                AttributePriority = priorities.AttributePriority,
                SpecialPriority = priorities.SpecialPriority,
                SkillPriority = priorities.SkillPriority,
                ResourcePriority = priorities.ResourcePriority
            };
        }
    }
}
