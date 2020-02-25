using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Foundation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Serialization
{
    public class RulesPrototype : PropertyList, IRules
    {
        public GenerationMethod GenerationMethod { get; set; } = GenerationMethod.Priority;
        public int StartingKarma { get; set; } = 20;

        public int MaxAugment { get; set; } = 4;

        public int StartingSkillCap { get; set; } = 6;

        public int StartingMaxedSkillCount { get; set; } = 2;

        public int InPlaySkillCap { get; set; } = 12;

        public int StartingMaxedAttributeCount { get; set; } = 1;

        public int AttributeKarmaMult { get; set; } = 5;

        public int SkillGroupKarmaMult { get; set; } = 5;

        public int ActiveSkillKarmaMult { get; set; } = 2;

        public int MagicSkillKarmaMult { get; set; } = 2;

        public int ResonanceSkillKarmaMult { get; set; } = 2;

        public int KnowledgeSkillKarmaMult { get; set; } = 1;

        public int LanguageSkillKarmaMult { get; set; } = 1;

        public int InitiationKarmaBase { get; set; } = 10;

        public int InitiationKarmaMult { get; set; } = 3;

        public int SubmersionKarmaBase { get; set; } = 10;

        public int SubmersionKarmaMult { get; set; } = 3;

        public int ComplexFormKarma { get; set; } = 4;

        public int SpellKarma { get; set; } = 5;

        public int PowerPointKarma { get; set; } = 5;

        public int SpecializationKarma { get; set; } = 7;

        public int MartialArtsStyleKarma { get; set; } = 7;

        public int MartialArtsTechniqueKarma { get; set; } = 5;

        public int InPlayQualityMult { get; set; } = 2;
    }
}
