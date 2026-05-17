using FruityScale.Application.Contracts;
using FruityScale.Domain.Models;
using FruityScale.Domain.Services;

namespace FruityScale.Application.Services;

public class ScaleMatchingOrchestrator
{
    private readonly IScaleMatcher _scaleMatcher;
    private readonly IScaleProvider _scaleProvider;
    private readonly INoteProvider _noteProvider;
    private readonly ISettingsService _settingsService;
    private readonly ISetupService _setupService;
    
    public ScaleMatchingOrchestrator(
        IScaleMatcher scaleMatcher, 
        IScaleProvider scaleProvider, 
        INoteProvider noteProvider,
        ISettingsService settingsService,
        ISetupService setupService)
    {
        _scaleMatcher = scaleMatcher;
        _scaleProvider = scaleProvider;
        _noteProvider = noteProvider;
        _settingsService = settingsService;
        _setupService = setupService;
    }

    public async Task<IEnumerable<ScaleMatchResult>> GetMatchesAsync()
    {
        string flPath = _settingsService.GetFlStudioPath();
        if (string.IsNullOrEmpty(flPath)) return Enumerable.Empty<ScaleMatchResult>();
        
        string flStudioFilePath = _setupService.GetNotesJsonPath(flPath);
        
        if (!File.Exists(flStudioFilePath)) return Enumerable.Empty<ScaleMatchResult>();
        
        string scaleLibraryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "scale_library.json");
        
        var scalesTask = _scaleProvider.GetScalesAsync(scaleLibraryPath);
        var notesTask = _noteProvider.LoadNotesAsync(flStudioFilePath);
        
        await Task.WhenAll(scalesTask, notesTask);
        
        var allScales = await scalesTask;
        var userNotes = await notesTask;
        
        // TODO: do something with the warning
        if (userNotes == null || !userNotes.Any())
        {
            return Enumerable.Empty<ScaleMatchResult>();
        }
        
        var noteNumbers = userNotes
            .Select(n => n.NoteNumber)
            .Distinct()
            .ToList();
        
        var results = _scaleMatcher.Match(noteNumbers, allScales);
        
        return results.OrderByDescending(r => r.Score);
    }
}