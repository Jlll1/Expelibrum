using System;
using System.Collections.Generic;

namespace Expelibrum.Services.Events
{
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<string, List<Action<EventArgs>>> eventsToSubscribers = new Dictionary<string, List<Action<EventArgs>>>();

        public void PublishEvent(string eventName, EventArgs eventArgs)
        {
            if (!eventsToSubscribers.ContainsKey(eventName))
            {
                eventsToSubscribers[eventName] = new List<Action<EventArgs>>();
            }
            foreach (var action in eventsToSubscribers[eventName])
            {
                action.Invoke(eventArgs);
            }
        }

        public void SubscribeToEvent(string eventName, Action<EventArgs> action)
        {
            if (!eventsToSubscribers.ContainsKey(eventName))
            {
                eventsToSubscribers[eventName] = new List<Action<EventArgs>>();
            }
            eventsToSubscribers[eventName].Add(action);
        }
    }
}
