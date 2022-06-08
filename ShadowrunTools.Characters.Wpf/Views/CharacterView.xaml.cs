using ReactiveUI;
using ShadowrunTools.Characters.ViewModels;

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
                this
            });
        }
    }
}
