namespace FruityScale.Domain.MusicTheory;

public static class MusicConstants
{
    public const int NotesInOctave = 12;
    
    public static readonly string[] NoteNamesSharp = new[] 
    { 
        "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" 
    };

    // TODO: we dont need it yet but will be useful when we add user settings (maybe someone want to see Db instead of c#)
    public static readonly string[] NoteNamesFlat = new[] 
    { 
        "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B" 
    };
}