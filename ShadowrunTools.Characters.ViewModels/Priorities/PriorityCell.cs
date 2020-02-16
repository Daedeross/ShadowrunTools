using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public class PriorityCell : ViewModelBase
    {
        public List<string> Items { get; private set; }
        public string VisibleItems { get; private set; }

        private bool mIsSelected;
        public bool IsSelected
        {
            get => mIsSelected;
            set => this.RaiseAndSetIfChanged(ref mIsSelected, value);
        }

        public PriorityCell(
            DisplaySettings displaySettings,
            List<string> items)
            : base(displaySettings)
        {
            Items = items;
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
