namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Foundation;

    /// <summary>
    /// Interface for repository of game rules information
    /// </summary>
    public interface IRules: INotifyItemChanged
    {
        /// <summary>
        /// The method used to generate the character. <see cref="GenerationMethod"/>
        /// </summary>
        GenerationMethod GenerationMethod { get; }

        /// <summary>
        /// The descetionary Karma avalable during character creation.
        /// </summary>
        int StartingKarma { get; }

        /// <summary>
        /// The maximum increase to a <see cref="ILeveledTrait"/> due to augmentation.
        /// </summary>
        int MaxAugment { get; }

        /// <summary>
        /// The maximum number of skills that can have the highest available rating during character creation.
        /// </summary>
        int StartingMaxedSkillCount { get; }

        /// <summary>
        /// The maximum rating of skills in character creation.
        /// </summary>
        int StartingMaxSkillRating { get; }

        /// <summary>
        /// The maximum number of attributes that can have the highest available rating during character creation.
        /// </summary>
        int StartingMaxedAttributeCount { get; }

        /// <summary>
        /// The maximum number of discounts that can be applied to the Karma cost of Initiation
        /// </summary>
        int MaxInitiationDiscounts { get; }

        /// <summary>
        /// The maximum number of discounts that can be applied to the Karma cost of Initiation
        /// </summary>
        int MaxSubmersionDiscounts { get; }

        /// <summary>
        /// The maximum rating a skill can be raised to "in-play"
        /// </summary>
        int InPlayMaxSkillRating { get; }

        /// <summary>
        /// The ammount added to an attribute to get the pool for a skill with no rating.
        /// (defult -1)
        /// </summary>
        int SkillDefaultAdjustment { get; }

        #region Scaling Karma Costs

        /// <summary>
        /// For Karma advancement of Attributes.
        /// Karma cost is [New Rating] x AttributeKarmaMult (default 5 in base rules).
        /// </summary>
        int AttributeKarmaMult { get; }

        /// <summary>
        /// For Karma advancement of Skill Groups.
        /// Karma cost is [New Rating] x SkillGroupKarmaMult (default 5 in base rules).
        /// </summary>
        int SkillGroupKarmaMult { get; }

        /// <summary>
        /// For Karma advancement of Active Skills (including social).
        /// Karma cost is [New Rating] x ActiveSkillKarmaMult (default 2 in base rules).
        /// </summary>
        int ActiveSkillKarmaMult { get; }

        /// <summary>
        /// For Karma advancement of Magic Skills.
        /// Karma cost is [New Rating] x MagicSkillKarmaMult (default 2 in base rules).
        /// </summary>
        int MagicSkillKarmaMult { get; }

        /// <summary>
        /// For Karma advancement of Resonance Skills.
        /// Karma cost is [New Rating] x ResonanceSkillKarmaMult (default 2 in base rules).
        /// </summary>
        int ResonanceSkillKarmaMult { get; }

        /// <summary>
        /// For Karma advancement of Knowledge Skills.
        /// Karma cost is [New Rating] x KnowledgeSkillKarmaMult (default 1 in base rules).
        /// </summary>
        int KnowledgeSkillKarmaMult { get; }

        /// <summary>MagicSkill
        /// For Karma advancement of Language Skills (including social).
        /// Karma cost is [New Rating] x LanguageSkillKarmaMult (default 1 in base rules).
        /// </summary>
        int LanguageSkillKarmaMult { get; }

        /// <summary>
        /// The Base Karma cost for initiation
        /// </summary>
        int InitiationKarmaBase { get; }

        int InitiationKarmaMult { get; }

        int SubmersionKarmaBase { get; }

        int SubmersionKarmaMult { get; }

        #endregion Scaling Karma Costs

        #region Flat Karma Costs

        int ComplexFormKarma { get; }

        int SpellKarma { get; }

        int PowerPointKarma { get; }

        int SpecializationKarma { get; }

        int MartialArtsStyleKarma { get; }

        int MartialArtsTechniqueKarma { get; }

        int InPlayQualityMult { get; }

        #endregion // Flat Karma Costs
    }
}
