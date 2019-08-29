using UnityEngine;

namespace TurnBasedAssets.Scripts.Interface
{
    public interface IPosition
    {
        Vector3 Reposition(Vector3 position, Vector3 planePosition);
    }
}
