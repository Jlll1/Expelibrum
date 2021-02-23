using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public interface IDirectorySettingsViewModel
    {
        ICommand ChangeOriginDirectoryPathCommand { get; }
        ICommand ChangeTargetDirectoryPathCommand { get; }
        bool IncludeSubdirectories { get; set; }
        string OriginDirectoryPath { get; set; }
        string TargetDirectoryPath { get; set; }
    }
}