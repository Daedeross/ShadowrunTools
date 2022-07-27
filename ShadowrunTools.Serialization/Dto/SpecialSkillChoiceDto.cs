namespace ShadowrunTools.Serialization
{
    using ShadowrunTools.Characters.Model;
    using System.Runtime.Serialization;

    [DataContract(Name = "SpecialSkillChoice", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class SpecialSkillChoiceDto
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