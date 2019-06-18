using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Turn_based_assets.Scripts
{
    public class CubeSelection : MonoBehaviour, ISelection
    {
        public void Select(GameObject selection)
        {
            selection.GetComponent<Renderer>().material.color = Color.red;
        }


        public void DeSelect(GameObject selection)
        {
            selection.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}

