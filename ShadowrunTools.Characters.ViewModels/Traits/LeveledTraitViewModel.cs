using ReactiveUI;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels.Traits
{
    public abstract class LeveledTraitViewModel : TraitViewModelBase, ILeveledTraitViewModel
    {
        private readonly ILeveledTrait _leveledTrait;
        private static readonly ISet<string> _propertyNames;

        static LeveledTraitViewModel()
        {
            _propertyNames = new HashSet<string>(typeof(LeveledTraitViewModel)
                .GetProperties(System.Reflection.BindingFlags.Instance).Select(pi => pi.Name));
        }

        public LeveledTraitViewModel(DisplaySettings displaySettings, ILeveledTrait leveledTrait)
            : base(displaySettings, leveledTrait)
        {
            _leveledTrait = leveledTrait;

            _displayRating = this.WhenAnyValue(x => x.ImprovedRating, x => x.AugmentedRating, (imp, aug) => aug > imp ? $"{imp}({aug})" : $"{imp}")
                .ToProperty(this, x => x.DisplayRating);

            _maxBaseIncrease = this.WhenAnyValue(x => x.Min, x => x.Max, x => x.Improvement, (min, max, imp) => max - min - imp)
                .ToProperty(this, x => x.MaxBaseIncrease);

            _maxImprovement = this.WhenAnyValue(x => x.Min, x => x.Max, x => x.BaseIncrease, (min, max, inc) => max - min - inc)
                .ToProperty(this, x => x.MaxImprovement);

        }

        private ObservableAsPropertyHelper<string> _displayRating;
        public string DisplayRating => _displayRating.Value;

        private ObservableAsPropertyHelper<int> _maxBaseIncrease;
        public int MaxBaseIncrease => _maxBaseIncrease.Value;

        private ObservableAsPropertyHelper<int> _maxImprovement;
        public int MaxImprovement => _maxImprovement.Value;

        #region ILeveledTrait

        public int ExtraMin { get => _leveledTrait.ExtraMin; set => _leveledTrait.ExtraMin = value; }

        public int Min => _leveledTrait.Min;

        public int ExtraMax { get => _leveledTrait.ExtraMax; set => _leveledTrait.ExtraMax = value; }

        public int Max => _leveledTrait.Max;

        public int BaseIncrease { get => _leveledTrait.BaseIncrease; set => _leveledTrait.BaseIncrease = value; }

        public int BaseRating { get => _leveledTrait.BaseRating; }

        public int RatingBonus => _leveledTrait.RatingBonus;

        public int Improvement { get => _leveledTrait.Improvement; set => _leveledTrait.Improvement = value; }

        public int ImprovedRating => _leveledTrait.ImprovedRating;

        public int AugmentedRating => _leveledTrait.AugmentedRating;

        public int AugmentedMax => _leveledTrait.AugmentedMax;

        public ObservableCollection<IAugment> Augments => _leveledTrait.Augments;

        public int CompareTo(ILeveledTrait other)
        {
            return _leveledTrait.CompareTo(other);
        }

        public void OnAugmentChanged(object sender, ItemChangedEventArgs e)
        {
            _leveledTrait.OnAugmentChanged(sender, e);
        }

        public void OnAugmentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _leveledTrait.OnAugmentCollectionChanged(sender, e);
        }

        public void OnAugmentRemoving(AugmentKind kind)
        {
            _leveledTrait.OnAugmentRemoving(kind);
        }

        #endregion

        #region Commands

        #endregion

    }
}
