namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILeveledTrait: ITrait
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
        int BonusRating { get; set; }
        /// <summary>
        /// The "actual" rating, Base + Karma Improvement + Bonus
        /// </summary>
        int ImprovedRating { get; set; }
    }
}
