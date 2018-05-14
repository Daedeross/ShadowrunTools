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
            _augments = new ObservableCollection<IAugment>();
            _augments.CollectionChanged += OnAugmentCollectionChanged;
        }

        protected int _extraMin;
        public virtual int ExtraMin
        {
            get => _extraMin;
            set
            {
                if (_extraMin != value)
                {
                    _extraMin = value;
                    RaiseItemChanged(new[]
                    {
                        nameof(ExtraMin),
                        nameof(Min),
                        nameof(BaseRating),
                        nameof(ImprovedRating),
                        nameof(AugmentedRating),
                    });
                }
            }
        }
        public abstract int Min { get; }

        protected int _extraMax;
        public virtual int ExtraMax
        {
            get => _extraMax;
            set
            {
                if (_extraMax != value)
                {
                    _extraMax = value;
                    RaiseItemChanged(new[]
                    {
                        nameof(ExtraMax),
                        nameof(Max),
                        nameof(AugmentedMax),
                    });
                }
            }
        }

        public abstract int Max { get; }

        protected int _baseIncrease;
        public int BaseRating
        {
            get => Min + _baseIncrease;
            set
            {
                if (value <= Max)
                {
                    var diff = value - Min;
                    if (diff >= 0 && diff != _baseIncrease)
                    {
                        _baseIncrease = diff;
                        RaiseItemChanged(new[]
                        {
                            nameof(BaseRating),
                            nameof(ImprovedRating),
                            nameof(AugmentedRating)
                        });
                    }
                }
            }
        }

        protected int _ratingBonus;
        public int RatingBonus
        {
            get => _ratingBonus;
            protected set
            {
                if (value != _ratingBonus)
                {
                    _ratingBonus = value;
                    RaiseItemChanged(new[]
                    {
                        nameof(AugmentedRating)
                    });
                }
            }
        }

        protected int _improvement;
        public int Improvement
        {
            get => _improvement;
            set
            {
                if (_improvement != value && value + BaseRating <= Max)
                {
                    _improvement = value;
                    RaiseItemChanged(new[]
                    {
                        nameof(ImprovedRating),
                        nameof(AugmentedRating)
                    });
                }
            }
        }
        public int ImprovedRating => BaseRating + Improvement;

        public int AugmentedRating => Math.Min(ImprovedRating + RatingBonus, Max);

        public abstract int AugmentedMax { get; }

        private ObservableCollection<IAugment> _augments;
        public ObservableCollection<IAugment> Augments
        {
            get => _augments;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int CompareTo(ILeveledTrait other)
        {
            if (other is null)
            {
                return 1;
            }
            return ImprovedRating.CompareTo(other.ImprovedRating);
        }

        public abstract void OnAugmentChanged(object sender, PropertyChangedEventArgs e);

        public void OnAugmentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is IAugment augment)
                    {
                        OnAugmentAdded(augment);
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is IAugment augment)
                    {
                        OnAugmentRemoved(augment);
                    }
                }
            }
        }

        public abstract void OnAugmentRemoving(AugmentKind kind);

        protected abstract void OnAugmentAdded(IAugment augment);

        protected abstract void OnAugmentRemoved(IAugment augment);
    }
}
