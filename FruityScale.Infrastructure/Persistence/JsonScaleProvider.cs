using System.Text.Json;
using FruityScale.Application.Contracts;
using FruityScale.Domain.Models;

namespace FruityScale.Infrastructure.Persistence;

public class JsonScaleProvider : IScaleProvider
{
    // TODO: filePath shouldn't be passed as param. The best option is to get filepath const from something like appsettings.json
    public async Task<List<ScaleDefinition>> GetScalesAsync(string filePath)
    {
        //if (!File.Exists(filePath)) return new List<ScaleDefinition>();
        
        await using var stream = File.OpenRead(filePath);
        
        // Ignore case
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return await JsonSerializer.DeserializeAsync<List<ScaleDefinition>>(stream, options) ?? new();
    }
}