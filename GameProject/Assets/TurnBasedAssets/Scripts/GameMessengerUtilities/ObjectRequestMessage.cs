using TurnBasedAssets.Scripts.Controllers;
using TurnBasedAssets.Scripts.Interface;

namespace TurnBasedAssets.Scripts.GameMessengerUtilities
{
    public struct ObjectRequestMessage
    {
        public IObjectAvoidanceInitialisable RequestingComponent { get; }

        public ObjectRequestMessage(IObjectAvoidanceInitialisable requestingComponent) => RequestingComponent = requestingComponent;
    }
}
