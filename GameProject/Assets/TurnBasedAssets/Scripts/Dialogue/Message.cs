using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Dialogue
{
    [Serializable]
    public class Message
    {
        [SerializeField] private int _npcMoodId;
        [SerializeField] private string _messageText;
        [SerializeField] private List<Response> _responses;
        public int NpcMoodId => _npcMoodId;
        public String MessageText => _messageText;
        public List<Response> Responses => _responses;
    }
}