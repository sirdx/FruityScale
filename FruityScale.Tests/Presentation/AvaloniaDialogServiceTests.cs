using Avalonia.Platform.Storage;
using FruityScale.Presentation.Services;
using NSubstitute;

namespace FruityScale.Tests.Presentation;

public class AvaloniaDialogServiceTests
{
    [Fact]
    public async Task SelectFolderAsync_ShouldReturnNull_WhenStorageProviderIsUnavailable()
    {
        // Arrange
        var sut = new AvaloniaDialogService(() => null);

        // Act
        var result = await sut.SelectFolderAsync("Select Folder");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectFolderAsync_ShouldReturnNull_WhenNoFolderIsSelected()
    {
        // Arrange
        var mockStorageProvider = Substitute.For<IStorageProvider>();
        
        mockStorageProvider.OpenFolderPickerAsync(Arg.Any<FolderPickerOpenOptions>())
            .Returns(Task.FromResult<IReadOnlyList<IStorageFolder>>(new List<IStorageFolder>()));

        var sut = new AvaloniaDialogService(() => mockStorageProvider);

        // Act
        var result = await sut.SelectFolderAsync("Select Folder");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectFolderAsync_ShouldReturnLocalPath_WhenValidFolderIsSelected()
    {
        // Arrange
        // Generally it should work on every platform
        string expectedPath = OperatingSystem.IsWindows() 
            ? @"C:\Music\FL Studio Projects" 
            : "/Music/FL Studio Projects";

        var mockFolder = Substitute.For<IStorageFolder>();
        
        mockFolder.Path.Returns(new Uri(expectedPath));

        var mockStorageProvider = Substitute.For<IStorageProvider>();
        mockStorageProvider.OpenFolderPickerAsync(Arg.Is<FolderPickerOpenOptions>(o => o.Title == "Select Beats"))
            .Returns(Task.FromResult<IReadOnlyList<IStorageFolder>>(new List<IStorageFolder> { mockFolder }));

        var sut = new AvaloniaDialogService(() => mockStorageProvider);

        // Act
        var result = await sut.SelectFolderAsync("Select Beats");

        // Assert
        Assert.Equal(expectedPath, result);
    }
}