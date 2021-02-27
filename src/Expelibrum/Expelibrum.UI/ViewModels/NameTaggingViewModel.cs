using Expelibrum.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Expelibrum.UI.ViewModels
{
    public class NameTaggingViewModel : ViewModelBase, INameTaggingViewModel
    {
        public ObservableCollection<TagViewModel> TagVMs { get; }
        public IEnumerable<string> SelectedTags
        {
            get
            {
                foreach (var tagVM in TagVMs)
                {
                    yield return tagVM.SelectedTag.PropertyName;
                }
            }
        }

        public NameTaggingViewModel()
        {
            TagVMs = new ObservableCollection<TagViewModel>();
            TagVMs.Add(new TagViewModel());
        }
    }
}
