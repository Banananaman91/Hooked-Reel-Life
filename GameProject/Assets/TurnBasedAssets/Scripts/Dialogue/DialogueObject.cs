using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedAssets.Scripts.Dialogue
{
    public class DialogueObject : MonoBehaviour, ISelection
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private NpcImages _npcImages;
        [SerializeField] private RenderDialogue _pageRender;
        [SerializeField] private int _startMessage;
        
        public void Select() => _pageRender.PlayParagraphCycle(_dialogue, _npcImages,_startMessage);
        

        public void DeSelect()
        {
            
        }
    }
}
