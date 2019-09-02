using TurnBasedAssets.Scripts.Controllers;
using TurnBasedAssets.Scripts.PlayerControls;

namespace TurnBasedAssets.Scripts.MessageBroker
{
    public struct PathFinderRequestMessage
    {
        public Controller RequestingComponent { get; }

        public PathFinderRequestMessage(Controller requestingComponent) => RequestingComponent = requestingComponent;
        
    }
}
