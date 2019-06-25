using UnityEngine;

namespace TurnBasedAssets.Scripts
{
    public class AlternateCubeSelection : MonoBehaviour, ISelection
    {
        [SerializeField] private Renderer _boxRender;
        // Start is called before the first frame update
        public void Select()
        {
            _boxRender.material.color = Color.blue;
        }


        public void DeSelect()
        {
            _boxRender.material.color = Color.white;
        }
    }
}
