﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using ReactiveUI;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Characters.ViewModels;
using ShadowrunTools.Characters.Wpf.Resources.Prototypes;
using ShadowrunTools.Serialization;
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

namespace ShadowrunTools.Characters.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<IWorkspaceViewModel>
    {
        private readonly IViewModelFactory _viewModelFactory;

        public IWorkspaceViewModel ViewModel
        {
            get { return (IWorkspaceViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(IWorkspaceViewModel), typeof(MainWindow), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IWorkspaceViewModel)value;
        }

        public MainWindow(IWorkspaceViewModel viewModel, IViewModelFactory viewModelFactory)
        {
            InitializeComponent();

            //var logger = new Castle.Core.Logging.NullLogger();
            //// temp bootstrap stuff
            //var serializer = new JsonSerializer();
            //serializer.Converters.Add(new StringEnumConverter());
            //var dataLoader = new TestData(serializer, logger)
            //{
            //    CurrentFiles = new List<string>
            //    {
            //        @"Resources\Prototypes\Attributes.json",
            //        @"Resources\Prototypes\Metatypes.json",
            //        @"Resources\Prototypes\merge.json",
            //        @"Resources\Prototypes\Priorities.json",
            //    }
            //};

            //var rules = new RulesPrototype();

            //var settings = new DisplaySettings { PriorityCellVisibleItemsCount = 2 };

            //var workspace = new WorkspaceViewModel(dataLoader, rules, settings);

            //this.DataContext = workspace;

            ViewModel = viewModel;
            _viewModelFactory = viewModelFactory;

            //requestInitiatives.DataContext = new Mock.MockRequestInitiativesViewModel();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Characters, view => view.DocumentsTabControl.ItemsSource)
                    .DisposeWith(d);

            });

            DataContext = ViewModel;
        }
    }
}
