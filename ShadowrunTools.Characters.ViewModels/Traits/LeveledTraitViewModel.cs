using ReactiveUI;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels.Traits
{
    public class LeveledTraitViewModel : TraitViewModelBase, ILeveledTrait
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
            _leveledTrait.ItemChanged += OnTraitChanged;
        }

        public event EventHandler<ItemChangedEventArgs> ItemChanged
        {
            add
            {
                _leveledTrait.ItemChanged += value;
            }

            remove
            {
                _leveledTrait.ItemChanged -= value;
            }
        }

        public int ExtraMin { get => _leveledTrait.ExtraMin; set => _leveledTrait.ExtraMin = value; }

        public int Min => _leveledTrait.Min;

        public int ExtraMax { get => _leveledTrait.ExtraMax; set => _leveledTrait.ExtraMax = value; }

        public int Max => _leveledTrait.Max;

        public int BaseRating { get => _leveledTrait.BaseRating; set => _leveledTrait.BaseRating = value; }

        public int RatingBonus => _leveledTrait.RatingBonus;

        public int Improvement { get => _leveledTrait.Improvement; set => _leveledTrait.Improvement = value; }

        public int ImprovedRating => _leveledTrait.ImprovedRating;

        public int AugmentedRating => _leveledTrait.AugmentedRating;

        public int AugmentedMax => _leveledTrait.AugmentedMax;

        public ObservableCollection<IAugment> Augments => _leveledTrait.Augments;

        #region ILeveledTrait

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

        protected override void OnTraitChanged(object sender, ItemChangedEventArgs e)
        {
            foreach (var propName in e.PropertyNames)
            {
                if (_propertyNames.Contains(propName))
                {
                    this.RaisePropertyChanged(propName);
                }
            }
        }
    }
}
