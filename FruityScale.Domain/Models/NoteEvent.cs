using System.Text.Json.Serialization;

namespace FruityScale.Domain.Models;

// TODO: It's needed because of .pyscript json structure... but it can be made better i guess
public record NoteEvent(
    [property: JsonPropertyName("note_number")] int NoteNumber,
    string Name,
    string Key);