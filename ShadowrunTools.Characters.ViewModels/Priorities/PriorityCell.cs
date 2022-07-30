using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunTools.Characters.ViewModels
{
    public class PriorityCell : ViewModelBase, IPriorityCell
    {
        public List<string> Items { get; private set; }
        public string VisibleItems { get; private set; }

        private bool mIsSelected;
        private readonly Action<bool> _onChangedCallback;

        public bool IsSelected
        {
            get => mIsSelected;
            set
            {
                if (mIsSelected != value)
                {
                    mIsSelected = value;
                    this.RaisePropertyChanged();
                    _onChangedCallback(value);
                }
            }
        }

        public PriorityCell(
            DisplaySettings displaySettings,
            List<string> items,
            Action<bool> onChangedCallback,
            bool isSelected = false)
            : base(displaySettings)
        {
            Items = items;
            mIsSelected = isSelected;
            _onChangedCallback = onChangedCallback;
            VisibleItems = string.Join("\n", Items.Take(displaySettings.PriorityCellVisibleItemsCount));
        }

        protected override void OnDisplaySettingsPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(DisplaySettings.PriorityCellVisibleItemsCount))
            {
                var count = _displaySettings.PriorityCellVisibleItemsCount;

                if (count >= 0)
                {
                    VisibleItems = string.Join("\n", Items.Take(count));
                    this.RaisePropertyChanged(nameof(VisibleItems));
                }
            }
        }
    }
}
