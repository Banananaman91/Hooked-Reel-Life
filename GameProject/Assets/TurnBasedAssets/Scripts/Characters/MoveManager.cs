using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Characters.PlayerControls;
using TurnBasedAssets.Scripts.Characters.NPCControls;
using TurnBasedAssets.Scripts.MouseController;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Characters
{
    public class MoveManager : MonoBehaviour
    {
        [SerializeField] private MouseSelection mouseSelection;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private List<NPCController> npcControllers;

        public void StartCharacterMoves(bool isPlayersTurn)
        {
            switch (isPlayersTurn)
            {
                case true:
                    StartCoroutine(playerController.MoveCharacterAcrossPath());
                    break;

                case false:
                    foreach (NPCController NPC in npcControllers)
                    {
                        StartCoroutine(NPC.VisualisePath());
                    }

                    mouseSelection.Selection.DeSelect();
                    break;

                default:
                    break;

            }
            
//            switch (_characterType)
//            {
//                case CharacterTypes.Player:
//                    _playerFinishedMove = false;
//                    StartCoroutine(MoveCharacterAcrossPath());
//                    if (_playerFinishedMove) goto case CharacterTypes.NPC;
//                    break;
//                case CharacterTypes.NPC:
//                    Debug.Log("npc move");
//                    StartCoroutine(MoveCharacterAcrossPath());
//                    
//                    goto case CharacterTypes.Player;
//            }
        }
    }
}
