using FruityScale.Domain.MusicTheory;

namespace FruityScale.Tests.Domain;

public class NoteNameNormalizerTests
{
    [Theory]
    [InlineData(0, "C")]    // First note
    [InlineData(1, "C#")]   // Sharp note
    [InlineData(11, "B")]   // Last note in octave
    [InlineData(12, "C")]   // Wrap around (Octave 1)
    [InlineData(25, "C#")]  // Wrap around (Octave 2)
    public void GetNoteName_ShouldReturnCorrectName_ForGivenNoteNumbers(int noteNumber, string expectedName)
    {
        // Arrange
        // (Inputs provided by InlineData)

        // Act
        var result = NoteNameNormalizer.GetNoteName(noteNumber);

        // Assert
        Assert.Equal(expectedName, result);
    }

    [Theory]
    [InlineData(0, "C0")]     // Start of MIDI / Octave 0
    [InlineData(12, "C1")]    // Start of Octave 1
    [InlineData(13, "C#1")]   // Sharp in Octave 1
    [InlineData(60, "C5")]    // Middle C (FL Studio convention)
    [InlineData(127, "G10")]  // Standard MIDI Max
    public void GetFullNoteName_ShouldReturnNameWithOctave_ForGivenNoteNumbers(int noteNumber, string expectedFullName)
    {
        // Arrange
        // (Inputs provided by InlineData)

        // Act
        var result = NoteNameNormalizer.GetFullNoteName(noteNumber);

        // Assert
        Assert.Equal(expectedFullName, result);
    }

    [Fact]
    public void GetNoteName_ShouldHandleLargeNoteNumbers_UsingModulo()
    {
        // Arrange
        int highNote = 1200; // 100th octave C
        string expected = "C";

        // Act
        var result = NoteNameNormalizer.GetNoteName(highNote);

        // Assert
        Assert.Equal(expected, result);
    }
}