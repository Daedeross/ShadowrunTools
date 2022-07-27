using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Foundation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShadowrunTools.Serialization
{
    public class RulesPrototype
    {
        [DataMember]
        [Display(Editable = true)]
        public GenerationMethod GenerationMethod { get; set; } = GenerationMethod.Priority;
        [DataMember]
        [Display(Editable = true)]
        public int StartingKarma { get; set; } = 20;

        [DataMember]
        [Display(Editable = true)]
        public int MaxAugment { get; set; } = 4;

        [DataMember]
        [Display(Editable = true)]
        public int StartingSkillCap { get; set; } = 6;

        [DataMember]
        [Display(Editable = true)]
        public int StartingMaxedSkillCount { get; set; } = 2;

        [DataMember]
        [Display(Editable = true)]
        public int InPlaySkillCap { get; set; } = 12;

        [DataMember]
        [Display(Editable = true)]
        public int StartingMaxedAttributeCount { get; set; } = 1;

        [DataMember]
        [Display(Editable = true)]
        public int AttributeKarmaMult { get; set; } = 5;

        [DataMember]
        [Display(Editable = true)]
        public int SkillGroupKarmaMult { get; set; } = 5;

        [DataMember]
        [Display(Editable = true)]
        public int ActiveSkillKarmaMult { get; set; } = 2;

        [DataMember]
        [Display(Editable = true)]
        public int MagicSkillKarmaMult { get; set; } = 2;

        [DataMember]
        [Display(Editable = true)]
        public int ResonanceSkillKarmaMult { get; set; } = 2;

        [DataMember]
        [Display(Editable = true)]
        public int KnowledgeSkillKarmaMult { get; set; } = 1;

        [DataMember]
        [Display(Editable = true)]
        public int LanguageSkillKarmaMult { get; set; } = 1;

        [DataMember]
        [Display(Editable = true)]
        public int InitiationKarmaBase { get; set; } = 10;

        [DataMember]
        [Display(Editable = true)]
        public int InitiationKarmaMult { get; set; } = 3;

        [DataMember]
        [Display(Editable = true)]
        public int SubmersionKarmaBase { get; set; } = 10;

        [DataMember]
        [Display(Editable = true)]
        public int SubmersionKarmaMult { get; set; } = 3;

        [DataMember]
        [Display(Editable = true)]
        public int ComplexFormKarma { get; set; } = 4;

        [DataMember]
        [Display(Editable = true)]
        public int SpellKarma { get; set; } = 5;

        [DataMember]
        [Display(Editable = true)]
        public int PowerPointKarma { get; set; } = 5;

        [DataMember]
        [Display(Editable = true)]
        public int SpecializationKarma { get; set; } = 7;

        [DataMember]
        [Display(Editable = true)]
        public int MartialArtsStyleKarma { get; set; } = 7;

        [DataMember]
        [Display(Editable = true)]
        public int MartialArtsTechniqueKarma { get; set; } = 5;

        [DataMember]
        [Display(Editable = true)]
        public int InPlayQualityMult { get; set; } = 2;

        [DataMember]
        [Display(Editable = true)]
        public int MaxInitiationDiscounts { get; set; } = 3;

        [DataMember]
        [Display(Editable = true)]
        public int MaxSubmersionDiscounts { get; set; } = 3;
    }
}
