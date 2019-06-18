using UnityEngine;

namespace Turn_based_assets.Scripts
{
    public interface ISelection
    {
        void Select(GameObject selection);

        void DeSelect(GameObject selection);
    }
}
