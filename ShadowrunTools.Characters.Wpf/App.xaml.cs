using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

#nullable enable

namespace ShadowrunTools.Characters.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Bootstrapper _bootstrapper;

        public App()
        {
            _bootstrapper = new Bootstrapper().Setup();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _bootstrapper.DisplayRootView();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _bootstrapper.Dispose();
        }
    }
}
