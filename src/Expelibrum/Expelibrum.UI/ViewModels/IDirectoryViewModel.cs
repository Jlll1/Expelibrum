using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public interface IDirectoryViewModel
    {
        ICommand ChangeOriginDirectoryPathCommand { get; }
        ICommand ChangeTargetDirectoryPathCommand { get; }
        string OriginDirectoryPath { get; set; }
        string TargetDirectoryPath { get; set; }
    }
}