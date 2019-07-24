using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedAssets.Scripts.Dialogue
{
    [Serializable]
    public class NpcMoods
    {
        [SerializeField] private string _npcName;
        [SerializeField] private List<Image> _npcMoodImages;
        public string NpcName => _npcName;
        public List<Image> NpcMoodImages => _npcMoodImages;
    }
}