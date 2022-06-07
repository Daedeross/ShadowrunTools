using DynamicData;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface IWorkspaceViewModel : IViewModel
    {
        IObservableCollection<IDocumentViewModel> Documents { get; }

        IObservableCollection<ICharacterViewModel> Characters { get; }
    }
}
