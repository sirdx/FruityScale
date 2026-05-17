using FruityScale.Application.Contracts;

namespace FruityScale.Infrastructure.Services;

public class FlStudioSetupService : ISetupService
{
    public bool ValidateAndSetup(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            return false;

        try
        {
            string targetScriptsFolder = ResolveScriptsFolder(path);

            if (targetScriptsFolder == null)
                return false;

            Directory.CreateDirectory(targetScriptsFolder);

            // TODO: change this .pyscript when i add it to project
            string sourceScriptPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources",
                "FlStudioNotesExporter.pyscript"
            );

            string targetScriptPath = Path.Combine(
                targetScriptsFolder,
                "FlStudioNotesExporter.pyscript"
            );

            if (!File.Exists(sourceScriptPath))
                return false;

            File.Copy(sourceScriptPath, targetScriptPath, overwrite: true);

            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public string GetNotesJsonPath(string path)
    {
        string targetScriptsFolder = ResolveScriptsFolder(path);

        if (targetScriptsFolder == null)
            throw new ArgumentException("Invalid FL Studio path.");

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