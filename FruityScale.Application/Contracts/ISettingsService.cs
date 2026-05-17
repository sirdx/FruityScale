namespace FruityScale.Application.Contracts;

public interface ISettingsService
{
    // TODO: make these methods general (example: GetDawPath, SaveDawPath)
    string GetFlStudioPath();
    void SaveFlStudioPath(string path);
}