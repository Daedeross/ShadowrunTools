namespace ShadowrunTools.Characters.Loaders
{
    using ShadowrunTools.Characters.Model;
    using System.Runtime.Serialization;

    [DataContract(Name = "SpecialSkillChoiceLoader", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class SpecialSkillChoiceLoader
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