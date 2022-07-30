using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ICommonViewModel : IViewModel<ICharacter>
    {
        IObservableCollection<IAttributeViewModel> Attributes { get; }

        IObservableCollection<IQualityViewModel> Qualities { get; }
    }
}
