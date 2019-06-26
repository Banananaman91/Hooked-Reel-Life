using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turn_based_assets.Scripts
{
    public class SetPlayerPosition : MonoBehaviour, ISelection
    {
        public void Select()
        {
            PlayerController playerControler = FindObjectOfType<PlayerController>();

            if(playerControler != null)
            {
                playerControler.currentPos = transform.position;
                //playerControler.MovePlayer();
            }
        }


        public void DeSelect()
        {
            
        }
    }
}
