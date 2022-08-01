namespace ShadowrunTools.Characters.Traits
{
    using ReactiveUI;
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Foundation;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;

    public abstract class LeveledTrait : BaseTrait, ILeveledTrait, IAugmentable
    {

        public LeveledTrait(Guid id,
                            int prototypeHash,
                            string name,
                            string category,
                            ITraitContainer container,
                            ICategorizedTraitContainer root,
                            IRules rules)
            : base(id, prototypeHash, name, category, container, root, rules)
        {
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
                    me => me.BonusRating,
                    me => me.AugmentedMax,
                    (improved, bonus, max) => Math.Min(improved + bonus, max))
                .ToProperty(this, me => me.AugmentedRating);
        }

        protected int m_ExtraMin;
        public int BonusMin
        {
            get => m_ExtraMin;
            set => this.RaiseAndSetIfChanged(ref m_ExtraMin, value);
        }

        public abstract int Min { get; }

        protected int m_ExtraMax;
        public int BonusMax
        {
            get => m_ExtraMax;
            set => this.RaiseAndSetIfChanged(ref m_ExtraMax, value);
        }

        public abstract int Max { get; }

        protected int m_BaseIncrease;
        public int BaseIncrease
        {
            get => m_BaseIncrease;
            set => this.RaiseAndSetIfChanged(ref m_BaseIncrease, value);
        }

        private readonly ObservableAsPropertyHelper<int> _baseRating;
        public int BaseRating => _baseRating.Value;

        protected int m_Improvement;
        public int Improvement
        {
            get => m_Improvement;
            set => this.RaiseAndSetIfChanged(ref m_Improvement, value);
        }

        private readonly ObservableAsPropertyHelper<int> _improvedRating;
        public int ImprovedRating => _improvedRating.Value;

        private int m_BonusRating;
        public int BonusRating
        {
            get => m_BonusRating;
            set => this.RaiseAndSetIfChanged(ref m_BonusRating, value);
        }

        private readonly ObservableAsPropertyHelper<int> _augmentedRating;
        public int AugmentedRating => _augmentedRating.Value;

        public abstract int AugmentedMax { get; }

        public int CompareTo(ILeveledTrait other)
        {
            if (other is null)
            {
                return 1;
            }
            return ImprovedRating.CompareTo(other.ImprovedRating);
        }

        #region IAugmentable Implemenation


        private readonly Dictionary<string, List<IBonus>> _propToBonusMap = new();

        private readonly Dictionary<IBonus, string> _bonusToPropCache = new();
        public IEnumerable<IBonus> Bonuses => _bonusToPropCache.Keys;

        public virtual void AddBonus(IBonus bonus)
        {
            bonus.PropertyChanged += OnBonusPropertyChanged;
            _propToBonusMap.AddOrUpdate(
                bonus.TargetProperty,
                k => new List<IBonus> { bonus },
                (k, v) => { v.Add(bonus); return v; });

            _bonusToPropCache[bonus] = bonus.TargetProperty;
        }

        public virtual void RemoveBonus(IBonus bonus)
        {
            bonus.PropertyChanged -= OnBonusPropertyChanged;
            _propToBonusMap[bonus.TargetProperty].Remove(bonus);
            _bonusToPropCache.Remove(bonus);

            RecalcBonus(bonus.TargetProperty);
        }

        protected virtual void RecalcBonus(string propertyName)
        {
            var value = GetBonus(propertyName);
            switch (propertyName)
            {
                case nameof(BonusMin):
                    BonusMin = (int)Math.Floor(value);
                    break;
                case nameof(BonusMax):
                    BonusMax = (int)Math.Floor(value);
                    break;
                case nameof(BonusRating):
                    BonusRating = (int)Math.Floor(value);
                    break;
                default:
                    break;
            }
        }

        protected double GetBonus(string propName)
        {
            if (_propToBonusMap.TryGetValue(propName, out var bonuses))
            {
                return bonuses
                    .Select(b => b.Amount)
                    .Sum();
            }
            else
            {
                return 0d;
            }
        }

        private void OnBonusPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var bonus = (IBonus)sender;
            if (!_bonusToPropCache.ContainsKey(bonus))
            {
                throw new InvalidOperationException("Sanity check failed. Trait is subscribed to a Bonus it does not have.");
            }
            if (e.PropertyName == nameof(IBonus.TargetProperty))
            {
                var oldProp = _bonusToPropCache[bonus];
                RecalcBonus(oldProp);
                _bonusToPropCache[bonus] = bonus.TargetProperty;
                RecalcBonus(bonus.TargetProperty);
            }
            else if (e.PropertyName == nameof(IBonus.Amount))
            {
                RecalcBonus(bonus.TargetProperty);
            }
        }

        #endregion IAugmentable Implemenation
    }
}
