using Expelibrum.Services.Events;
using Expelibrum.UI.Events;

namespace Expelibrum.UI.ViewModels
{
    public class NameTagViewModel : TagViewModelBase
    {
        public NameTagViewModel(int id, IEventAggregator ea) : base (id, ea)
        {
        }

        protected override void SubscribeToEvents()
        {
            _ea.SubscribeToEvent("NameTagCountChanged", base.OnTagCountChanged);
        }

        protected override void OnRemoveTag(object param)
        {
            _ea.PublishEvent("NameTagRemoveRequested", new TagRemoveRequestedEventArgs { Id = this.Id });
        }
    }
}
