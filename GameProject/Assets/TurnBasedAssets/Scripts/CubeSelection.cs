using UnityEngine;

namespace TurnBasedAssets.Scripts
{
    public class CubeSelection : MonoBehaviour, ISelection
    {
        [SerializeField] private Renderer _cubeRender;
        public void Select()
        {
            _cubeRender.material.color = Color.red;
        }


        public void DeSelect()
        {
            _cubeRender.material.color = Color.white;
        }
    }
}

