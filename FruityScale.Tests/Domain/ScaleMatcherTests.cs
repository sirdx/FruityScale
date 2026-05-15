using FruityScale.Domain.Models;
using FruityScale.Domain.MusicTheory;

namespace FruityScale.Tests.Domain;

public class ScaleMatcherTests
{
    private readonly ScaleMatcher _sut;
    private readonly List<ScaleDefinition> _mockLibrary;

    public ScaleMatcherTests()
    {
        _sut = new ScaleMatcher();
        
        // Setup a basic library with Major (0, 2, 4, 5, 7, 9, 11) 
        // and Minor (0, 2, 3, 5, 7, 8, 10) scales
        _mockLibrary = new List<ScaleDefinition>
        {
            new ScaleDefinition("Major", new[] { 0, 2, 4, 5, 7, 9, 11 }),
            new ScaleDefinition("Minor", new[] { 0, 2, 3, 5, 7, 8, 10 })
        };
    }

    [Fact]
    public void Match_ShouldReturnEmpty_WhenUserNotesIsEmpty()
    {
        // Arrange
        var userNotes = Enumerable.Empty<int>();

        // Act
        var results = _sut.Match(userNotes, _mockLibrary);

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void Match_ShouldFindPerfectMatch_RegardlessOfOctave()
    {
        // Arrange
        // C Major notes: C (0), E (4), G (7) - using different octaves
        var userNotes = new[] { 0, 16, 31 }; // 0, 4 (16%12), 7 (31%12)

        // Act
        var results = _sut.Match(userNotes, _mockLibrary).ToList();

        // Assert
        // The first result should be C Major with a score of 1.0 (100%)
        var topMatch = results.First();
        Assert.Equal("Major", topMatch.Scale.Name);
        Assert.Equal(0, topMatch.RootNote);
        Assert.Equal(1.0, topMatch.Score);
        Assert.Empty(topMatch.WrongNotes);
    }

    [Fact]
    public void Match_ShouldIdentifyWrongNotes_WhenScaleDoesNotFullyCoverInput()
    {
        // Arrange
        // Input: C (0), E (4), G (7), and F# (6). 
        // F# is not in C Major.
        var userNotes = new[] { 0, 4, 7, 6 }; 

        // Act
        var results = _sut.Match(userNotes, _mockLibrary).ToList();

        // Assert
        var cMajorMatch = results.First(r => r.Scale.Name == "Major" && r.RootNote == 0);
        Assert.Contains(6, cMajorMatch.WrongNotes);
        Assert.Equal(0.75, cMajorMatch.Score); // 3 out of 4 notes match
    }

    [Fact]
    public void Match_ShouldIdentifyMissingNotes_FromScaleDefinition()
    {
        // Arrange
        // Input: C (0) and G (7). 
        // Missing from C Major: 2, 4, 5, 9, 11
        var userNotes = new[] { 0, 7 };

        // Act
        var results = _sut.Match(userNotes, _mockLibrary).ToList();

        // Assert
        var cMajorMatch = results.First(r => r.Scale.Name == "Major" && r.RootNote == 0);
        Assert.Contains(2, cMajorMatch.MissingNotes);
        Assert.Contains(4, cMajorMatch.MissingNotes);
        Assert.Equal(1.0, cMajorMatch.Score); // All input notes are in the scale
    }

    [Fact]
    public void Match_ShouldDetectTransposedScales()
    {
        // Arrange
        // D Major is C Major (0, 2, 4...) transposed by +2: (2, 4, 6, 7, 9, 11, 1)
        var userNotes = new[] { 2, 6, 9 }; // D, F#, A

        // Act
        var results = _sut.Match(userNotes, _mockLibrary).ToList();

        // Assert
        var topMatch = results.First();
        Assert.Equal("Major", topMatch.Scale.Name);
        Assert.Equal(2, topMatch.RootNote); // Root note should be 2 (D)
        Assert.Equal(1.0, topMatch.Score);
    }

    [Fact]
    public void Match_ShouldOrderResultsByScoreDescending()
    {
        // Arrange
        // Notes that fit Minor better than Major
        var userNotes = new[] { 0, 3, 7 }; // C, Eb, G

        // Act
        var results = _sut.Match(userNotes, _mockLibrary).ToList();

        // Assert
        Assert.True(results[0].Score >= results[1].Score);
    }
}