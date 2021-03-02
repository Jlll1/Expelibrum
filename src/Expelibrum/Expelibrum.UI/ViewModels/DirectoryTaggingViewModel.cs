using Expelibrum.Services.Events;
using Expelibrum.UI.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public class DirectoryTaggingViewModel : ViewModelBase, IDirectoryTaggingViewModel
    {
        #region fields

        private IEventAggregator _ea;

        #endregion

        #region properties

        public ObservableCollection<DirectoryTagViewModel> TagVMs { get; }
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

        #endregion

        #region commands

        public ICommand AddTagCommand { get; }

        #region commandmethods

        private void OnAddTag(object param)
        {
            AddTag();
        }

        #endregion

        #endregion

        #region constructors

        public DirectoryTaggingViewModel(IEventAggregator ea)
        {
            _ea = ea;

            _ea.SubscribeToEvent("DirectoryTagRemoveRequested", OnTagRemoveRequested);

            TagVMs = new ObservableCollection<DirectoryTagViewModel>();
            AddTag();

            AddTagCommand = new RelayCommand(OnAddTag);
        }

        #endregion

        #region methods

        private void OnTagRemoveRequested(EventArgs e)
        {
            var args = e as TagRemoveRequestedEventArgs;
            RemoveTag(args.Id);
        }

        private void AddTag()
        {
            TagVMs.Add(new DirectoryTagViewModel(TagVMs.Count, _ea));
            _ea.PublishEvent("DirectoryTagCountChanged", new TagCountChangedEventArgs() { Count = TagVMs.Count });
        }

        private void RemoveTag(int id)
        {
            TagVMs.RemoveAt(id);
            _ea.PublishEvent("DirectoryTagCountChanged", new TagCountChangedEventArgs() { Count = TagVMs.Count });
        }

        #endregion
    }
}
