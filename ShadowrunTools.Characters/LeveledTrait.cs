namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class LeveledTrait : BaseTrait, ILeveledTrait
    {
        public LeveledTrait(ITraitContainer container, ICategorizedTraitContainer root)
            : base (container, root)
        {
        }

        public virtual int ExtraMin { get; set; }
        public virtual int Min { get; set; }

        public virtual int ExtraMax { get; set; }
        public virtual int Max { get; set; }

        public int BaseRating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int BonusRating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ImprovedRating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
