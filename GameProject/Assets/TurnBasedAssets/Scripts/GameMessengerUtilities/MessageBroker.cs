using System;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Pathfinding;

namespace TurnBasedAssets.Scripts.GameMessengerUtilities
{
    public class MessageBroker : PocoSingleton<MessageBroker>
    {
        private Dictionary<Type, List<Delegate>> _messageMappings;
        
        public MessageBroker() => _messageMappings = new Dictionary<Type, List<Delegate>>();
        
        
        public void RegisterMessageOfType<T>(Action<T> messageHandler)
        {
            var messageType = typeof(T);
            if(!_messageMappings.ContainsKey(messageType)) _messageMappings.Add(messageType, new List<Delegate>());
            _messageMappings[messageType].Add(messageHandler);
        }

        public void SendMessageOfType<T>(T message)
        {
            var messageType = typeof(T);
            if (!_messageMappings.ContainsKey(messageType)) return;

            foreach (var listener in _messageMappings[messageType])
            {
                listener.DynamicInvoke(message);
            }
        }
    }
}
