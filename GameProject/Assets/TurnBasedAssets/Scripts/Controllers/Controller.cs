using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.MessageBroker;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.PathFinding;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Controllers
{
    public class Controller : MonoBehaviour, IObjectAvoidanceInitialisable
    {
        internal IPathfinder Pathfinder;
        private IPosition _position;
        private ObjectAvoidance _avoidance;
        [SerializeField] internal MouseSelection mouseSelectionScript;
        [SerializeField] internal GameObject pathFinderTiles;
        [SerializeField] internal float movementSpeed;
        [SerializeField] internal float rotationSpeed;
        public Renderer RenderBounds => GetComponent<Renderer>();

        public void Start()
        {
            GetPathfinder();
            GetObjectReposition();
            SetPosition();
            AddToObjectAvoidance();
            AvoidMe();
        }

        private void GetPathfinder()
        {
            //uses message broker to return pathfinder delegate
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new PathFinderRequestMessage(this));
        }

        private void GetObjectReposition()
        {
            //uses message broker to return object reposition delegate
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new PositionControllerRequestMessage(this));
        }

        private void AvoidMe()
        {
            _avoidance.Objects.Add(this);
        }

        private void AddToObjectAvoidance()
        {
            //uses message broker to return object avoidance delegate
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new ObjectRequestMessage(this));
        }

        public void ObjectInitialise(ObjectAvoidance objectAvoidance)
        {
            _avoidance = objectAvoidance;
        }
        
        public void PositionInitialise(IPosition position)
        {
            _position = position;
        }
        
        public void PathInitialise(IPathfinder pathfinder)
        {
            Pathfinder = pathfinder;
        }
        
        private void SetPosition()
        {
            transform.position = _position.Reposition(transform.position, mouseSelectionScript.PlanePosition);
        }
    }
}
