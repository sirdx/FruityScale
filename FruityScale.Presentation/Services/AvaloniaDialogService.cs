using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using FruityScale.Application.Contracts;

namespace FruityScale.Presentation.Services;

public class AvaloniaDialogService : IDialogService
{
    private readonly Func<IStorageProvider?> _storageProviderResolver;
    
    public AvaloniaDialogService()
    {
        _storageProviderResolver = () =>
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop 
                && desktop.MainWindow != null)
            {
                return Avalonia.Controls.TopLevel.GetTopLevel(desktop.MainWindow)?.StorageProvider;
            }
            return null;
        };
    }
    
    // internal constructor needed for tests
    internal AvaloniaDialogService(Func<IStorageProvider?> storageProviderResolver)
    {
        _storageProviderResolver = storageProviderResolver;
    }

    public async Task<string?> SelectFolderAsync(string title)
    {
        var storageProvider = _storageProviderResolver();
        if (storageProvider == null) return null;

        var folders = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = title,
            AllowMultiple = false
        });

        if (folders.Count > 0)
        {
            return folders[0].Path.LocalPath;
        }

        return null;
    }
}