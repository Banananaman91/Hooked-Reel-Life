﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 1)]
    [Serializable]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private int _npcID;
        [SerializeField] private string _npcName;

        [SerializeField] private List<Message> _messages;
        public String NpcName => _npcName;
        public List<Message> Messages => _messages;

    }

    [Serializable]
    public class Message
    {
        [SerializeField] private string _messageText;
        [SerializeField] private List<Response> _responses;
        public String MessageText => _messageText;
        public List<Response> Responses => _responses;
    }

    [Serializable]
    public class Response
    {
        [SerializeField] private int _next;
        [SerializeField] private string _reply;
        public int Next => _next;
        public string Reply => _reply;
    }
}
