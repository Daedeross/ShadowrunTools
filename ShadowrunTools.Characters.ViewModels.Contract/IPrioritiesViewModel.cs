using DynamicData.Binding;
using ShadowrunTools.Characters.Priorities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface IPrioritiesViewModel : IViewModel<ICharacter>
    {
        IObservableCollection<IPriorityRow> Rows { get; }
    }
}
