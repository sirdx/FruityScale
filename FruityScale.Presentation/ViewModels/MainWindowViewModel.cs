using CommunityToolkit.Mvvm.ComponentModel;
using FruityScale.Application.Contracts;
using FruityScale.Application.Services;

namespace FruityScale.Presentation.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ISettingsService _settingsService;
    private readonly ISetupService _setupService;
    private readonly ScaleMatchingOrchestrator _orchestrator;
    
    [ObservableProperty]
    private ViewModelBase? _currentContent;
    
    public MainWindowViewModel(
        ISettingsService settingsService, 
        ISetupService setupService, 
        ScaleMatchingOrchestrator orchestrator)
    {
        _settingsService = settingsService;
        _setupService = setupService;
        _orchestrator = orchestrator;

        DetermineInitialView();
    }
    
    public void DetermineInitialView()
    {
        string currentPath = _settingsService.GetFlStudioPath();

        if (string.IsNullOrEmpty(currentPath))
        {
            CurrentContent = new SetupViewModel(this, _setupService, _settingsService);
        }
        else
        {
            CurrentContent = new MainDashboardViewModel(_orchestrator, _settingsService);
        }
    }
}