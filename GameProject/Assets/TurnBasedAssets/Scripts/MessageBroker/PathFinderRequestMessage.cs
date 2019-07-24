using TurnBasedAssets.Scripts.PlayerControls;

namespace TurnBasedAssets.Scripts.MessageBroker
{
    public struct PathFinderRequestMessage
    {
        public PlayerController RequestingComponent { get; }

        public PathFinderRequestMessage(PlayerController requestingComponent)
        {
            RequestingComponent = requestingComponent;
        }
    }
}
