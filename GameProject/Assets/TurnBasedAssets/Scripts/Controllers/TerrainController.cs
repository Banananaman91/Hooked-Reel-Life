using System;
using TurnBasedAssets.Scripts.Interface;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Controllers
{
    public class TerrainController : Controller
    {
        private void Start()
        {
            AddToObjectAvoider();
            //VertexLocations();
            AvoidMe();
        }
    }
}
