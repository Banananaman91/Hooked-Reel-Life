using UnityEngine;

namespace Turn_based_assets.Scripts
{
    public class AlternateCubeSelection : MonoBehaviour, ISelection
    {
        // Start is called before the first frame update
        public void Select()
        {
            
            this.GetComponent<Renderer>().material.color = Color.blue;
        }


        public void DeSelect()
        {
            this.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
