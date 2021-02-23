using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public interface IProcessViewModel
    {
        ICommand ProcessFilesCommand { get; }
    }
}