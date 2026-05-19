using FruityScale.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace FruityScale.Infrastructure.Services;

public class FlStudioSetupService : ISetupService
{
    private readonly ILogger<FlStudioSetupService> _logger;

    public FlStudioSetupService(ILogger<FlStudioSetupService> logger)
    {
        _logger = logger;
    }
    
    public bool ValidateAndSetup(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
        {
            _logger.LogWarning("Validation failed: Provided path is empty or does not exist: {Path}", path);
            return false;
        }

        try
        {
            string targetScriptsFolder = ResolveScriptsFolder(path);

            if (targetScriptsFolder == null)
            {
                _logger.LogWarning("Validation failed: Could not resolve FL Studio scripts folder from path: {Path}", path);
                return false;
            }

            Directory.CreateDirectory(targetScriptsFolder);
            
            // path we get the script from
            string sourceScriptPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources",
                "FlStudioNotesExporter.pyscript"
            );

            // path we copy the script to
            string targetScriptPath = Path.Combine(
                targetScriptsFolder,
                "FlStudioNotesExporter.pyscript"
            );

            if (!File.Exists(sourceScriptPath))
            {
                _logger.LogError("Setup failed: Source script file not found at: {SourcePath}", sourceScriptPath);
                return false;
            }

            File.Copy(sourceScriptPath, targetScriptPath, overwrite: true);
            
            _logger.LogInformation("Successfully deployed FL Studio script to: {TargetPath}", targetScriptPath);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred during FL Studio validation and setup.");
            return false;
        }
    }
    
    public string GetNotesJsonPath(string path)
    {
        string targetScriptsFolder = ResolveScriptsFolder(path);

        if (targetScriptsFolder == null)
        {
            _logger.LogError("Failed to get notes JSON path. Invalid FL Studio path: {Path}", path);
            throw new ArgumentException("Invalid FL Studio path.");
        }

        return Path.Combine(targetScriptsFolder, "notes.json");
    }
    
    private string? ResolveScriptsFolder(string path)
    {
        string folderName = new DirectoryInfo(path).Name;

        if (folderName.Equals("FL Studio", StringComparison.OrdinalIgnoreCase))
        {
            return Path.Combine(
                path,
                "Settings",
                "Piano roll scripts"
            );
        }

        if (folderName.Equals("Piano roll scripts", StringComparison.OrdinalIgnoreCase))
        {
            return path;
        }

        return null;
    }
}