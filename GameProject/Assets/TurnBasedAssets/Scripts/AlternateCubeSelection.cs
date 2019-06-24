using UnityEngine;

namespace TurnBasedAssets.Scripts
{
    public class AlternateCubeSelection : MonoBehaviour, ISelection
    {
        [SerializeField] private Renderer boxRender;
        // Start is called before the first frame update
        public void Select()
        {
            boxRender.material.color = Color.blue;
        }


        public void DeSelect()
        {
            boxRender.material.color = Color.white;
        }
    }
}
