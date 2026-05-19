namespace FruityScale.Domain.MusicTheory;

public static class NoteNameNormalizer
{
    // Note name
    // Return examples: C, C#, D
    public static string GetNoteName(int noteNumber)
    {
        return MusicConstants.NoteNamesSharp[noteNumber % MusicConstants.NotesInOctave];
    }
    
    // Note name with octave if we ever need it
    // Return examples: C5, C#7, D2
    public static string GetFullNoteName(int noteNumber)
    {
        int octave = noteNumber / MusicConstants.NotesInOctave;
        return $"{GetNoteName(noteNumber)}{octave}";
    }
}