using Expelibrum.Model;
using System.Collections.Generic;

namespace Expelibrum.UI.ViewModels
{
    public class TagViewModel : ViewModelBase
    {
        private Tag _selectedTag;

        public Tag SelectedTag
        {
            get => _selectedTag;
            set 
            {
                _selectedTag = value;
                OnPropertyChanged();
            }
        }

        public List<Tag> Tags { get; set; }

        public TagViewModel()
        {
            Tags = new List<Tag>
            {
                new Tag("Title", "title"),
                new Tag("Author", "authors"),
                new Tag("PageCount", "number_of_pages"),
                new Tag("Publisher", "publishers"),
                new Tag("PublishDate", "publish_date"),
                new Tag("Subject", "subjects")
            };

            SelectedTag = Tags[0];
        }
    }
}
