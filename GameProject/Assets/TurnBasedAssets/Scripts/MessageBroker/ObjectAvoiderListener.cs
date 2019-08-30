using TurnBasedAssets.Scripts.Pathfinding;
using TurnBasedAssets.Scripts.MessageBroker;
using TurnBasedAssets.Scripts.PathFinding;
using UnityEngine;

namespace TurnBasedAssets.Scripts.MessageBroker
{
    public class ObjectAvoiderListener : MonoBehaviour
    {
        private ObjectAvoidance _avoidanceInstance;

        private void Awake()
        {
            MessageBroker.Instance.RegisterMessageOfType<ObjectRequestMessage>(OnObjectAvoiderRequestMessage);
            _avoidanceInstance = new ObjectAvoidance();
        }

        private void OnObjectAvoiderRequestMessage(ObjectRequestMessage message) => message.RequestingComponent.ObjectInitialise(_avoidanceInstance);
    }
}
