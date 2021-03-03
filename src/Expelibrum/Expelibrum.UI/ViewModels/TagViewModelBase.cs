using Expelibrum.Model;
using Expelibrum.Services.Events;
using Expelibrum.UI.Events;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public class TagViewModelBase : ViewModelBase
    {
        #region fields

        protected IEventAggregator _ea;
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

        protected virtual bool CanRemoveTag(object param)
        {
            return tagCount > 1;
        }

        protected virtual void OnRemoveTag(object param)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region constructors

        public TagViewModelBase(int id, IEventAggregator ea)
        {
            Id = id;
            _ea = ea;

            SubscribeToEvents();

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

        protected virtual void SubscribeToEvents()
        {
            throw new NotImplementedException();
        }

        protected void OnTagCountChanged(EventArgs e)
        {
            var args = e as TagCountChangedEventArgs;
            tagCount = args.Count;
        }

        #endregion
    }
}
