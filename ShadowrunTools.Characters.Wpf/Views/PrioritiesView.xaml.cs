using ReactiveUI;
using ShadowrunTools.Characters.ViewModels;

namespace ShadowrunTools.Characters.Wpf.Views
{
    /// <summary>
    /// Interaction logic for PrioritiesView.xaml
    /// </summary>
    public partial class PrioritiesView : ReactiveUserControl<IPrioritiesViewModel>
    {
        public PrioritiesView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                DataContext = ViewModel;
            });

            DataContext = ViewModel;
        }
    }
}
