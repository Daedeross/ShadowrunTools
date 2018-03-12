namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Text;
    using ShadowrunTools.Characters.Model;

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

        public int BaseRating { get; set; }
        public int BonusRating { get; set; }
        public int Improvement { get; set; }
        public int ImprovedRating => BaseRating + Improvement;

        public int AugmentedRating => ImprovedRating + BonusRating;

        public ObservableCollection<IAugment> Augments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;

        public int CompareTo(ILeveledTrait other)
        {
            if (other is null)
            {
                return 1;
            }
            return ImprovedRating.CompareTo(other.ImprovedRating);
        }

        public void OnAugmentChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnAugmentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnAugmentRemoving(AugmentKind kind)
        {
            throw new NotImplementedException();
        }
    }
}
