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
        [SerializeField] private GameObject _pageImagePosition;
        [SerializeField] private float _sentenceSpeed;
        [SerializeField] private Button _option;
        [SerializeField] private GameObject[] _buttonPositions;
        [SerializeField] private Image _dialogueBackground;
        [SerializeField] private GameObject _dialogueBox;
        private List<Button> _responseOptions = new List<Button>();
        private Image _previousImage;
        private NpcMoods _npcImageMoods;
        private Image _newMoodImage;
        private Coroutine _currentRoutine;

        private void RenderPageText(string pageName, string pageText)
        {
            _pageName.text = pageName;
            _pageText.text = pageText;
        }

        private IEnumerator Play(Dialogue npc, Message npcMessage)
        {
            var sb = new StringBuilder();
            var letters = npcMessage.MessageText.ToCharArray();
            foreach (var letter in letters)
            {
                sb.Append(letter);
                RenderPageText(npc.NpcName, sb.ToString());
                yield return new WaitForSeconds(_sentenceSpeed);
            }
            yield return null;
        }

        public void PlayParagraphCycle(Dialogue npcDialogue, NpcImages npcImages, int paragraphNumber)
        {
            if (paragraphNumber < 0)
            {
                EndDialogue();
            }
            else
            {
                foreach (var npcImageMoods in npcImages.NpcImage)
                {
                    var npcImageName = npcImageMoods.NpcName.ToLower();
                    if (npcImageName.Contains(npcDialogue.NpcName.ToLower()))
                    {
                        _npcImageMoods = npcImageMoods;
                    }
                }
                foreach (var npcMood in _npcImageMoods.NpcMoodImages)
                {
                    var npcMoodName = npcMood.name.ToLower();
                    if (npcMoodName.Contains(npcDialogue.Messages[paragraphNumber].NpcMood.ToLower()))
                    {
                        _newMoodImage = npcMood;
                    }
                }

                if (!_dialogueBox.activeSelf)
                {
                    _dialogueBox.SetActive(true);
                }

                if (_previousImage != null)
                {
                    Destroy(_previousImage.gameObject);
                }

                if (_newMoodImage != null)
                {
                    var newImage = Instantiate(_newMoodImage, _pageImagePosition.transform);
                    newImage.transform.SetParent(_dialogueBackground.transform);
                    _previousImage = newImage;
                }
                if (_currentRoutine != null) StopCoroutine(_currentRoutine);
                _pageName.text = string.Empty;
                _pageText.text = string.Empty;

                _currentRoutine = StartCoroutine(Play(npcDialogue, npcDialogue.Messages[paragraphNumber]));
                GetResponse(npcDialogue, npcDialogue.Messages[paragraphNumber], npcImages);

            }
        }

        private void GetResponse(Dialogue npcMessage, Message npcResponses, NpcImages npcImages)
        {
            foreach (Button buttonObject in _responseOptions)
            {
                Destroy(buttonObject.gameObject);
            }
            _responseOptions.Clear();
            for (var index = 0; index < npcResponses.Responses.Count; index++)
            {
                var response = npcResponses.Responses[index];
                var button = Instantiate(_option, _buttonPositions[index].transform.position,
                    _buttonPositions[index].transform.rotation);
                button.transform.SetParent(_dialogueBackground.transform);
                button.GetComponentInChildren<Text>().text = response.Reply;
                _responseOptions.Add(button);
                button.onClick.AddListener(() => PlayParagraphCycle(npcMessage, npcImages, response.Next));
            }
        }

        private void EndDialogue()
        {
            if (_currentRoutine != null) StopCoroutine(_currentRoutine);
            _pageName.text = string.Empty;
            _pageText.text = string.Empty;
            _dialogueBox.SetActive(false);
        }
    }
}
