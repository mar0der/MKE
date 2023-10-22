using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKE.Services
{
    public class EventAggregator
    {
        private static readonly Lazy<EventAggregator> _instance = new Lazy<EventAggregator>(() => new EventAggregator());

        public static EventAggregator Instance => _instance.Value;

        private readonly Dictionary<Type, List<Action<object>>> _eventSubscribers = new();

        private EventAggregator() { }  // Private constructor ensures it's a singleton.


        public void Publish<TMessage>(TMessage message)
        {
            var messageType = typeof(TMessage);
            if (_eventSubscribers.ContainsKey(messageType))
            {
                foreach (var action in _eventSubscribers[messageType].ToList())
                {
                    action(message);
                }
            }
        }

        public void Subscribe<TMessage>(Action<TMessage> action)
        {
            var messageType = typeof(TMessage);
            if (!_eventSubscribers.ContainsKey(messageType))
            {
                _eventSubscribers[messageType] = new List<Action<object>>();
            }
            _eventSubscribers[messageType].Add(msg => action((TMessage)msg));
        }
    }

}
