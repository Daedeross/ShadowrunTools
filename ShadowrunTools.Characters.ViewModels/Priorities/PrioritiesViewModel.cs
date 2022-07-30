using DynamicData.Binding;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public class PrioritiesViewModel : ViewModelBase, IPrioritiesViewModel
    {
        public IObservableCollection<IPriorityRow> Rows { get; } = new ObservableCollectionExtended<IPriorityRow>();

        public PrioritiesViewModel(DisplaySettings displaySettings, IPriorities priorities, ICharacter model)
            : base(displaySettings)
        {
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.A, priorities, model.Priorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.B, priorities, model.Priorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.C, priorities, model.Priorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.D, priorities, model.Priorities));
            Rows.Add(new PriorityRow(displaySettings, PriorityLevel.E, priorities, model.Priorities));
        }
    }
}
