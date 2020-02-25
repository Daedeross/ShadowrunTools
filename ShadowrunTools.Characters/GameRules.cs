namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Foundation;

    public class GameRules : PropertyList, IRules
    {
        public GenerationMethod GenerationMethod { get; set; }
        public int StartingKarma { get; set; }

        public int MaxAugment { get; set; }

        public int StartingSkillCap { get; set; }

        public int StartingMaxedSkillCount { get; set; }

        public int InPlaySkillCap { get; set; }

        public int StartingMaxedAttributeCount { get; set; }

        public int AttributeKarmaMult { get; set; }

        public int SkillGroupKarmaMult { get; set; }

        public int ActiveSkillKarmaMult { get; set; }

        public int MagicSkillKarmaMult { get; set; }

        public int ResonanceSkillKarmaMult { get; set; }

        public int KnowledgeSkillKarmaMult { get; set; }

        public int LanguageSkillKarmaMult { get; set; }

        public int InitiationKarmaBase { get; set; }

        public int InitiationKarmaMult { get; set; }

        public int SubmersionKarmaBase { get; set; }

        public int SubmersionKarmaMult { get; set; }

        public int ComplexFormKarma { get; set; }

        public int SpellKarma { get; set; }

        public int PowerPointKarma { get; set; }

        public int SpecializationKarma { get; set; }

        public int MartialArtsStyleKarma { get; set; }

        public int MartialArtsTechniqueKarma { get; set; }

        public int InPlayQualityMult { get; set; }
    }
}
