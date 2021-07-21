using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Priorities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes.Priorities
{
    [DataContract(Name = "SpecialOptionPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class SpecialOptionPrototype : ISpecialOption
    {
        #region Serialized
        [DataMember(Name = "SkillOptions")]
        private List<SpecialSkillChoicePrototype> _skillOptions { get; set; } = new List<SpecialSkillChoicePrototype>();

        [DataMember(Name = "SkillGroupOptions")]
        private List<SpecialSkillChoicePrototype> _skillGroupOptions { get; set; } = new List<SpecialSkillChoicePrototype>();
        #endregion
        [DataMember]
        public string Quality { get; set; }
        [DataMember]
        public string AttributeName { get; set; }
        [DataMember]
        public int AttributeRating { get; set; }
        [DataMember]
        public int FreeSpells { get; set; }
        [DataMember]
        public int FreeComplexForms { get; set; }

        public IReadOnlyCollection<ISpecialSkillChoice> SkillOptions => _skillOptions;

        public IReadOnlyCollection<ISpecialSkillChoice> SkillGroupOptions => _skillGroupOptions;
    }
}
