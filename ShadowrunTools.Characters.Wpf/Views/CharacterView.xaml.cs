using ReactiveUI;
using ShadowrunTools.Characters.ViewModels;
using System.Reactive.Disposables;

namespace ShadowrunTools.Characters.Wpf.Views
{
    /// <summary>
    /// Interaction logic for CharacterView.xaml
    /// </summary>
    public partial class CharacterView : ReactiveUserControl<ICharacterViewModel>
    {
        public CharacterView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.Priorities, view => view.Priorities.ViewModel)
                    .DisposeWith(d);

                this.Bind(ViewModel, vm => vm.Common, view => view.Attributes.ViewModel)
                    .DisposeWith(d);
            });
        }
    }
}
