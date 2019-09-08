using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.Characters;
using TurnBasedAssets.Scripts.Characters.PlayerControls;
using UnityEngine;

namespace TurnBasedAssets.Scripts.SelectedTile
{
    public class SelectTile : MonoBehaviour, ISelection
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private MouseSelection _mouseSelection;
        [SerializeField] private MoveManager _moveManager;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _mouseSelection = FindObjectOfType<MouseSelection>();
            _moveManager = FindObjectOfType<MoveManager>();

            StartCoroutine(_playerController.VisualisePath());
        }

        public void Select()
        {
            _moveManager.StartCharacterMoves(true);
        }

   
        public void DeSelect()
        {
            _mouseSelection.Selection = null;
            Destroy(gameObject);
        }
    }
}
