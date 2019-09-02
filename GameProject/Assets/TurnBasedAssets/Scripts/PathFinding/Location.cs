using UnityEngine;

namespace TurnBasedAssets.Scripts.Pathfinding
{
    public class Location
    {
        public Location Parent { get; set; }
        public Vector3 PositionInWorld { get; }

        private float G => Parent?.G + 1 ?? 0;
        private float H { get; }
        public float F => Mathf.Round(G + H);


        public Location(Vector3 position, Vector3 destination, Location parent)
        {
            Parent = parent;
            PositionInWorld = position;
            H = Vector3.Distance(position, destination);
        }
    }
}