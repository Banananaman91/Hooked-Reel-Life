using TurnBasedAssets.Scripts.Interface;
using TurnBasedAssets.Scripts.MessageBroker;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Controllers
{
    public class ObjectPositioner : Controller
    {
        private void Start()
        {
            MessageBroker.MessageBroker.Instance.SendMessageOfType(new PositionControllerRequestMessage(this));
            SetPosition();
            AddToObjectAvoider();
            //VertexLocations();
            AvoidMe();
        }

        private void SetPosition()
        {
            transform.position = Position.Reposition(transform.position, mouseSelectionScript.PlanePosition);
        }
    }
}
