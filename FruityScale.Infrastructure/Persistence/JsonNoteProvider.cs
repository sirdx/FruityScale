using System.Text.Json;
using FruityScale.Application.Contracts;
using FruityScale.Domain.Models;

namespace FruityScale.Infrastructure.Persistence;

public class JsonNoteProvider : INoteProvider
{
    public async Task<List<NoteEvent>> LoadNotesAsync(string filePath)
    {
        await using var stream = File.OpenRead(filePath);
        
        // Ignore case
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return await JsonSerializer.DeserializeAsync<List<NoteEvent>>(stream, options) ?? new();
    }
}