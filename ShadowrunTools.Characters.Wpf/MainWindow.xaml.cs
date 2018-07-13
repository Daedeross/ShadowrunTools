using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShadowrunTools.Characters.Wpf
{
    internal class TestBaseTrait : BaseTrait
    {
        public TestBaseTrait(Guid id, ITraitContainer container, ICategorizedTraitContainer root)
            : base(id, "TEST", "Skill", container, root, null)
        {
        }

        public override TraitType TraitType => throw new NotImplementedException();

        public override bool Independant => throw new NotImplementedException();
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var id = Guid.NewGuid();

            var trait = new TestBaseTrait(id, null, null)
            {
                Name = "Bob",
                Book = "hai",
                Page = 12,
                SubCategory = "Sub Cat",
                UserNotes = "Hmm, this is a note."
            };

            var vm = new ViewModel.EditListViewModel(trait.BeginEdit());

            List_View.DataContext = vm;
        }
    }
}
