using TurnBasedAssets.Scripts.Controllers;
using TurnBasedAssets.Scripts.Characters.PlayerControls;
using UnityEngine;

namespace TurnBasedAssets.Scripts.MessageBroker
{
    public class PositionControllerRequestMessage
    {
        public Controller RequestingComponent { get; }

        public PositionControllerRequestMessage(Controller requestingComponent) => RequestingComponent = requestingComponent;
    }
}
