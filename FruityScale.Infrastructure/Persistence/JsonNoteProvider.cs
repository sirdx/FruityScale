using System.Text.Json;
using FruityScale.Application.Contracts;
using FruityScale.Domain.Models;

namespace FruityScale.Infrastructure.Persistence;

public class JsonNoteProvider : INoteProvider
{
    // TODO: filePath shouldn't be parameter. The best option is to get filepath const from something like appsettings.json
    public async Task<List<NoteEvent>> LoadNotesAsync(string filePath)
    {
        //if (!File.Exists(filePath)) return new List<NoteEvent>();
        
        await using var stream = File.OpenRead(filePath);
        
        // Ignore case
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return await JsonSerializer.DeserializeAsync<List<NoteEvent>>(stream, options) ?? new();
    }
}