using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FruityScale.Application.Services;
using FruityScale.Domain.MusicTheory;
using FruityScale.Infrastructure.Persistence;
using FruityScale.Infrastructure.Services;
using FruityScale.Presentation.ViewModels;
using FruityScale.Presentation.Views;

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
            var scaleMatcher = new ScaleMatcher();
            var scaleProvider = new JsonScaleProvider();
            var noteProvider = new JsonNoteProvider();
            var settingsService = new JsonSettingsService();
            var setupService = new FlStudioSetupService();

            var orchestrator = new ScaleMatchingOrchestrator(
                scaleMatcher,
                scaleProvider,
                noteProvider,
                settingsService,
                setupService
            );
            
            var mainWindowViewModel = new MainWindowViewModel(
                settingsService,
                setupService,
                orchestrator
            );
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}