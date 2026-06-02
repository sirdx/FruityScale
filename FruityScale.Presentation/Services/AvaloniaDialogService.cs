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
}