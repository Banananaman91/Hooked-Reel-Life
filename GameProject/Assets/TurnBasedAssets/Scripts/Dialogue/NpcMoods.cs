using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedAssets.Scripts.Dialogue
{
    [Serializable]
    public class NpcMoods
    {
        [SerializeField] private List<Image> _npcMoodImages;
        public List<Image> NpcMoodImages => _npcMoodImages;
    }
}