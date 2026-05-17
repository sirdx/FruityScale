using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FruityScale.Application.Contracts;

namespace FruityScale.Presentation.ViewModels;

public partial class SetupViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainNavigation;
    private readonly ISetupService _setupService;
    private readonly ISettingsService _settingsService;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ConfirmPathCommand))]
    private string _selectedPath = string.Empty;
    
    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public SetupViewModel(
        MainWindowViewModel mainNavigation,
        ISetupService setupService,
        ISettingsService settingsService)
    {
        _mainNavigation = mainNavigation;
        _setupService = setupService;
        _settingsService = settingsService;
    }
    
    [RelayCommand(CanExecute = nameof(CanConfirm))]
    private void ConfirmPath()
    {
        ErrorMessage = string.Empty;
        
        bool success = _setupService.ValidateAndSetup(SelectedPath);

        if (success)
        {
            _settingsService.SaveFlStudioPath(SelectedPath);
            
            // Inform MainWindowViewModel to change view after correct setup
            _mainNavigation.DetermineInitialView();
        }
        else
        {
            ErrorMessage = "Directory is invalid or script installation failed. Make sure you have selected correct directory.";
        }
    }

    private bool CanConfirm() => !string.IsNullOrWhiteSpace(SelectedPath);
}