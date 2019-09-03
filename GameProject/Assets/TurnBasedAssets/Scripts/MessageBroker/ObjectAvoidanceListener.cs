using TurnBasedAssets.Scripts.PathFinding;
using UnityEngine;

namespace TurnBasedAssets.Scripts.MessageBroker
{
    public class ObjectAvoidanceListener : MonoBehaviour
    {
        private ObjectAvoidance _avoidanceInstance;

        private void Awake()
        {
            MessageBroker.Instance.RegisterMessageOfType<ObjectRequestMessage>(OnObjectAvoidanceRequestMessage);
            _avoidanceInstance = new ObjectAvoidance();
        }

        private void OnObjectAvoidanceRequestMessage(ObjectRequestMessage message) => message.RequestingComponent.ObjectInitialise(_avoidanceInstance);
    }
}
