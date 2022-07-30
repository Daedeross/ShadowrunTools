using DynamicData.Binding;
using ReactiveUI;
using ShadowrunTools.Characters.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
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

namespace ShadowrunTools.Characters.Wpf.Views
{
    /// <summary>
    /// Interaction logic for AttributesView.xaml
    /// </summary>
    public partial class AttributesView : ReactiveUserControl<ICommonViewModel>
    {
        public AttributesView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Attributes, view => view.AttributesList.ItemsSource)
                    .DisposeWith(d);
            });
        }

        private void AttributesList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //ListView listView = sender as ListView;
            //GridView gridView = listView.View as GridView;
            //var actualWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            //for (Int32 i = 1; i < gridView.Columns.Count; i++)
            //{
            //    actualWidth -= gridView.Columns[i].ActualWidth;
            //}
            //gridView.Columns[0].Width = actualWidth;
        }
    }
}
