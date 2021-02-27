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
        #region fields

        private IEventAggregator _ea;
        private Tag _selectedTag;
        private int tagCount;

        #endregion

        #region properties

        public int Id { get; }
        public List<Tag> Tags { get; }
        public Tag SelectedTag
        {
            get => _selectedTag;
            set
            {
                _selectedTag = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region commands

        public ICommand RemoveTagCommand { get; }

        #region commandmethods

        private bool CanRemoveTag(object param)
        {
            return tagCount > 1;
        }

        private void OnRemoveTag(object param)
        {
            _ea.PublishEvent("TagRemoveRequested", new TagRemoveRequestedEventArgs { Id = this.Id });
        }

        #endregion

        #endregion

        #region constructors

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

        #endregion

        #region methods

        private void OnTagCountChanged(EventArgs e)
        {
            var args = e as TagCountChangedEventArgs;
            tagCount = args.Count;
        }

        #endregion
    }
}
