using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Dialogue
{
    [Serializable]
    public class Message
    {
        [SerializeField] private string _messageText;
        [SerializeField] private string _npcMood;
        [SerializeField] private List<Response> _responses;
        public string NpcMood => _npcMood;
        public string MessageText => _messageText;
        public List<Response> Responses => _responses;
    }
}