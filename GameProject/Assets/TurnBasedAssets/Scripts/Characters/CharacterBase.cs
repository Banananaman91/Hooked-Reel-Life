using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Controllers;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Characters
{
    public class CharacterBase : Controller
    {
        private IEnumerable<Vector3> _path = new List<Vector3>();
        private List<GameObject> _pathVisualized = new List<GameObject>();

        private Vector3 _previousLocation;
        private Vector3 _previousDistance;
    }
}
