using Expelibrum.Services.Events;
using Expelibrum.UI.Events;

namespace Expelibrum.UI.ViewModels
{
    public class DirectoryTagViewModel : TagViewModelBase
    {
        public DirectoryTagViewModel(int id, IEventAggregator ea) : base (id, ea)
        {
        }

        protected override void SubscribeToEvents()
        {
            _ea.SubscribeToEvent("DirectoryTagCountChanged", base.OnTagCountChanged);
        }

        protected override void OnRemoveTag(object param)
        {
            _ea.PublishEvent("DirectoryTagRemoveRequested", new TagRemoveRequestedEventArgs { Id = this.Id });
        }

        protected override bool CanRemoveTag(object param)
        {
            return true;
        }
    }
}
