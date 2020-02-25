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

        public PrioritiesViewModel(DisplaySettings displaySettings, IPriorities priorities, ICharacterPriorities characterPriorities)
            : base(displaySettings)
        {
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.A, priorities, characterPriorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.B, priorities, characterPriorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.C, priorities, characterPriorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.D, priorities, characterPriorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.E, priorities, characterPriorities));
        }
    }
}
