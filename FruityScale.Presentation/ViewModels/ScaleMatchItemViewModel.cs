using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FruityScale.Domain.Models;
using FruityScale.Domain.MusicTheory;

namespace FruityScale.Presentation.ViewModels;

public partial class ScaleMatchItemViewModel : ObservableObject
{
    // It might be possible to run something like JsonNoteProvider, although it need path as parameter...
    private static readonly string[] NoteNames =
        { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    
    public ScaleMatchResult Result { get; }

    [ObservableProperty]
    private bool _isExpanded;
    
    public List<string> ScaleNotes { get; }
    public List<string> WrongNotes { get; }
    public List<string> MissingNotes { get; }
    public List<string> UserNotes { get; }
    
    public bool HasWrongNotes => WrongNotes.Any();
    public bool HasMissingNotes => MissingNotes.Any();

    public ScaleMatchItemViewModel(ScaleMatchResult result)
    {
        Result = result;
        
        ScaleNotes = result.Scale.Intervals
            .Select(interval => NoteNameNormalizer.GetNoteName(result.RootNote + interval))
            .Distinct()
            .ToList();
        
        WrongNotes = result.WrongNotes?
            .Select(noteNum => NoteNameNormalizer.GetNoteName(noteNum))
            .Distinct()
            .ToList() ?? new List<string>();
        
        MissingNotes = result.MissingNotes?
            .Select(noteNum => NoteNameNormalizer.GetNoteName(noteNum))
            .Distinct()
            .ToList() ?? new List<string>();
        
        var correctPlayedNotes = ScaleNotes.Except(MissingNotes);
        UserNotes = correctPlayedNotes
            .Union(WrongNotes)
            .OrderBy(note => Array.IndexOf(NoteNames, note))
            .ToList();
    }

    [RelayCommand]
    private void ToggleExpand()
    {
        IsExpanded = !IsExpanded;
    }
}

