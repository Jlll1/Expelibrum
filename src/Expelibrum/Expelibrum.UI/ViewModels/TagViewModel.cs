using Expelibrum.Model;
using Expelibrum.Services.Events;
using Expelibrum.UI.Events;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public class TagViewModel : ViewModelBase
    {
        private IEventAggregator _ea;
        private Tag _selectedTag;
        private int tagCount;

        public Tag SelectedTag
        {
            get => _selectedTag;
            set 
            {
                _selectedTag = value;
                OnPropertyChanged();
            }
        }

        public List<Tag> Tags { get; }
        public int Id { get; }

        public ICommand RemoveTagCommand { get; }

        private bool CanRemoveTag(object param)
        {
            return tagCount > 1;
        }

        private void OnRemoveTag(object param)
        {
            _ea.PublishEvent("TagRemoveRequested", new TagRemoveRequestedEventArgs { Id = this.Id });
        }

        public TagViewModel(int id, IEventAggregator ea)
        {
            Id = id;
            _ea = ea;

            _ea.SubscribeToEvent("TagCountChanged", OnTagCountChanged);

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
            RemoveTagCommand = new RelayCommand(OnRemoveTag, CanRemoveTag);
        }

        private void OnTagCountChanged(EventArgs e)
        {
            var args = e as TagCountChangedEventArgs;
            tagCount = args.Count;
        }
    }
}
