using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.PlayerControls;
using UnityEngine;

namespace TurnBasedAssets.Scripts.SelectedTile
{
    public class SelectedTile : MonoBehaviour, ISelection
    {
        [SerializeField]private PlayerController _playerController;
        [SerializeField]private MouseSelection _mouseSelection;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _mouseSelection = FindObjectOfType<MouseSelection>();
            StartCoroutine(_playerController.FindPossibleMovePositions(_mouseSelection.RawGridPoint));
        }

        public void Select()
        {
            StartCoroutine(_playerController.StartPlayerMovement());
        }

   
        public void DeSelect()
        {
            _mouseSelection.Selection = null;
            Destroy(gameObject);
        }
    }
}
