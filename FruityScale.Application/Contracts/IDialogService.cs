namespace FruityScale.Application.Contracts;

public interface IDialogService
{
    Task<string?> SelectFolderAsync(string title);
}