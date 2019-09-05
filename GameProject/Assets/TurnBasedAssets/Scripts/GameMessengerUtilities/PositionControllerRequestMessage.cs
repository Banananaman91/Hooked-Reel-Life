using TurnBasedAssets.Scripts.Controllers;
using TurnBasedAssets.Scripts.PlayerControls;
using UnityEngine;

namespace TurnBasedAssets.Scripts.GameMessengerUtilities
{
    public class PositionControllerRequestMessage
    {
        public Controller RequestingComponent { get; }

        public PositionControllerRequestMessage(Controller requestingComponent) => RequestingComponent = requestingComponent;
    }
}
