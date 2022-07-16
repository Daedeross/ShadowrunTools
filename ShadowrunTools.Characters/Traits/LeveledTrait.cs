namespace ShadowrunTools.Characters.Traits
{
    using ReactiveUI;
    using ShadowrunTools.Characters.Model;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    public abstract class LeveledTrait : BaseTrait, ILeveledTrait
    {
        protected readonly HashSet<string> RemovedNames = new();

        public LeveledTrait(Guid id,
                            int prototypeHash,
                            string name,
                            string category,
                            ITraitContainer container,
                            ICategorizedTraitContainer root,
                            IRules rules)
            : base (id, prototypeHash, name, category, container, root, rules)
        {
            Augments = new ObservableCollection<IAugment>();
            Augments.CollectionChanged += OnAugmentCollectionChanged;

            _baseRating = this.WhenAnyValue(
                    me => me.Min,
                    me => me.BaseIncrease,
                    me => me.Max,
                    (min, increase, max) => Math.Min(min + increase, max))
                .ToProperty(this, me => me.BaseRating);

            _improvedRating = this.WhenAnyValue(
                    me => me.BaseRating,
                    me => me.Improvement,
                    me => me.Max,
                    (rating, improvement, max) => Math.Min(rating + improvement, max))
                .ToProperty(this, me => me.ImprovedRating);

            _augmentedRating = this.WhenAnyValue(
                    me => me.ImprovedRating,
                    me => me.RatingBonus,
                    me => me.AugmentedMax,
                    (improved, bonus, max) => Math.Min(improved + bonus, max))
                .ToProperty(this, me => me.AugmentedRating);
        }

        private int m_ExtraMin;
        public int ExtraMin
        {
            get => m_ExtraMin;
            set => this.RaiseAndSetIfChanged(ref m_ExtraMin, value);
        }

        public abstract int Min { get; }

        private int m_ExtraMax;
        public int ExtraMax
        {
            get => m_ExtraMax;
            set => this.RaiseAndSetIfChanged(ref m_ExtraMax, value);
        }

        public abstract int Max { get; }

        private int m_BaseIncrease;
        public int BaseIncrease
        {
            get => m_BaseIncrease;
            set => this.RaiseAndSetIfChanged(ref m_BaseIncrease, value);
        }

        private readonly ObservableAsPropertyHelper<int> _baseRating;
        public int BaseRating => _baseRating.Value;

        private int m_Improvement;
        public int Improvement
        {
            get => m_Improvement;
            set => this.RaiseAndSetIfChanged(ref m_Improvement, value);
        }

        private readonly ObservableAsPropertyHelper<int> _improvedRating;
        public int ImprovedRating => _improvedRating.Value;


        private double _ratingBonus;
        public int RatingBonus => (int)_ratingBonus;


        private readonly ObservableAsPropertyHelper<int> _augmentedRating;
        public int AugmentedRating =>  _augmentedRating.Value;

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
            if (sender is not IAugment)
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
            HashSet<string> propNames = new();
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

            foreach (var name in propNames)
            {
                this.RaisePropertyChanged(name);
            }
        }

        protected virtual HashSet<string> AddAugment(IAugment augment, HashSet<string> propNames)
        {
            if (augment.Kind == AugmentKind.Rating)
            {
                _ratingBonus += augment.Bonus;
                propNames.Add(nameof(RatingBonus));
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
