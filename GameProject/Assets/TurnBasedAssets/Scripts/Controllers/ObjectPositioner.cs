using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.MessageBroker;

namespace TurnBasedAssets.Scripts.Controllers
{
    public class ObjectPositioner : Controller
    {
        private void Start()
        {
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new PositionControllerRequestMessage(this));
            SetPosition();
        }

        private void SetPosition()
        {
            transform.position = Position.Reposition(transform.position, mouseSelectionScript.PlanePosition);
        }

        public override void Initialise(IPosition iPosition)
        {
            Position = iPosition;
        }
    }
}
