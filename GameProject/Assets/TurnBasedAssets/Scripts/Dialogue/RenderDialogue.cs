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
            if (!_dialogueBox.activeSelf) _dialogueBox.SetActive(true);
            
            if (paragraphNumber < 0)
            {
                EndDialogue();
            }
            else
            {
                if (_previousImage != null) Destroy(_previousImage.gameObject);
                var newImage = Instantiate(npcImages.NpcImage[npcDialogue.NpcId].NpcMoodImages[npcDialogue.Messages[paragraphNumber].NpcMoodId], _pageImagePosition.transform);
                newImage.transform.SetParent(_dialogueBackground.transform);
                _previousImage = newImage;
                StartCoroutine(Play(npcDialogue, npcDialogue.Messages[paragraphNumber]));
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
            int buttonCount = 0;
            foreach (var response in npcResponses.Responses)
            {
                var button = Instantiate(_option, _buttonPositions[buttonCount].transform.position, _buttonPositions[buttonCount].transform.rotation);
                button.transform.SetParent(_dialogueBackground.transform);
                button.GetComponentInChildren<Text>().text = response.Reply;
                _responseOptions.Add(button);
                button.onClick.AddListener(() => PlayParagraphCycle(npcMessage, npcImages, response.Next));
                ++buttonCount;
            }
        }

        private void EndDialogue()
        {
            _pageName.text = string.Empty;
            _pageText.text = string.Empty;
            _dialogueBox.SetActive(false);
        }
    }
}
