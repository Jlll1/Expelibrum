using Expelibrum.Services.Events;
using Expelibrum.UI.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Expelibrum.UI.ViewModels
{
    public class NameTaggingViewModel : ViewModelBase, INameTaggingViewModel
    {
        #region fields

        private IEventAggregator _ea;

        #endregion

        #region properties

        public ObservableCollection<NameTagViewModel> TagVMs { get; }
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

        public NameTaggingViewModel(IEventAggregator ea)
        {
            _ea = ea;

            _ea.SubscribeToEvent("NameTagRemoveRequested", OnTagRemoveRequested);

            TagVMs = new ObservableCollection<NameTagViewModel>();
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
            TagVMs.Add(new NameTagViewModel(TagVMs.Count, _ea));
            _ea.PublishEvent("NameTagCountChanged", new TagCountChangedEventArgs() { Count = TagVMs.Count });
        }

        private void RemoveTag(int id)
        {
            TagVMs.RemoveAt(id);
            _ea.PublishEvent("NameTagCountChanged", new TagCountChangedEventArgs() { Count = TagVMs.Count });
        }

        #endregion
    }
}
