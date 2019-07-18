using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedAssets.Scripts.Dialogue
{
    [CreateAssetMenu(fileName = "NpcImages", menuName = "NpcImages", order = 2)]
    public class NpcImages : ScriptableObject
    {
        [SerializeField] private List<NpcMoods> _npcImage;
        public List<NpcMoods> NpcImage => _npcImage;
    }
    
    [Serializable]
    public class NpcMoods
    {
        [SerializeField] private List<Image> _npcMoodImages;
        public List<Image> NpcMoodImages => _npcMoodImages;
    }
}
