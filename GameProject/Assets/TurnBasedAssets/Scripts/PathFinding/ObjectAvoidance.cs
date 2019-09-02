using System.Collections.Generic;
using System.Linq;
using TurnBasedAssets.Scripts.Controllers;
using TurnBasedAssets.Scripts.Interface;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PathFinding
{
    public class ObjectAvoidance : IAvoider
    {
        public List<Controller> Objects = new List<Controller>();

        public void UpdateSpaces(Controller controller)
        {
            Objects.Add(controller);
        }
    }
}
