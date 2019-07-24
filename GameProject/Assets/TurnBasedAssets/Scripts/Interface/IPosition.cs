using UnityEngine;

namespace TurnBasedAssets.Scripts.Interface
{
    public interface IPosition
    {
        Vector3 RePosition(Vector3 position, Vector3 planePosition);
    }
}
