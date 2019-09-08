using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.Characters;
using TurnBasedAssets.Scripts.Characters.PlayerControls;
//using TurnBasedAssets.Scripts.Characters.NPCControls;
using UnityEngine;

namespace TurnBasedAssets.Scripts.SelectedTile
{
    public class SelectTile : MonoBehaviour, ISelection
    {
        [SerializeField] private PlayerController _playerController;
        //[SerializeField] private NPCController _npcController;
        [SerializeField] private MouseSelection _mouseSelection;

        [SerializeField] private MoveManager _moveManager;

        //public bool confirmedMove;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            //_npcController = FindObjectOfType<NPCController>();
            _mouseSelection = FindObjectOfType<MouseSelection>();

            _moveManager = FindObjectOfType<MoveManager>();

            //confirmedMove = false;
            _playerController.goalPosition = _mouseSelection.RawGridPoint;
            StartCoroutine(_playerController.VisualisePath());

            //if(!_npcController.pathFound)
            //    StartCoroutine(_npcController.FindPossibleMovePositions());
        }

        public void Select()
        {
            _moveManager.StartCharacterMoves(true);

            //confirmedMove = true;
            //StartCoroutine(_playerController.StartPlayerMovement());
            //StartCoroutine(_npcController.StartNPCMovement());
        }

   
        public void DeSelect()
        {
            _mouseSelection.Selection = null;
            Destroy(gameObject);
        }
    }
}
