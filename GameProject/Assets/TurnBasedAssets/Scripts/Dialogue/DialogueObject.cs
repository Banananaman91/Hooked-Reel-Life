using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedAssets.Scripts.Dialogue
{
    public class DialogueObject : MonoBehaviour, ISelection
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private RenderDialogue _pageRender;
        
        public void Select()
        {
            StartCoroutine(_pageRender.RunParagraphCycle(_dialogue));
        }

        public void DeSelect() => StopCoroutine(_pageRender.RunParagraphCycle(_dialogue));
        
    }
}
