namespace ShadowrunTools.Characters.Internal
{
    using System;

    [Flags]
    public enum LeveledTraitObservables
    {
        None              = 0,
        BaseRating        = 1 << 0,
        ImprovedRating    = 1 << 1,
        AugmentedRating   = 1 << 2,
        BaseImproved      = BaseRating     | ImprovedRating,
        BaseAugmented     = BaseRating     | AugmentedRating,
        ImprovedAugmented = ImprovedRating | AugmentedRating,
        All               = BaseRating     | ImprovedRating | AugmentedRating,
    }
}
