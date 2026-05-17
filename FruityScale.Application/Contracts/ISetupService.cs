namespace FruityScale.Application.Contracts;

public interface ISetupService
{
    bool ValidateAndSetup(string path);
    string GetNotesJsonPath(string basePath);
}