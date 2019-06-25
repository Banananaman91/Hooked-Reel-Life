using UnityEngine;

namespace TurnBasedAssets.Scripts
{
    public class RotateCube : MonoBehaviour, ISelection
    {
        public void Select() => MoveCube();
        

        public void DeSelect()
        {
            Debug.Log("Deselect");
        }

        public void MoveCube()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
        }
    }
}