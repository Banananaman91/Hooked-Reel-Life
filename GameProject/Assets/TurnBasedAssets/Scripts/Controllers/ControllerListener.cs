using TurnBasedAssets.Scripts.GameMessengerUtilities;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Controllers
{
    public class ControllerListener : MonoBehaviour
    {
        private void Awake() => GameMessengerUtilities.MessageBroker.Instance.RegisterMessageOfType<PositionControllerRequestMessage>(OnPositionControllerComponentRequestMessage);
        
        private void OnPositionControllerComponentRequestMessage(PositionControllerRequestMessage positionControllerRequestMessage) => positionControllerRequestMessage.RequestingComponent.PositionInitialise(new PositionController());
    }
}
