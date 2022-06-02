namespace ShadowrunTools.Characters.Traits
{
    using ShadowrunTools.Characters.Model;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;

    public abstract class LeveledTrait : BaseTrait, ILeveledTrait
    {
        protected readonly HashSet<string> RemovedNames = new HashSet<string>();

        public LeveledTrait(Guid id, int prototypeHash, string name, string category, ITraitContainer container, ICategorizedTraitContainer root, IRules rules)
            : base (id, prototypeHash, name, category, container, root, rules)
        {
            Augments = new ObservableCollection<IAugment>();
            Augments.CollectionChanged += OnAugmentCollectionChanged;
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

        protected double _ratingBonus;
        public int RatingBonus
        {
            get
            {
                if (_ratingBonus > mRules.MaxAugment)
                    _ratingBonus = mRules.MaxAugment;
                if (_ratingBonus < 0)
                    _ratingBonus = 0;
                return (int)_ratingBonus;
            }
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

        public int AugmentedRating => Math.Min(ImprovedRating + RatingBonus, AugmentedMax);

        public abstract int AugmentedMax { get; }

        public ObservableCollection<IAugment> Augments { get; }

        public int CompareTo(ILeveledTrait other)
        {
            if (other is null)
            {
                return 1;
            }
            return ImprovedRating.CompareTo(other.ImprovedRating);
        }

        #region IAugmentable Implemenation

        public void OnAugmentChanged(object sender, ItemChangedEventArgs e)
        {
            var augment = sender as IAugment;
            if (augment is null)
            {
                Logger.Warn("Non-augment sent to OnAugmentChanged event on Trait:{0}", Name);
                return;
            }
            if (e.PropertyNames.Contains(nameof(IAugment.Bonus)))
            {
                RecalcBonus();
            }
        }

        public void OnAugmentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HashSet<string> propNames = new HashSet<string>();
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is IAugment augment)
                    {
                        OnAugmentAdded(augment);

                        propNames.Add(augment.TargetName);
                        augment.ItemChanged -= this.OnAugmentChanged;
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

                        augment.ItemChanged += this.OnAugmentChanged;
                    }
                }
            }
        }

        public virtual void OnAugmentRemoving(AugmentKind kind)
        {
            switch (kind)
            {
                case AugmentKind.None:
                    break;
                case AugmentKind.Rating:
                    RemovedNames.Add(nameof(RatingBonus));
                    RemovedNames.Add(nameof(AugmentedRating));
                    //RemovedNames.Add(nameof(DisplayValue)); // TODO: maybe move to ViewModel?
                    break;
                case AugmentKind.Max:
                    RemovedNames.Add(nameof(Max));
                    break;
                case AugmentKind.DamageValue:
                    break;
                case AugmentKind.DamageType:
                    break;
                case AugmentKind.Accuracy:
                    break;
                case AugmentKind.Availability:
                    break;
                case AugmentKind.Restriction:
                    break;
                case AugmentKind.RC:
                    break;
                case AugmentKind.AP:
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnAugmentAdded(IAugment augment) { }

        protected virtual void OnAugmentRemoved(IAugment augment) { }

        protected virtual void RecalcBonus(HashSet<string> propNames = null)
        {
            //create HashSet if needed
            if (propNames == null)
                propNames = new HashSet<string>();

            foreach (string name in RemovedNames)
            {
                propNames.Add(name);
            }
            RemovedNames.Clear();

            //Clear Bonus
            ExtraMax = 0;
            ExtraMin = 0;
            _ratingBonus = 0;

            foreach (var a in Augments)
                propNames = AddAugment(a, propNames);

            if (_ratingBonus > mRules.MaxAugment)
            {
                _ratingBonus = mRules.MaxAugment;
            }

            // Call ItemChanged
            RaiseItemChanged(propNames.ToArray());
        }

        protected virtual HashSet<string> AddAugment(IAugment augment, HashSet<string> propNames)
        {
            if (augment.Kind == AugmentKind.Rating)
            {
                _ratingBonus += augment.Bonus;
                propNames.Add(nameof(RatingBonus));
                propNames.Add(nameof(AugmentedRating));
                //propNames.Add(nameof(DisplayValue)); // TODO: maybe move to ViewModel?
            }
            if (augment.Kind == AugmentKind.Max)
            {
                ExtraMax += (int)augment.Bonus;
                propNames.Add(nameof(Max));
            }
            return propNames;
        }

        #endregion IAugmentable Implemenation
    }
}
