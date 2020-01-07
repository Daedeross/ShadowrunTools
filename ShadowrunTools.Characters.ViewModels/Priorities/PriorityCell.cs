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

        private string _visible;
        public string VisibleItems => _visible;

        public PriorityCell(DisplaySettings displaySettings, List<string> items)
            : base(displaySettings)
        {
            Items = items;
            _visible = string.Join("\n", Items.Take(displaySettings.PriorityCellVisibleItemsCount));
        }

        protected override void OnDisplaySettingsPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(DisplaySettings.PriorityCellVisibleItemsCount))
            {
                var count = _displaySettings.PriorityCellVisibleItemsCount;

                if (count >= 0)
                {
                    _visible = string.Join("\n", Items.Take(count));
                    RaisePropertyChanged(nameof(VisibleItems));
                }
            }
        }
    }
}
