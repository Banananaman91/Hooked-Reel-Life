using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.MouseController;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Controllers
{
    public class Controller : MonoBehaviour
    {
        internal IPathfinder Pathfinder;
        internal IPosition Position;
        [SerializeField] internal MouseSelection mouseSelectionScript;
        [SerializeField] internal GameObject pathFinderTiles;
        [SerializeField] internal float movementSpeed;
        [SerializeField] internal float rotationSpeed;
        public virtual void Initialise(IPosition iPosition)
        {
            
        }
    }
}
