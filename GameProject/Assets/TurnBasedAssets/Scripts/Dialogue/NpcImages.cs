using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Dialogue
{
    [CreateAssetMenu(fileName = "NpcImages", menuName = "NpcImages", order = 2), Serializable]
    public class NpcImages : ScriptableObject
    {
        [SerializeField] private List<NpcMoods> _npcImage;
        public List<NpcMoods> NpcImage => _npcImage;
    }
}
