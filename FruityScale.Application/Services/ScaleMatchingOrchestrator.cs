using FruityScale.Application.Contracts;
using FruityScale.Domain.Models;
using FruityScale.Domain.Services;

namespace FruityScale.Application.Services;

public class ScaleMatchingOrchestrator
{
    private readonly IScaleMatcher _scaleMatcher;
    private readonly IScaleProvider _scaleProvider;
    private readonly INoteProvider _noteProvider;
    
    public ScaleMatchingOrchestrator(
        IScaleMatcher scaleMatcher, 
        IScaleProvider scaleProvider, 
        INoteProvider noteProvider)
    {
        _scaleMatcher = scaleMatcher;
        _scaleProvider = scaleProvider;
        _noteProvider = noteProvider;
    }

    public async Task<IEnumerable<ScaleMatchResult>> GetMatchesAsync(string flStudioFilePath)
    {
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