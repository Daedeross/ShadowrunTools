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
#pragma warning disable IDE1006 // Naming Styles
        [DataMember(Name = "SkillOptions")]
        public List<SpecialSkillChoicePrototype> _SkillOptions { get; set; } = new List<SpecialSkillChoicePrototype>();
        [DataMember(Name = "SkillGroupOptions")]
        public List<SpecialSkillChoicePrototype> _SkillGroupOptions { get; set; } = new List<SpecialSkillChoicePrototype>();
        #pragma warning restore IDE1006 // Naming Styles
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


        public IReadOnlyCollection<ISpecialSkillChoice> SkillOptions => _SkillOptions;

        public IReadOnlyCollection<ISpecialSkillChoice> SkillGroupOptions => _SkillGroupOptions;
    }
}
