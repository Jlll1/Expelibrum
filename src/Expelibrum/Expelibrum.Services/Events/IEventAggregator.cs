using System;

namespace Expelibrum.Services.Events
{
    public interface IEventAggregator
    {
        void PublishEvent(string eventName, EventArgs eventArgs);
        void SubscribeToEvent(string eventName, Action<EventArgs> action);
    }
}