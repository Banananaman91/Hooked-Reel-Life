using TurnBasedAssets.Scripts.MessageBroker;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Pathfinding
{
    public class PathFinderListener : MonoBehaviour
    {
        private void Awake() => MessageBroker.MessageBroker.Instance.RegisterMessageOfType<PathFinderRequestMessage>(OnPathFinderComponentRequestMessage);
        
        private void OnPathFinderComponentRequestMessage(PathFinderRequestMessage message) => message.RequestingComponent.PathInitialise(new Pathfinder());
    }
}
