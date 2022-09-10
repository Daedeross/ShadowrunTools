using ShadowrunTools.Characters.Model;
using System;

namespace ShadowrunTools.Characters.Traits
{
    /// <summary>
    /// Represents any trait that can come in multiple levels/ranks
    /// </summary>
    public interface ILeveledTrait: ITrait, IComparable<ILeveledTrait>
    {
        /// <summary>
        /// Added to the (base) minimum.
        /// </summary>
        int BonusMin { get; }

        /// <summary>
        /// The actual Minimum level, after accounting for any extras.
        /// </summary>
        int Min { get; }

        /// <summary>
        /// Added to the (base) maximum.
        /// </summary>
        int BonusMax { get; }

        /// <summary>
        /// The actual Maximum level, after accounting for any extras.
        /// </summary>
        int Max { get; }

        /// <summary>
        /// Increase to the BaseRating from Attribute Points or Life Modules.
        /// </summary>
        int BaseIncrease { get; set; }

        /// <summary>
        /// The Base Rating of the trait, before extras and Karma Improvement.
        /// </summary>
        int BaseRating { get; }

        /// <summary>
        /// The improvement to the <see cref="BaseRating"/> from Karma Advancement
        /// </summary>
        int Improvement { get; set; }

        /// <summary>
        /// The "actual" rating before bonus, Base + Karma Improvement
        /// </summary>
        int ImprovedRating { get; }

        /// <summary>
        /// Any Bonus to the Rating, from <see cref="IAugment"/>s
        /// </summary>
        int BonusRating { get; }

        /// <summary>
        /// The rating after bonus
        /// </summary>
        int AugmentedRating { get; }

        /// <summary>
        /// The maximum augmented rating for this trait
        /// </summary>
        int AugmentedMax { get; }

        /// <summary>
        /// Improve the trait's rating by one (1) using Karma.
        /// Only usable when In-play.
        /// </summary>
        /// <param name="source">The source of the improvement.</param>
        /// <returns>True if the improvement is valid.</returns>
        bool Improve(ImprovementSource source = ImprovementSource.Karma, int value = 1);
    }
}
