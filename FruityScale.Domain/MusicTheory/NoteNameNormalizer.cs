namespace FruityScale.Domain.MusicTheory;

public static class NoteNameNormalizer
{
    // We are following FL Studio's naming convention style
    // We will use hardcoded names for now because our main target is FL Studio
    // There are couple other possible options for naming but this can be added later
    private static readonly string[] NoteNames =
        { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    
    // Note name
    // Return examples: C, C#, D
    public static string GetNoteName(int noteNumber)
    {
        return NoteNames[noteNumber % 12];
    }
    
    // Note name with octave if we ever need it
    // Return examples: C5, C#7, D2
    public static string GetFullNoteName(int noteNumber)
    {
        int octave = (noteNumber / 12);
        return $"{GetNoteName(noteNumber)}{octave}";
    }
}