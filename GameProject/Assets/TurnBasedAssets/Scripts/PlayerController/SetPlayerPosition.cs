using TurnBasedAssets.Scripts.Interface;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PlayerController
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
