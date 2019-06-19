using UnityEngine;

namespace Turn_based_assets.Scripts
{
    public class RotateCube : MonoBehaviour, ISelection
    {
        public void Select()
        {
            MoveCube();
        }

        public void DeSelect()
        {
            Debug.Log("Deselect");
        }

        public void MoveCube()
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 5);
        }
    }
}