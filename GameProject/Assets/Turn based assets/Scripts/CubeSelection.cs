using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Turn_based_assets.Scripts
{
    public class CubeSelection : MonoBehaviour, ISelection
    {
        public void Select()
        {
            this.GetComponent<Renderer>().material.color = Color.red;
        }


        public void DeSelect()
        {
            this.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}

