using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FruityScale.Application.Contracts;
using FruityScale.Application.Services;
using FruityScale.Domain.MusicTheory;
using FruityScale.Domain.Services;
using FruityScale.Infrastructure.Persistence;
using FruityScale.Infrastructure.Services;
using FruityScale.Presentation.ViewModels;
using FruityScale.Presentation.Views;
using Microsoft.Extensions.DependencyInjection;

namespace FruityScale;

public partial class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var services = new ServiceCollection();
            
            services.AddSingleton<IScaleMatcher, ScaleMatcher>();
            services.AddSingleton<IScaleProvider, JsonScaleProvider>();
            services.AddSingleton<INoteProvider, JsonNoteProvider>();
            services.AddSingleton<ISettingsService, JsonSettingsService>();
            services.AddSingleton<ISetupService, FlStudioSetupService>();
            
            services.AddSingleton<ScaleMatchingOrchestrator>();
            
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainDashboardViewModel>();
            
            var serviceProvider = services.BuildServiceProvider();
            
            var mainWindowViewModel = serviceProvider.GetRequiredService<MainWindowViewModel>();
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}