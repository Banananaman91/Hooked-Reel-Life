using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.MouseController;
using TurnBasedAssets.Scripts.PathFinding;
using UnityEngine;

namespace TurnBasedAssets.Scripts.PlayerControls
{
    public class PlayerController : MonoBehaviour
    {
        private PathFinder _pathFinder;
        private Vector3 _currentPos;
        private MouseSelection _mouseSelection;
        [SerializeField] private GameObject tile;
        private List<GameObject> _pathTiles = new List<GameObject>();

        private void Start()
        {
            if(GetComponent<PathFinder>() != null)
                _pathFinder = GetComponent<PathFinder>();
        }

        public IEnumerator FindPossibleMovePositions(Vector3 rawGridPoint)
        {
            IEnumerable<Vector3> path = new List<Vector3>();
            foreach (var TILE in _pathTiles)
            {
                Destroy(TILE);
            }
            _pathTiles?.Clear();
            yield return StartCoroutine(routine: _pathFinder.FindPath(transform.position, rawGridPoint, newPath => path = newPath));

            foreach (var LOCATION in path)
            {
                GameObject newTile = Instantiate(tile, new Vector3(LOCATION.x, LOCATION.y, LOCATION.z), Quaternion.identity);
                _pathTiles.Add(newTile);
                transform.position = Vector3.MoveTowards(transform.position, LOCATION, 1);
            }

            yield return null;
        }
    }
}
