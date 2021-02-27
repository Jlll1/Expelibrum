using Expelibrum.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Expelibrum.UI.ViewModels
{
    public interface INameTaggingViewModel
    {
        IEnumerable<string> SelectedTags { get; }
        ObservableCollection<TagViewModel> TagVMs { get; }
    }
}