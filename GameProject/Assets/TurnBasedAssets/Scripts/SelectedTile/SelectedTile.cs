using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.Characters.PlayerControls;
using TurnBasedAssets.Scripts.Characters.NPCControls;
using UnityEngine;

namespace TurnBasedAssets.Scripts.SelectedTile
{
    public class SelectedTile : MonoBehaviour, ISelection
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private NPCController _npcController;
        [SerializeField] private MouseSelection _mouseSelection;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _npcController = FindObjectOfType<NPCController>();
            _mouseSelection = FindObjectOfType<MouseSelection>();
            StartCoroutine(_playerController.FindPossibleMovePositions(_mouseSelection.RawGridPoint));

            if(!_npcController.pathFound)
                StartCoroutine(_npcController.FindPossibleMovePositions());
        }

        public void Select()
        {
            //StartCoroutine(_playerController.StartPlayerMovement());
            StartCoroutine(_npcController.StartNPCMovement());
        }

   
        public void DeSelect()
        {
            _mouseSelection.Selection = null;
            Destroy(gameObject);
        }
    }
}
