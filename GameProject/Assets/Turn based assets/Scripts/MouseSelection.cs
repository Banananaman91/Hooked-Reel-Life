using System;
using UnityEngine;

namespace Turn_based_assets.Scripts
{
    public class MouseSelection : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (ray != null)
                {
                    
                }
            }
        }
    }
}