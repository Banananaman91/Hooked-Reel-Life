using UnityEngine;
using UnityEngine.Tilemaps;

namespace Turn_based_assets.Scripts
{
    public class GridSelectionService : MonoBehaviour, ISelection
    {
        [SerializeField] private Tilemap tileGrid;
        public void Select(GameObject selection)
        {
            tileGrid.SetColor(default, Color.blue);
        }

        public void DeSelect(GameObject selection)
        {
            tileGrid.SetColor(default, default);
        }
    }
}