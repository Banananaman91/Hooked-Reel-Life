using UnityEngine;

namespace TurnBasedAssets.Scripts.PathFinding
{
    public class PathFinderListener : MonoBehaviour
    {
        private void Awake() => PathMessenger.Instance.RegisterMessageOfType<PathFinderRequestMessage>(OnPathFinderComponentRequestMessage);
        
        private void OnPathFinderComponentRequestMessage(PathFinderRequestMessage message) => message.RequestingComponent.Initialise(new PathFinder());
    }
}
