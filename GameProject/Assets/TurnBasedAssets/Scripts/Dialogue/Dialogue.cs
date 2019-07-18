using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedAssets.Scripts.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 1), Serializable]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private int _npcID;
        [SerializeField] private string _npcName;
        [SerializeField] private List<Message> _messages;
        public int NpcId => _npcID;
        public String NpcName => _npcName;
        public List<Message> Messages => _messages;
    }
}
