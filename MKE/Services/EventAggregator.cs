using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKE.Services
{
    public class EventAggregator
    {
        private readonly Dictionary<Type, List<Action<object>>> _eventSubscribers = new();

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
