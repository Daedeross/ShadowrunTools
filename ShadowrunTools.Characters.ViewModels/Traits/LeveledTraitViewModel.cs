namespace ShadowrunTools.Characters.ViewModels.Traits
{
    using ReactiveUI;
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Characters.Traits;
    using System.Reactive.Disposables.Fluent;

    public abstract class LeveledTraitViewModel : TraitViewModelBase, ILeveledTraitViewModel
    {
        private readonly ILeveledTrait _leveledTrait;

        public LeveledTraitViewModel(DisplaySettings displaySettings, ILeveledTrait leveledTrait)
            : base(displaySettings, leveledTrait)
        {
            _leveledTrait = leveledTrait;

            _displayRating = this.WhenAnyValue(x => x.ImprovedRating, x => x.AugmentedRating, (imp, aug) => aug > imp ? $"{imp}({aug})" : $"{imp}")
                .ToProperty(this, x => x.DisplayRating)
                .DisposeWith(Disposables);

            _maxBaseIncrease = this.WhenAnyValue(x => x.Min, x => x.Max, x => x.Improvement, (min, max, imp) => max - min - imp)
                .ToProperty(this, x => x.MaxBaseIncrease)
                .DisposeWith(Disposables);

            _maxImprovement = this.WhenAnyValue(x => x.Min, x => x.Max, x => x.BaseIncrease, (min, max, inc) => max - min - inc)
                .ToProperty(this, x => x.MaxImprovement)
                .DisposeWith(Disposables);
        }

        private readonly ObservableAsPropertyHelper<string> _displayRating;
        public string DisplayRating => _displayRating.Value;

        private readonly ObservableAsPropertyHelper<int> _maxBaseIncrease;
        public int MaxBaseIncrease => _maxBaseIncrease.Value;

        private readonly ObservableAsPropertyHelper<int> _maxImprovement;
        public int MaxImprovement => _maxImprovement.Value;

        #region ILeveledTrait

        public int BonusMin { get => _leveledTrait.BonusMin; }

        public int Min => _leveledTrait.Min;

        public int BonusMax { get => _leveledTrait.BonusMax; }

        public int Max => _leveledTrait.Max;

        public bool CanChangeBaseRating => _leveledTrait.CanChangeBaseRating;

        public int BaseIncrease { get => _leveledTrait.BaseIncrease; set => _leveledTrait.BaseIncrease = value; }

        public int BaseRating { get => _leveledTrait.BaseRating; }

        public int BonusRating => _leveledTrait.BonusRating;

        public bool CanChangeImprovedRating => _leveledTrait.CanChangeImprovedRating;

        public int Improvement { get => _leveledTrait.Improvement; set => _leveledTrait.Improvement = value; }

        public int ImprovedRating => _leveledTrait.ImprovedRating;

        public int AugmentedRating => _leveledTrait.AugmentedRating;

        public int AugmentedMax => _leveledTrait.AugmentedMax;

        public int CompareTo(ILeveledTrait other)
        {
            return _leveledTrait.CompareTo(other);
        }

        public bool Improve(ImprovementSource source = ImprovementSource.Karma, int value = 1) => _leveledTrait.Improve(source, value);

        #endregion

        #region Commands

        #endregion

    }
}
