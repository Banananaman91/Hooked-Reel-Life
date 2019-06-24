using UnityEngine;

namespace TurnBasedAssets.Scripts
{
    public class CubeSelection : MonoBehaviour, ISelection
    {
        [SerializeField] private Renderer cubeRender;
        public void Select()
        {
            cubeRender.material.color = Color.red;
        }


        public void DeSelect()
        {
            cubeRender.material.color = Color.white;
        }
    }
}

