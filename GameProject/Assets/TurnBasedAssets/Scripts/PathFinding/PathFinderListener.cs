using TurnBasedAssets.Scripts.GameMessengerUtilities;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Pathfinding
{
    public class PathFinderListener : MonoBehaviour
    {
        private void Awake() => GameMessengerUtilities.MessageBroker.Instance.RegisterMessageOfType<PathFinderRequestMessage>(OnPathFinderComponentRequestMessage);
        
        private void OnPathFinderComponentRequestMessage(PathFinderRequestMessage message) => message.RequestingComponent.PathInitialise(new Pathfinder());
    }
}
