﻿using System;

namespace ShadowrunTools.Characters.Traits
{
    public interface ILeveledTrait: ITrait, IComparable<ILeveledTrait>, IAugmentable
    {
        /// <summary>
        /// Added to the (base) minimum.
        /// </summary>
        int ExtraMin { get; set; }

        /// <summary>
        /// The actual Minimum level, after accounting for any extras.
        /// </summary>
        int Min { get; }

        /// <summary>
        /// Added to the (base) maximum.
        /// </summary>
        int ExtraMax { get; set; }

        /// <summary>
        /// The actual Maximum level, after accounting for any extras.
        /// </summary>
        int Max { get; }

        /// <summary>
        /// The Base Rating of the trait, before extras and Karma Improvement.
        /// </summary>
        int BaseRating { get; set; }
        /// <summary>
        /// Any Bonus to the Rating, from <see cref="IAugment"/>s
        /// </summary>
        int RatingBonus { get; }
        /// <summary>
        /// The improvement to the <see cref="BaseRating"/> from Karma Advancement
        /// </summary>
        int Improvement { get; set; }
        /// <summary>
        /// The "actual" rating before bonus, Base + Karma Improvement
        /// </summary>
        int ImprovedRating { get;}
        /// <summary>
        /// The rating after bonus
        /// </summary>
        int AugmentedRating { get; }

        /// <summary>
        /// The maximum augmented rating for this trait
        /// </summary>
        int AugmentedMax { get; }
    }
}
