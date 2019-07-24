using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.MessageBroker;
using TurnBasedAssets.Scripts.MouseController;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Controllers
{
    public class ObjectPositioner : Controller
    {
        private IPosition _iPosition;
        [SerializeField] private MouseSelection _mouseSelectionScript;

        private void Start()
        {
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new PositionControllerRequestMessage(this));
            SetPosition();
        }

        private void SetPosition()
        {
            transform.position = _iPosition.RePosition(transform.position, _mouseSelectionScript.PlanePosition);
        }

        public override void Initialise(IPosition iPosition)
        {
            _iPosition = iPosition;
        }
    }
}
