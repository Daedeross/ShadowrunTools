using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.Traits
{
    public interface IAugmentCollection
    {
        IObservableCollection<string> Gives { get; }
    }
}
