namespace FruityScale.Domain.Models;

public record ScaleDefinition(
    string Name,
    int[] Intervals,
    string? Description = null);