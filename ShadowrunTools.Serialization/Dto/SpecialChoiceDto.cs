namespace ShadowrunTools.Serialization
{
    using System.Runtime.Serialization;

    [DataContract(Name = "SpecialChoice", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class SpecialChoiceDto
    {
        [DataMember]
        public string Quality { get; set; }

        [DataMember]
        public string AttributeName { get; set; }

        [DataMember]
        public int AttributeRating { get; set; }

        [DataMember]
        private SpecialSkillChoiceDto SkillOptions { get; set; }

        [DataMember]
        private SpecialSkillChoiceDto SkillGroupOptions { get; set; }

        [DataMember]
        public int FreeSpells { get; set; }

        [DataMember]
        public int FreeComplexForms { get; set; }
    }
}
