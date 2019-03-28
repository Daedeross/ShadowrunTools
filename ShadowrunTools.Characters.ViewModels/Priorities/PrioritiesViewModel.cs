using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public class PrioritiesViewModel : ViewModelBase
    {
        public ObservableCollection<PriorityRow> Rows { get; } = new ObservableCollection<PriorityRow>();

        public PrioritiesViewModel(DisplaySettings displaySettings, IPriorities priorities)
            : base(displaySettings)
        {
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.A, priorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.B, priorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.C, priorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.D, priorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.E, priorities));
        }
    }
}
