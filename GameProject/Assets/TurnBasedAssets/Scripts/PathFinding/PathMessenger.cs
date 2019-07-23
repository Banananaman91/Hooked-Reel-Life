using System;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Interface;

namespace TurnBasedAssets.Scripts.PathFinding
{
    public class PathMessenger : PocoSingleton<PathMessenger>
    {
        private Dictionary<Type, List<Delegate>> _messageMappings;
        
        public PathMessenger()
        {
            _messageMappings = new Dictionary<Type, List<Delegate>>();
        }
        
        public void RegisterMessageOfType<T>(Action<T> messageHandler) where T : struct
        {
            var messageType = typeof(T);
            if(!_messageMappings.ContainsKey(messageType))
            {
                _messageMappings.Add(messageType, new List<Delegate>());
            }
            
            _messageMappings[messageType].Add(messageHandler);
        }

        public void SendMessageOfType<T>(T message) where T : struct
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
