using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.GameMessengerUtilities;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.PathFinding;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Controllers
{
    [RequireComponent(typeof(Renderer))]
    public class Controller : MonoBehaviour, IObjectAvoidanceInitialisable
    {
        protected IPathfinder Pathfinder;
        private IPosition _position;
        private ObjectAvoidance _avoidance;
        private Renderer _renderBounds;
        [SerializeField] protected MouseSelection mouseSelectionScript;
        [SerializeField] protected GameObject pathFinderTiles;
        [SerializeField] protected float movementSpeed;
        [SerializeField] protected float rotationSpeed;
        public Renderer RenderBounds => _renderBounds == null ? _renderBounds : _renderBounds = GetComponent<Renderer>();

        public void Start()
        {
            GetPathfinder();
            GetObjectReposition();
            SetPosition();
            AddToObjectAvoidance();
            AvoidMe();
        }

        private void GetPathfinder() => MessageBroker.Instance.SendMessageOfType(new PathFinderRequestMessage(this));
        
        private void GetObjectReposition() => MessageBroker.Instance.SendMessageOfType(new PositionControllerRequestMessage(this));
        
        private void AvoidMe() => _avoidance.Objects.Add(this);
        
        private void AddToObjectAvoidance() => MessageBroker.Instance.SendMessageOfType(new ObjectRequestMessage(this));
        
        public void ObjectInitialise(ObjectAvoidance objectAvoidance) => _avoidance = objectAvoidance;
        
        public void PositionInitialise(IPosition position) => _position = position;
        
        public void PathInitialise(IPathfinder pathfinder) => Pathfinder = pathfinder;
        
        private void SetPosition() => transform.position = _position.Reposition(transform.position, mouseSelectionScript.PlanePosition);
        
    }
}
