using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface IWorkspaceViewModel : IViewModel
    {
        IViewContainer CurrentTab { get; set; }

        IObservableCollection<IViewContainer> Documents { get; }

        IObservableCollection<ICharacterViewModel> Characters { get; }


        ICommand NewCharacterCommand { get; }

        ICommand LoadDataFile { get; }

        void LoadDataFiles(string[] filesNames);
    }
}
