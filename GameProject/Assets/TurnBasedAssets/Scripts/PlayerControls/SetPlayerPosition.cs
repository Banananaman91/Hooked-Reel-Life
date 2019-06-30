using UnityEngine;

namespace TurnBasedAssets.Scripts.PlayerControls
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
