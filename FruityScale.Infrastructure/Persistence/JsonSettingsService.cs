using System.Text.Json;
using FruityScale.Application.Contracts;

namespace FruityScale.Infrastructure.Persistence;

public class JsonSettingsService : ISettingsService
{
    private readonly string _configPath;
    private string _cachedPath = string.Empty;

    public JsonSettingsService()
    {
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
            return _cachedPath;
        }
        catch
        {
            return string.Empty;
        }
    }

    public void SaveFlStudioPath(string path)
    {
        _cachedPath = path;
        var configData = new { FlStudioPath = path };
        var json = JsonSerializer.Serialize(configData);
        File.WriteAllText(_configPath, json);
    }
}