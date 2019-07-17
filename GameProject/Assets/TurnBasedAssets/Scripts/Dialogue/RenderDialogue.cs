using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedAssets.Scripts.Dialogue
{
    public class RenderDialogue : MonoBehaviour
    {
        [SerializeField] private Text _pageName;
        [SerializeField] private Text _pageText;
        [SerializeField] private float _sentenceSpeed;
        [SerializeField] private Button _option;
        [SerializeField] private int _buttonDistance;
        [SerializeField] private Vector3 _buttonPosition;
        private List<Button> _responseOptions;
        private Button _previousButton;
        private int Next { get; set; }
        private Vector3 PreviousButtonPosition => _previousButton.transform.position;

        private void RenderPageText(string pageName, string pageText)
        {
            _pageName.text = pageName;
            _pageText.text = pageText;
        }

        private IEnumerator Play(Message npcMessage)
        {
            var sb = new StringBuilder();
            var letters = npcMessage.MessageText.ToCharArray();
            foreach (var letter in letters)
            {
                sb.Append(letter);
                RenderPageText(npcMessage.ToString(), sb.ToString());
                yield return new WaitForSeconds(_sentenceSpeed);
            }
            yield return null;
        }

        public IEnumerator RunParagraphCycle(Dialogue npc)
        {
            int paragraphCounter = 0;
            Coroutine currentRoutine = null;
            while (paragraphCounter < npc.Messages.Count)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                //currentRoutine = StartCoroutine(Play(npc));
                ++paragraphCounter;
                yield return new WaitForSeconds(_sentenceSpeed);
                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }
                yield return null;
            }
            if (currentRoutine != null) StopCoroutine(currentRoutine);
        }

        public IEnumerator PlayParagraphCycle(Dialogue npcDialogue)
        {
            int endCount = 0;
            int nextParagraph = 0;
            Coroutine currentRoutine = null;
            Next = npcDialogue.Messages[endCount].Responses[endCount].Next;
            while (endCount <= Next)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                currentRoutine = StartCoroutine(Play(npcDialogue.Messages[nextParagraph]));
                yield return StartCoroutine(GetResponse(npcDialogue.Messages[nextParagraph], nextResponse => Next = nextResponse));
                nextParagraph = Next;
            }

            yield return null;
        }

        private IEnumerator GetResponse(Message npcResponses, Action<int> nextResponse)
        {
            _responseOptions = new List<Button>();
            foreach (var response in npcResponses.Responses)
            {
                var Button = Instantiate(_option);
                Button.GetComponentInChildren<Text>().text = response.Reply;
                if (_previousButton != null)
                {
                    Button.transform.position = new Vector3(PreviousButtonPosition.x + _buttonDistance, PreviousButtonPosition.y, PreviousButtonPosition.z);
                }
                else
                {
                    Button.transform.position = new Vector3(_buttonPosition.x, _buttonPosition.y, _buttonPosition.z);
                }
                _previousButton = Button;
                _responseOptions.Add(Button);
            }
            
            
            
            yield return null;
        }
    }
}
