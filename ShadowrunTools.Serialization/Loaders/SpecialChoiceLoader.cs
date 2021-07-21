namespace ShadowrunTools.Serialization.Loaders
{
    using ShadowrunTools.Characters.Loaders;
    using System.Runtime.Serialization;

    [DataContract(Name = "SpecialChoiceLoader", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class SpecialChoiceLoader
    {
        [DataMember]
        public string Quality { get; set; }

        [DataMember]
        public string AttributeName { get; set; }

        [DataMember]
        public int AttributeRating { get; set; }

        [DataMember]
        private SpecialSkillChoiceLoader SkillOptions { get; set; }

        [DataMember]
        private SpecialSkillChoiceLoader SkillGroupOptions { get; set; }

        [DataMember]
        public int FreeSpells { get; set; }

        [DataMember]
        public int FreeComplexForms { get; set; }
    }
}
