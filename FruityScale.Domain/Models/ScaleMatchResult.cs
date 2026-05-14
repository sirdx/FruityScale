namespace FruityScale.Domain.Models;

public record ScaleMatchResult(
    ScaleDefinition Scale,
    int RootNote,
    double Score, // Score will be used for sorting scales in UI, although there is also another idea that it will be calculated based on wrong notes and missing notes so it's not inside model.
    List<int>? WrongNotes,
    List<int>? MissingNotes);