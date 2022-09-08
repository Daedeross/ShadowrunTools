#nullable disable
namespace ShadowrunTools.Characters.Wpf.Views
{
    using ReactiveUI;
    using ShadowrunTools.Characters.ViewModels;
    using ShadowrunTools.Characters.Wpf.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for CharacterSkillsView.xaml
    /// </summary>
    public partial class CharacterSkillsView : ReactiveUserControl<ICharacterSkillsViewModel>
    {
        private const double GridViewPadding = 10d;
        private Dictionary<object, GridViewColumnHeader> _headers = new Dictionary<object, GridViewColumnHeader>();

        public CharacterSkillsView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.ActiveSkills, view => view.ActiveSkills.ItemsSource)
                    .DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.ActiveSkillFilters, view => view.ActiveSkillFilterCombo.ItemsSource)
                    .DisposeWith(d);
                this.Bind(ViewModel, vm => vm.SelectedActiveSkillFilter, view => view.ActiveSkillFilterCombo.SelectedItem)
                    .DisposeWith(d);
                this.Bind(ViewModel, vm => vm.ActiveSkillSearchText, view => view.ActiveSkillSearchBox.Text)
                    .DisposeWith(d);
            });
        }

        private void ActiveSkills_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth - GridViewPadding;

            var usedWidth = gridView.Columns
                .Where(c => !MyListBoxItemAssist.GetFillRemaining(c))
                .Sum(c => Math.Max(c.ActualWidth, FindColumnHeader(listView, c)?.ActualWidth ?? 0d));
            var remainingWidth = workingWidth - usedWidth;
            var columsToFill = gridView.Columns.Where(MyListBoxItemAssist.GetFillRemaining).ToList();

            if (columsToFill.Any() && remainingWidth > 0)
            {
                var each = remainingWidth / columsToFill.Count;
                var remainder = remainingWidth % columsToFill.Count;

                foreach (var column in columsToFill)
                {
                    column.Width = each;
                }

                columsToFill.Last().Width += remainder;
            }
        }

        private GridViewColumnHeader FindColumnHeader(DependencyObject start, GridViewColumn gridViewColumn)
        {
            if (!_headers.TryGetValue(gridViewColumn, out var header))
            {
                header = FindColumnHeaderDfs(start, gridViewColumn);
                _headers.Add(gridViewColumn, header);
            }

            return header;
        }

        private GridViewColumnHeader FindColumnHeaderDfs(DependencyObject start, GridViewColumn gridViewColumn)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                Visual childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                if (childVisual is GridViewColumnHeader)
                {
                    GridViewColumnHeader gridViewHeader = childVisual as GridViewColumnHeader;
                    if (gridViewHeader.Column == gridViewColumn)
                    {
                        return gridViewHeader;
                    }
                }
                GridViewColumnHeader childGridViewHeader = FindColumnHeaderDfs(childVisual, gridViewColumn);  // recursive
                if (childGridViewHeader != null)
                {
                    return childGridViewHeader;
                }
            }

            return null;
        }

        private ScrollViewer FindScrollViewerBfs(DependencyObject start)
        {
            var explored = new HashSet<DependencyObject>();
            Queue<DependencyObject> queue = new Queue<DependencyObject>();
            explored.Add(start);
            queue.Enqueue(start);

            while (queue.Any())
            {
                var node = queue.Dequeue();
                if (node is ScrollViewer scrollViewer)
                {
                    return scrollViewer;
                }
                else
                {
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++)
                    {
                        var child = VisualTreeHelper.GetChild(node, i);
                        if (!explored.Contains(child))
                        {
                            queue.Enqueue(child);
                        }
                    }
                }
            }

            return null;
        }
    }
}
