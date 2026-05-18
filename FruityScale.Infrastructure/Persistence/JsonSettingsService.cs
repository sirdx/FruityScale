using System.Text.Json;
using FruityScale.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace FruityScale.Infrastructure.Persistence;

public class JsonSettingsService : ISettingsService
{
    private readonly ILogger<JsonSettingsService> _logger;
    private readonly string _configPath;
    private string _cachedPath = string.Empty;

    public JsonSettingsService(ILogger<JsonSettingsService> logger)
    {
        _logger = logger;
        
        // TODO: change .fruityscale (or directory in general) to something different
        var appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".fruityscale");
        Directory.CreateDirectory(appFolder);
        _configPath = Path.Combine(appFolder, "config.json");
    }

    public string GetFlStudioPath()
    {
        if (!string.IsNullOrEmpty(_cachedPath)) return _cachedPath;
        if (!File.Exists(_configPath)) return string.Empty;

        try
        {
            var json = File.ReadAllText(_configPath);
            using var doc = JsonDocument.Parse(json);
            _cachedPath = doc.RootElement.GetProperty("FlStudioPath").GetString() ?? string.Empty;
            _logger.LogInformation("Loaded FL Studio path from config: {Path}", _cachedPath);
            
            return _cachedPath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading FL Studio path from config file.");
            return string.Empty;
        }
    }

    public void SaveFlStudioPath(string path)
    {
        _cachedPath = path;
        
        try
        {
            var json = JsonSerializer.Serialize(new { FlStudioPath = path });
            File.WriteAllText(_configPath, json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save FL Studio path to {ConfigPath}", _configPath);
        }
    }
}