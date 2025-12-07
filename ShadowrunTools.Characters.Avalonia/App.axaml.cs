using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using ShadowrunTools.Characters.Avalonia.Views;
using System;

namespace ShadowrunTools.Characters.Avalonia;

public partial class App : Application
{
    private readonly Bootstrapper _bootstrapper;

    public App()
    {
        _bootstrapper = new Bootstrapper().Setup();
    }


    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = _bootstrapper.GetRoot()
            };

            desktop.ShutdownRequested += OnShutdownRequested;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = _bootstrapper.GetRoot()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        _bootstrapper.Dispose();
    }
}
