using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FruityScale.Application.Contracts;
using FruityScale.Application.Services;
using FruityScale.Domain.Models;

namespace FruityScale.Presentation.ViewModels;

public partial class MainDashboardViewModel : ViewModelBase
{
    private readonly ScaleMatchingOrchestrator _orchestrator;
    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    private string _dawPath;

    [ObservableProperty]
    private bool _isScanning;

    [ObservableProperty]
    private string _statusMessage = "Ready to scan. Run FlStudioNotesExporter script in FL studio, and click Scan.";
    
    public ObservableCollection<ScaleMatchResult> ScanResults { get; } = new();

    public MainDashboardViewModel(
        ScaleMatchingOrchestrator orchestrator, 
        ISettingsService settingsService)
    {
        _orchestrator = orchestrator;
        _settingsService = settingsService;
        
        _dawPath = _settingsService.GetFlStudioPath();
    }
    
    [RelayCommand]
    private async Task ScanNotesAsync()
    {
        IsScanning = true;
        StatusMessage = "Scanning and matching scales...";
        ScanResults.Clear();

        try
        {
            var results = await _orchestrator.GetMatchesAsync();
            
            int count = 0;
            foreach (var match in results)
            {
                ScanResults.Add(match);
                count++;
                if (count >= 20) break;
            }

            StatusMessage = ScanResults.Count == 0 ? 
                "notes.json file not found or it's empty. Are you sure you have run script in FL Studio?"
                : $"Success! Found {ScanResults.Count} matches.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Scanning error: {ex.Message}";
        }
        finally
        {
            IsScanning = false;
        }
    }
}