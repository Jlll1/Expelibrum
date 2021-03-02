using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public interface IDirectoryTaggingViewModel
    {
        ICommand AddTagCommand { get; }
        IEnumerable<string> SelectedTags { get; }
        ObservableCollection<DirectoryTagViewModel> TagVMs { get; }
    }
}