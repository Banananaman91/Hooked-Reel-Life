using System;
using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.PlayerControls;

namespace TurnBasedAssets.Scripts.PathFinding
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
