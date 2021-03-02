using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public interface IProcessViewModel
    {
        IDirectorySettingsViewModel DirectorySettings { get; }
        IDirectoryTaggingViewModel DirectoryTaggingViewModel { get; }
        INameTaggingViewModel NameTaggingViewModel { get; }
        ICommand ProcessFilesCommand { get; }
    }
}